using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Auth;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Authorization.AuthenticatedUser;
using Rabiscado.Core.Settings;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;

namespace Rabiscado.Application.Services;

public class UserAuthService : BaseService, IUserAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User?> _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailService _emailService;
    private readonly IAuthenticatedUser _authenticatedUser;

    public UserAuthService(IMapper mapper, INotificator notificator, IUserRepository userRepository,
        IPasswordHasher<User?> passwordHasher, IJwtService jwtService, IOptions<JwtSettings> jwtSettings,
        IEmailService emailService, IAuthenticatedUser authenticatedUser) : base(mapper, notificator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _emailService = emailService;
        _authenticatedUser = authenticatedUser;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<TokenDto?> Login(LoginDto loginDto)
    {
        User? user;
        if (loginDto.Email.Contains('@'))
        {
            user = await _userRepository.GetByEmail(loginDto.Email);
        }
        else
        {
            user = await _userRepository.GetByPhone(loginDto.Email);
        }

        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
        if (passwordResult != PasswordVerificationResult.Failed)
            return new TokenDto
            {
                Token = await CreateToken(user)
            };

        Notificator.Handle("User and/or password are incorrect");
        return null;
    }

    public async Task SendEmailRecoverPassword(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user is null)
        {
            return;
        }

        user.TokenRecoverPassword = Guid.NewGuid();
        user.TokenExpires = DateTime.UtcNow.AddHours(1);
        _userRepository.Update(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            _emailService.SendEmailRecoverPassword(user);
        }
    }

    public async Task ChangePassword(ChangePasswordAuthenticatedUserDto dto)
    {
        var user = await _userRepository.GetById(_authenticatedUser.Id);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return;
        }

        if (_passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password) !=
            PasswordVerificationResult.Success)
        {
            Notificator.Handle("Invalid password!");
            return;
        }

        if (dto.NewPassword != dto.ConfirmNewPassword)
        {
            Notificator.Handle("The password and confirmation password do not match!");
            return;
        }

        user.Password = _passwordHasher.HashPassword(user, dto.NewPassword);
        _userRepository.Update(user);
        if (!user.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return;
        }

        if (!await _userRepository.UnitOfWork.Commit())
        {
            Notificator.Handle("An error occurred while changing the password");
        }
    }

    public async Task ChangePassword(ChangePasswordUserDto dto)
    {
        var user = await _userRepository.GetByEmail(dto.Email);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return;
        }

        if (user.TokenRecoverPassword != dto.TokenRecoverPassword || user.TokenExpires < DateTime.UtcNow)
        {
            Notificator.Handle("Invalid or expired token!");
            return;
        }

        if (dto.NewPassword != dto.ConfirmNewPassword)
        {
            Notificator.Handle("The password and confirmation password do not match!");
            return;
        }

        user.Password = _passwordHasher.HashPassword(user, dto.NewPassword);
        user.TokenRecoverPassword = null;
        user.TokenExpires = null;
        _userRepository.Update(user);
        if (!user.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return;
        }

        if (!await _userRepository.UnitOfWork.Commit())
        {
            Notificator.Handle("An error occurred while changing the password");
        }
    }

    private async Task<string> CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        claimsIdentity.AddClaim(new Claim("ProfessorType", user.IsProfessor.ToString()));
        claimsIdentity.AddClaim(new Claim("AdminType", user.IsAdmin.ToString()));
        var key = await _jwtService.GetCurrentSigningCredentials();
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.CommonValidIn,
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationHours),
            SigningCredentials = key
        });

        return tokenHandler.WriteToken(token);
    }
}