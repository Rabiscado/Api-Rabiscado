using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Rabiscado.Application.Adapters.Assas.Application.Contracts;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Customers;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments;
using Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Subscriptions;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Base;
using Rabiscado.Application.Dtos.V1.Plan;
using Rabiscado.Application.Dtos.V1.Subscription;
using Rabiscado.Application.Dtos.V1.Users;
using Rabiscado.Application.Notifications;
using Rabiscado.Core.Authorization.AuthenticatedUser;
using Rabiscado.Core.Enums;
using Rabiscado.Core.Extensions;
using Rabiscado.Domain.Contracts;
using Rabiscado.Domain.Contracts.Repositories;
using Rabiscado.Domain.Entities;
using SubscriptionDto = Rabiscado.Application.Adapters.Assas.Application.Dtos.V1.Payments.SubscriptionDto;

namespace Rabiscado.Application.Services;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IPlanRepository _planRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IExtractRepository _extractRepository;
    private readonly ICustomerService _customerService;
    private readonly ISubscriptionService _subscriptionService;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IUserPlanSubscriptionRepository _userPlanSubscriptionRepository;
    private readonly IAuthenticatedUser _authenticatedUser;
    private readonly IScheduledpaymentRepository _scheduledpaymentRepository;
    private readonly IExtractReceiptRepository _extractReceiptRepository;
    private readonly IFileService _fileService;
    private readonly IUserClassRepository _userClassRepository;
    private readonly ILogger<UserService> _logger;
    private readonly IReimbursementRepository _reimbursement;

    public UserService(IMapper mapper, INotificator notificator, IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher, IPlanRepository planRepository, ICourseRepository courseRepository,
        IExtractRepository extractRepository, ICustomerService customerService,
        ISubscriptionService subscriptionService, IUserPlanSubscriptionRepository userPlanSubscriptionRepository,
        IAuthenticatedUser authenticatedUser, IScheduledpaymentRepository scheduledpaymentRepository,
        IExtractReceiptRepository extractReceiptRepository, IFileService fileService, ILogger<UserService> logger,
        ISubscriptionRepository subscriptionRepository, IUserClassRepository userClassRepository, IReimbursementRepository reimbursement) : base(mapper,
        notificator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _planRepository = planRepository;
        _courseRepository = courseRepository;
        _extractRepository = extractRepository;
        _customerService = customerService;
        _subscriptionService = subscriptionService;
        _userPlanSubscriptionRepository = userPlanSubscriptionRepository;
        _authenticatedUser = authenticatedUser;
        _scheduledpaymentRepository = scheduledpaymentRepository;
        _extractReceiptRepository = extractReceiptRepository;
        _fileService = fileService;
        _logger = logger;
        _subscriptionRepository = subscriptionRepository;
        _userClassRepository = userClassRepository;
        _reimbursement = reimbursement;
    }

    public async Task<UserDto?> GetById(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is not null) return Mapper.Map<UserDto>(user);
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<UserDto>> GetAll()
    {
        var users = await _userRepository.GetAll();
        return Mapper.Map<List<UserDto>>(users);
    }

    public async Task<PagedDto<UserDto>> Search(UserSearchDto dto)
    {
        var users = await _userRepository.Search(dto);
        return Mapper.Map<PagedDto<UserDto>>(users);
    }

    public async Task<UserDto?> Create(CreateUserDto dto)
    {
        var user = Mapper.Map<User>(dto);
        if (!await Validate(user)) return null;

        user.Password = _passwordHasher.HashPassword(user, dto.Password);
        _userRepository.Create(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("An error occurred while saving the user.");
        return null;
    }

    public async Task<UserDto?> Update(int id, UpdateUserDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("The IDs provided are different!.");
            return null;
        }

        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        Mapper.Map(dto, user);
        if (!await Validate(user)) return null;

        _userRepository.Update(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("An error occurred while updating the user.");
        return null;
    }

    public async Task<bool> Disable(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }

        _userRepository.Disable(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return true;
        }

        Notificator.Handle("An error occurred while disabling the user.");
        return false;
    }

    public async Task<bool> Active(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }

        _userRepository.Active(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return true;
        }

        Notificator.Handle("An error occurred while disabling the user.");
        return false;
    }

    public async Task<bool> ToggleAdmin(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }

        user.IsAdmin = !user.IsAdmin;
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.Commit();
        return true;
    }

    public async Task<bool> ToggleInstructor(int id)
    {
        var user = await _userRepository.GetById(id);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return false;
        }

        user.IsProfessor = !user.IsProfessor;
        _userRepository.Update(user);
        await _userRepository.UnitOfWork.Commit();
        return true;
    }

    public async Task<SubscriptionUnsubscribreResponseDto?> Unsubscribe(string email)
    {
        var user = await _userRepository.GetByEmail(email);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        var customer = await _customerService.GetByEmail(user.Email);
        if (customer is null)
        {
            Notificator.Handle("An error occurred while getting the customer.");
            return null;
        }

        var subscriptions = await _subscriptionService.GetByCustomerId(customer.First().Id);
        if (subscriptions is null || !subscriptions.Data!.Any())
        {
            Notificator.Handle("You do not have a subscription.");
            return null;
        }
        
        var unsubscribed = await _subscriptionService.Unsubscribe(subscriptions.Data!.First().Id!);
        if (unsubscribed is null)
        {
            Notificator.Handle("An error occurred while unsubscribing.");
            return null;
        }
        
        if (!(subscriptions.Data!.OrderBy(s => s.NextDueDate).First().NextDueDate >= DateTime.Now))
        {
            return null;
        }

        var toRemover = user.Coin;
        user.RemoveCoin(toRemover);
        if (user.Subscriptions.Any())
        {
            foreach (var subscription in user.Subscriptions)
            {
                subscription.Disabled = true;
            }
        }

        user.Reimbursements.Add(new Reimbursement
        {
            User = user
        });

        user.PlanId = null;
        user.UserPlanSubscriptions.Clear();
        _userRepository.Update(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return unsubscribed;
        }

        Notificator.Handle("An error occurred while trying to update the user");
        return null;
    }

    public async Task<UserDto?> Subscribe(SubscribeDto dto)
    {
        var user = await _userRepository.GetById(_authenticatedUser.Id);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        var course = await _courseRepository.GetById(dto.CourseId);
        if (course is null)
        {
            Notificator.Handle("The course informed does not exist.");
            return null;
        }

        var professor = await _userRepository.GetByEmail(course.ProfessorEmail);
        if (professor is null)
        {
            Notificator.Handle("The professor of the course does not exist.");
            return null;
        }

        if (user.Coin < course.Value)
        {
            Notificator.Handle("You do not have enough coins to subscribe to this course.");
            return null;
        }

        _extractRepository.Create(new Extract
        {
            Value = course.Value,
            UserId = user.Id,
            ProfessorId = professor.Id,
            CourseId = course.Id,
            Type = (int)EExtractType.Entry
        });

        user.RemoveCoin(course.Value);
        if (!await Validate(user))
        {
            return null;
        }

        foreach (var @class in course.Modules.SelectMany(module =>
                     module.Classes.Where(@class => user.UserClasses.All(c => c.ClassId != @class.Id))))
        {
            user.UserClasses.Add(new UserClass
            {
                UserId = user.Id,
                ClassId = @class.Id
            });
        }

        _userRepository.Update(user);
        var subscription = user.Subscriptions.FirstOrDefault(s => s.UserId == user.Id && s.CourseId == course.Id);
        if (subscription is not null)
        {
            if (!subscription.Disabled)
            {
                Notificator.Handle("You are already subscribed to this course.");
                return null;
            }

            subscription.SubscriptionEnd = DateTime.Now.AddDays(2);
            subscription.Disabled = false;
            subscription.IsHide = false;
        }
        else
        {
            user.Subscriptions.Add(new Subscription { CourseId = course.Id, SubscriptionEnd = DateTime.Now.AddDays(2) });
        }

        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("An error occurred while subscribing to the course.");
        return null;
    }

    public async Task<UserDto?> HideSubscription(int id)
    {
        var userId = _authenticatedUser.Id;

        var user = await _userRepository.GetById(userId);

        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        var subscription = user.Subscriptions.FirstOrDefault(s => s.Id == id);

        if (subscription is not null)
        {
            if (subscription.IsHide)
            {
                Notificator.Handle("You are already hided that subscription.");
                return null;
            }

            subscription.IsHide = true;
        }

        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("An error occurred while subscribing to the course.");
        return null;
    }

    public async Task<UserDto?> Unsubscribe(UnsubscribeDto dto)
    {
        var user = await _userRepository.GetById(_authenticatedUser.Id);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        var course = await _courseRepository.GetById(dto.CourseId);
        if (course is null)
        {
            Notificator.Handle("The course informed does not exist.");
            return null;
        }

        var subscription = user.Subscriptions.FirstOrDefault(s => s.UserId == user.Id && s.CourseId == course.Id);
        if (subscription is null)
        {
            Notificator.Handle("You are not subscribed to this course.");
            return null;
        }
        
        subscription.Disabled = true;

        _userRepository.Update(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UserDto>(user);
        }

        Notificator.Handle("An error occurred while unsubscribing to the course.");
        return null;
    }

    public async Task<SubscriptionResponseDto?> Subscribe(SubscribePlanDto dto)
    {
        var user = await _userRepository.GetById(dto.UserId);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        if (user.PlanId is not null)
        {
            Notificator.Handle("The user already have a plan.");
            return null;
        }

        var plan = await _planRepository.GetById(dto.PlanId);
        if (plan is null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        var customers = await _customerService.GetByEmail(user.Email);
        if (customers is null)
        {
            return null;
        }
        
        var customer = customers.Any()
            ? customers.First()
            : await _customerService.Create(
                new CreateCustomerDto
                {
                    Email = user.Email,
                    Name = user.Name,
                    Phone = user.Phone,
                    CpfCnpj = user.Cpf
                });
        if (customer is null)
        {
            Notificator.Handle("An error occurred while getting the customer.");
            return null;
        }

        var subscription = await _subscriptionService.GetByCustomerId(customer.Id);
        if (subscription is not null && subscription.Data!.Any())
        {
            Notificator.Handle("You already have a subscription.");
            return null;
        }

        var newSubscription = await _subscriptionService.Create(new SubscriptionDto
        {
            Customer = customer.Id,
            BillingType = "CREDIT_CARD",
            Value = plan.Price,
            NextDueDate = DateTime.Now.AddDays(1).Date,
            Cycle = "MONTHLY",
            CreditCard = new CreditCard
            {
                HolderName = dto.CreditCard.HolderName.Trim(),
                Number = dto.CreditCard.Number.OnlyNumbers()?.Replace(" ", "").Trim(),
                ExpiryMonth = dto.CreditCard.ExpiryMonth.Trim(),
                ExpiryYear = dto.CreditCard.ExpiryYear.Trim(),
                Ccv = dto.CreditCard.Ccv.Trim()
            },
            CreditCardHolderInfo = new CreditCardHolderInfo
            {
                Name = dto.CreditCardHolderInfo.Name.Trim(),
                Email = dto.CreditCardHolderInfo.Email.Trim(),
                CpfCnpj = dto.CreditCardHolderInfo.CpfCnpj.OnlyNumbers()?.Trim(),
                PostalCode = dto.CreditCardHolderInfo.PostalCode.Trim(),
                AddressNumber = dto.CreditCardHolderInfo.AddressNumber.Trim(),
                Phone = dto.CreditCardHolderInfo.Phone.OnlyNumbers()?.Trim()
            }
        });

        if (newSubscription is null)
        {
            Notificator.Handle("An error occurred while creating the subscription.");
            return null;
        }

        user.PlanId = plan.Id;
        _userPlanSubscriptionRepository.Create(new UserPlanSubscription
        {
            PlanId = plan.Id,
            Plan = plan,
            UserId = user.Id,
            User = user,
            SubscriptionEnd = DateTime.Now.AddMonths(1)
        });

        _userRepository.Update(user);
        if (await _userRepository.UnitOfWork.Commit())
        {
            return newSubscription;
        }

        Notificator.Handle("An error occurred while updating the user.");
        return null;
    }

    public async Task MarkAsWatched(int classId)
    {
        var userId = _authenticatedUser.Id;
        var user = await _userRepository.GetById(userId);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return;
        }

        var @class = await _userClassRepository.GetById(classId, userId);
        if (@class is null)
        {
            Notificator.HandleNotFoundResourse();
            return;
        }

        _userClassRepository.MarkAsWatched(@class);
        if (await _userClassRepository.UnitOfWork.Commit())
        {
            return;
        }

        Notificator.Handle("An error occurred while marking the class as watched.");
    }


    public async Task MarkAsUnWatched(int classId)
    {
        var userId = _authenticatedUser.Id;
        var user = await _userRepository.GetById(userId);
        if (user is null)
        {
            Notificator.HandleNotFoundResourse();
            return;
        }

        var @class = await _userClassRepository.FirstOrDefault(c => c.UserId == userId && c.ClassId == classId);
        if (@class is null)
        {
            Notificator.HandleNotFoundResourse();
            return;
        }

        _userClassRepository.MarkAsUnwatched(@class);
        if (await _userClassRepository.UnitOfWork.Commit())
        {
            return;
        }

        Notificator.Handle("An error occurred while marking the class as watched.");
    }

    public async Task<DashboardAdminDto> DashboardAdmin()
    {
        var plans = await _planRepository.GetAll();
        if (!plans.Any()) return new DashboardAdminDto();
        var dashboard = new DashboardAdminDto
        {
            TotalReceiptsPerMonth = await _userPlanSubscriptionRepository.TotalReceiptsPerMonth(),
            Subscribes = await _userPlanSubscriptionRepository.Count(c => true),
            Reimbursement = await _reimbursement.Count(c => true)
        };
        foreach (var plan in plans)
        {
            dashboard.TotalReceiptPlansPerMonth.Add(new ReceiptPlanDto
            {
                PlanId = plan.Id,
                Name = plan.Name,
                Subscribes = await _userPlanSubscriptionRepository.Count(c => c.PlanId == plan.Id),
                Receipts = await _userPlanSubscriptionRepository.TotalReceiptPlanPerMonth(plan.Id)
            });
        }

        return dashboard;
    }

    public async Task<DashboardProfessorDto> DashboardProfessor()
    {
        var receipts = await _scheduledpaymentRepository.TotalReceipt(_authenticatedUser.Email);
        return new DashboardProfessorDto
        {
            Subscribes = await _courseRepository.Count(c =>
                c.Subscriptions.Any(s => s.Disabled == false && s.Course.ProfessorEmail == _authenticatedUser.Email)),
            Unsubscribes = await _courseRepository.Count(c =>
                c.Subscriptions.Any(s => s.Disabled == true && s.Course.ProfessorEmail == _authenticatedUser.Email)),
            TotalReceipt = receipts.TotalReceipt,
            ToReceive = receipts.ToReceive,
            Received = receipts.Received
        };
    }

    public async Task<string?> GenerateReceiptsPdf()
    {
        var receipts = await _extractReceiptRepository.GetAll();
        var plans = await _planRepository.GetAll();
        var receiptsGrouped = receipts.OrderByDescending(c => c.CreateAt)
            .GroupBy(e => new { e.CreateAt.Month, e.CreateAt.Year }).ToList();

        var pxPerMm = 72 / 25.2f;
        var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        var pdf = new Document(PageSize.A4, 15 * pxPerMm, 15 * pxPerMm, 15 * pxPerMm, 20 * pxPerMm);
        var archiver = new MemoryStream();
        var writer = PdfWriter.GetInstance(pdf, archiver);
        pdf.Open();

        var paragraphFont = new Font(baseFont, 32, Font.NORMAL, BaseColor.Black);
        var title = new Paragraph("Relatório de receita\n\n", paragraphFont)
        {
            Alignment = Element.ALIGN_LEFT,
            SpacingAfter = 4
        };

        pdf.Add(title);
        var logo = await _fileService.GetDocument("5aab47e8-b1ee-434f-bf5c-1ab6a18076e4.jpg");
        if (logo is not null)
        {
            var image = Image.GetInstance(logo);
            var heigthWidthRatio = image.Width / image.Height;
            var imageHeight = 32;
            var imageWidth = imageHeight * heigthWidthRatio;
            image.ScaleAbsolute(imageWidth, imageHeight);
            var marginLeft = pdf.PageSize.Width - pdf.RightMargin - imageWidth;
            var marginTop = pdf.PageSize.Height - pdf.TopMargin - 54;
            image.SetAbsolutePosition(marginLeft, marginTop);
            writer.DirectContent.AddImage(image, false);
        }

        var table = new PdfPTable(plans.Count + 1)
        {
            DefaultCell =
            {
                BorderWidth = 0
            },
            WidthPercentage = 100
        };

        CreateCellText(baseFont, table, "Meses", Element.ALIGN_CENTER);
        foreach (var plan in plans)
        {
            CreateCellText(baseFont, table, plan.Name, Element.ALIGN_CENTER);
        }

        foreach (var receiptGrouped in receiptsGrouped)
        {
            CreateCellText(baseFont, table, $"{receiptGrouped.Key.Month:00}/{receiptGrouped.Key.Year}",
                Element.ALIGN_CENTER);
            foreach (var plan in plans)
            {
                var total = receiptGrouped.Where(c => c.PlanId == plan.Id).Sum(c => c.Value);
                CreateCellText(baseFont, table, total.ToString("C"), Element.ALIGN_CENTER);
            }
        }

        pdf.Add(table);
        pdf.Close();
        var path = await _fileService.UploadFile(archiver);
        if (path is null)
        {
            return null;
        }

        archiver.Close();
        return path;
    }

    public async Task<string?> GenerateProfessorsPdf()
    {
        var professors = await _userRepository.Search(c => c.IsProfessor == true);

        var pxPerMm = 72 / 25.2f;
        var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        var pdf = new Document(PageSize.A4, 15 * pxPerMm, 15 * pxPerMm, 15 * pxPerMm, 20 * pxPerMm);
        var archiver = new MemoryStream();
        var writer = PdfWriter.GetInstance(pdf, archiver);
        pdf.Open();

        var paragraphFont = new Font(baseFont, 32, Font.NORMAL, BaseColor.Black);
        var title = new Paragraph("Relatório de Professores\n\n", paragraphFont)
        {
            Alignment = Element.ALIGN_LEFT,
            SpacingAfter = 4
        };

        pdf.Add(title);
        var logo = await _fileService.GetDocument("5aab47e8-b1ee-434f-bf5c-1ab6a18076e4.jpg");
        if (logo is not null)
        {
            var image = Image.GetInstance(logo);
            var heigthWidthRatio = image.Width / image.Height;
            var imageHeight = 32;
            var imageWidth = imageHeight * heigthWidthRatio;
            image.ScaleAbsolute(imageWidth, imageHeight);
            var marginLeft = pdf.PageSize.Width - pdf.RightMargin - imageWidth;
            var marginTop = pdf.PageSize.Height - pdf.TopMargin - 54;
            image.SetAbsolutePosition(marginLeft, marginTop);
            writer.DirectContent.AddImage(image, false);
        }

        var table = new PdfPTable(3)
        {
            DefaultCell =
            {
                BorderWidth = 0
            },
            WidthPercentage = 100
        };

        CreateCellText(baseFont, table, "Professor", Element.ALIGN_CENTER);
        CreateCellText(baseFont, table, "Quantidade de cursos ativos", Element.ALIGN_CENTER);
        CreateCellText(baseFont, table, "Quantidade de cursos inativos", Element.ALIGN_CENTER);
        foreach (var professor in professors)
        {
            CreateCellText(baseFont, table, professor.Name, Element.ALIGN_CENTER);
            CreateCellText(baseFont, table,
                _courseRepository.Count(c => c.ProfessorEmail == professor.Email && c.Disabled == false).Result
                    .ToString(), Element.ALIGN_CENTER);
            CreateCellText(baseFont, table,
                _courseRepository.Count(c => c.ProfessorEmail == professor.Email && c.Disabled == true).Result
                    .ToString(), Element.ALIGN_CENTER);
        }

        pdf.Add(table);
        pdf.Close();
        var path = await _fileService.UploadFile(archiver);
        if (path is null)
        {
            return null;
        }

        archiver.Close();
        return path;
    }

    public async Task<string?> GenerateProfessorsReceiptPdf()
    {
        var professors = await _userRepository.Search(c => c.IsProfessor == true);
        var scheduledPayments = await _scheduledpaymentRepository.GetAll();

        var pxPerMm = 72 / 25.2f;
        var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        var pdf = new Document(PageSize.A4, 15 * pxPerMm, 15 * pxPerMm, 15 * pxPerMm, 20 * pxPerMm);
        var archiver = new MemoryStream();
        var writer = PdfWriter.GetInstance(pdf, archiver);
        pdf.Open();

        var paragraphFont = new Font(baseFont, 24, Font.NORMAL, BaseColor.Black);
        var title = new Paragraph("Relatório de receita por professor\n\n", paragraphFont)
        {
            Alignment = Element.ALIGN_LEFT,
            SpacingAfter = 4
        };

        pdf.Add(title);
        var logo = await _fileService.GetDocument("5aab47e8-b1ee-434f-bf5c-1ab6a18076e4.jpg");
        {
            var image = Image.GetInstance(logo);
            var heigthWidthRatio = image.Width / image.Height;
            var imageHeight = 32;
            var imageWidth = imageHeight * heigthWidthRatio;
            image.ScaleAbsolute(imageWidth, imageHeight);
            var marginLeft = pdf.PageSize.Width - pdf.RightMargin - imageWidth;
            var marginTop = pdf.PageSize.Height - pdf.TopMargin - 54;
            image.SetAbsolutePosition(marginLeft, marginTop);
            writer.DirectContent.AddImage(image, false);
        }

        var table = new PdfPTable(4)
        {
            DefaultCell =
            {
                BorderWidth = 0
            },
            WidthPercentage = 100
        };

        CreateCellText(baseFont, table, "Professor", Element.ALIGN_CENTER);
        CreateCellText(baseFont, table, "Email", Element.ALIGN_CENTER);
        CreateCellText(baseFont, table, "Saldo em aberto", Element.ALIGN_CENTER);
        CreateCellText(baseFont, table, "Últimos 30 dias", Element.ALIGN_CENTER);
        foreach (var professor in professors)
        {
            CreateCellText(baseFont, table, professor.Name, Element.ALIGN_CENTER);
            CreateCellText(baseFont, table, professor.Email, Element.ALIGN_CENTER);
            CreateCellText(baseFont, table,
                scheduledPayments
                    .Where(c => c.User.Email == professor.Email && c.PaidOut == false && c.Disabled == false)
                    .Sum(c => c.Value)
                    .ToString("C"), Element.ALIGN_CENTER);
            CreateCellText(baseFont, table,
                scheduledPayments
                    .Where(c => c.User.Email == professor.Email && c.PaidOut && c.Disabled == false &&
                                c.CreateAt >= DateTime.Now.AddDays(-30))
                    .Sum(c => c.Value).ToString("C"), Element.ALIGN_CENTER);
        }

        pdf.Add(table);
        pdf.Close();
        var path = await _fileService.UploadFile(archiver);
        if (path is null)
        {
            return null;
        }

        archiver.Close();
        return path;
    }

    public async Task<string?> GenerateProfessorMonthlyReceiptPdf()
    {
        var userId = _authenticatedUser.Id;
        if (!_authenticatedUser.IsProfessorUser)
        {
            Notificator.Handle("You are not a professor.");
            return null;
        }

        var scheduledPayments = await _scheduledpaymentRepository.Search(c =>
            c.UserId == userId && c.Disabled == false && c.CreateAt >= DateTime.Now.AddDays(-30));
        var scheduledPaymentsGrouped = scheduledPayments.GroupBy(c => c.CourseId).ToList();

        var pxPerMm = 72 / 25.2f;
        var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        var pdf = new Document(PageSize.A4, 15 * pxPerMm, 15 * pxPerMm, 15 * pxPerMm, 20 * pxPerMm);
        var archiver = new MemoryStream();
        var writer = PdfWriter.GetInstance(pdf, archiver);
        pdf.Open();

        var paragraphFont = new Font(baseFont, 32, Font.NORMAL, BaseColor.Black);
        var title = new Paragraph("Relatório de receita\n\n", paragraphFont)
        {
            Alignment = Element.ALIGN_LEFT,
            SpacingAfter = 4
        };

        pdf.Add(title);
        var logo = await _fileService.GetDocument("5aab47e8-b1ee-434f-bf5c-1ab6a18076e4.jpg");
        if (logo is not null)
        {
            var image = Image.GetInstance(logo);
            var heigthWidthRatio = image.Width / image.Height;
            var imageHeight = 24;
            var imageWidth = imageHeight * heigthWidthRatio;
            image.ScaleAbsolute(imageWidth, imageHeight);
            var marginLeft = pdf.PageSize.Width - pdf.RightMargin - imageWidth;
            var marginTop = pdf.PageSize.Height - pdf.TopMargin - 54;
            image.SetAbsolutePosition(marginLeft, marginTop);
            writer.DirectContent.AddImage(image, false);
        }

        var table = new PdfPTable(4)
        {
            DefaultCell =
            {
                BorderWidth = 0
            },
            WidthPercentage = 100
        };

        CreateCellText(baseFont, table, "Curso", Element.ALIGN_CENTER);
        CreateCellText(baseFont, table, "Faturamento total", Element.ALIGN_CENTER);
        CreateCellText(baseFont, table, "Recebidos", Element.ALIGN_CENTER);
        CreateCellText(baseFont, table, "Disponíveis", Element.ALIGN_CENTER);
        foreach (var scheduledPayment in scheduledPaymentsGrouped)
        {
            var course = await _courseRepository.GetById(scheduledPayment.Key);
            if (course is null)
            {
                Notificator.Handle($"The course with id = {scheduledPayment.Key} do not exist.");
                return null;
            }

            CreateCellText(baseFont, table, course.Name, Element.ALIGN_CENTER);
            CreateCellText(baseFont, table, scheduledPayment.Sum(c => c.Value).ToString("C"), Element.ALIGN_CENTER);
            CreateCellText(baseFont, table, scheduledPayment.Where(c => c.PaidOut).Sum(c => c.Value).ToString("C"),
                Element.ALIGN_CENTER);
            CreateCellText(baseFont, table,
                scheduledPayment.Where(c => c.PaidOut == false && c.Disabled == false).Sum(c => c.Value).ToString("C"),
                Element.ALIGN_CENTER);
        }

        pdf.Add(table);
        pdf.Close();
        var path = await _fileService.UploadFile(archiver);
        if (path is null)
        {
            return null;
        }

        archiver.Close();
        return path;
    }

    public async Task GenerateCourseBilling()
    {
        _logger.LogInformation($"{DateTime.UtcNow} - CourseBillingBackgroundJob is initialized");
        var subscriptions =
            await _subscriptionRepository.Search(s => s.SubscriptionEnd <= DateTime.Now && s.Disabled == false);
        if (!subscriptions.Any())
        {
            _logger.LogInformation($"{DateTime.UtcNow} - There are no subscriptions to be billed");
            return;
        }

        _logger.LogInformation($"{DateTime.UtcNow} - There are {subscriptions.Count} subscriptions to be billed");
        _logger.LogInformation($"{DateTime.UtcNow} - Billing process started...");
        foreach (var subscription in subscriptions)
        {
            var user = await _userRepository.GetById(subscription.UserId);
            if (user is null)
            {
                _logger.LogError($"{DateTime.UtcNow} - User with Id {subscription.UserId} not found");
                _logger.LogInformation($"{DateTime.UtcNow} - Billing process finished with errors");
                return;
            }

            var course = await _courseRepository.GetById(subscription.CourseId);
            if (course is null)
            {
                _logger.LogError($"{DateTime.UtcNow} - Course with Id {subscription.CourseId} not found");
                _logger.LogInformation($"{DateTime.UtcNow} - Billing process finished with errors");
                return;
            }

            if (user.Coin < course.Value)
            {
                var userSubscription = user.Subscriptions.First(s => s.UserId == user.Id && s.CourseId == course.Id);
                userSubscription.Disabled = true;
                _userRepository.Update(user);
                continue;
            }

            user.RemoveCoin(course.Value);
            _userRepository.Update(user);
        }

        if (await _userRepository.UnitOfWork.Commit())
        {
            _logger.LogInformation($"{DateTime.UtcNow} - Billing process finished successfully");
            return;
        }

        _logger.LogError($"{DateTime.UtcNow} - An error occurred while trying to save the changes");
        _logger.LogInformation($"{DateTime.UtcNow} - Billing process finished with errors");
    }

    public async Task GenerateScheduledPayment()
    {
        _logger.LogInformation($"{DateTime.UtcNow} - ScheduledPaymentBackgroundJob is initialized");
        if (DateTime.Now.Day != 20)
        {
            _logger.LogInformation(
                $"{DateTime.UtcNow} - ScheduledPaymentBackgroundJob is programmed to run on the 20th of each month");
            _logger.LogInformation($"{DateTime.UtcNow} - ScheduledPayment process finished without errors");
            return;
        }

        var extracts = await _extractRepository.Search(c =>
            c.CreateAt >= DateTime.Now.AddDays(-30) && c.Type == (int)EExtractType.Entry && c.Disabled == false);
        if (!extracts.Any())
        {
            _logger.LogInformation($"{DateTime.UtcNow} - There are no extracts to be create ScheduledPayment");
            return;
        }

        var extractsAgrouped = extracts.GroupBy(c => c.CourseId).ToList();
        _logger.LogInformation(
            $"{DateTime.UtcNow} - There are {extracts.Count} extracts to be create ScheduledPayment");
        _logger.LogInformation($"{DateTime.UtcNow} - ScheduledPayment process started...");

        foreach (var extractAgrouped in extractsAgrouped)
        {
            var course = await _courseRepository.GetById(extractAgrouped.Key);
            if (course is null)
            {
                _logger.LogError($"{DateTime.UtcNow} - Course with Id {extractAgrouped.Key} not found");
                _logger.LogInformation($"{DateTime.UtcNow} - ScheduledPayment process finished with errors");
                return;
            }

            var professor = await _userRepository.GetByEmail(course.ProfessorEmail);
            if (professor is null)
            {
                _logger.LogError($"{DateTime.UtcNow} - Professor with email {course.ProfessorEmail} not found");
                _logger.LogInformation($"{DateTime.UtcNow} - ScheduledPayment process finished with errors");
                return;
            }

            extractAgrouped.ToList().ForEach(c => _extractRepository.Disable(c));
            var scheduledPayment = new Scheduledpayment
            {
                CourseId = course.Id,
                UserId = professor.Id,
                Value = extractAgrouped.Sum(c => c.Course.Value)
            };

            _scheduledpaymentRepository.Create(scheduledPayment);
        }

        if (await _scheduledpaymentRepository.UnitOfWork.Commit())
        {
            _logger.LogInformation($"{DateTime.UtcNow} - ScheduledPayment process finished successfully");
            return;
        }

        _logger.LogError($"{DateTime.UtcNow} - An error occurred while trying to save the changes");
        _logger.LogInformation($"{DateTime.UtcNow} - ScheduledPayment process finished with errors");
    }

    private async Task<bool> Validate(User user)
    {
        if (!user.Validate(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        if (await _userRepository.Any(c => c.Id != user.Id && (c.Cpf == user.Cpf || c.Email == user.Email || c.Phone == user.Phone)))
        {
            Notificator.Handle(
                $"There is already an {(user.Disabled ? "disabled" : "activated")} user registered with one or more of these identifications.");
        }

        if (user.PlanId is null) return !Notificator.HasNotification;
        {
            var planExists = await _planRepository.FirstOrDefault(c => c.Id == user.PlanId);
            if (planExists is null)
            {
                Notificator.Handle("The plan informed does not exist.");
            }

            if (planExists is { Disabled: true })
            {
                Notificator.Handle("The plan informed is disabled.");
            }
        }

        return !Notificator.HasNotification;
    }

    private void CreateCellText(BaseFont baseFont, PdfPTable table, string text,
        int horizontalAligment = Element.ALIGN_LEFT, bool negrito = false, bool italic = false, int fontSize = 12,
        int cellHeigth = 25)
    {
        var style = Font.NORMAL;
        switch (negrito)
        {
            case true when italic:
                style = Font.BOLDITALIC;
                break;
            case true:
                style = Font.BOLD;
                break;
            default:
            {
                if (italic)
                {
                    style = Font.ITALIC;
                }

                break;
            }
        }

        var cellFont = new Font(baseFont, fontSize, style, BaseColor.Black);
        var bgColor = BaseColor.White;
        if (table.Rows.Count % 2 == 1)
        {
            bgColor = new BaseColor(0.95F, 0.95F, 0.95F);
        }

        var cell = new PdfPCell(new Phrase(text, cellFont))
        {
            HorizontalAlignment = horizontalAligment,
            VerticalAlignment = Element.ALIGN_MIDDLE,
            Border = 0,
            BorderWidthBottom = 1,
            FixedHeight = cellHeigth,
            PaddingBottom = 5,
            BackgroundColor = bgColor
        };

        table.AddCell(cell);
    }
}