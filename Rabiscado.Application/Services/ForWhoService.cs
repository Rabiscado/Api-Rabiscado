using AutoMapper;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.ForWho;
using Rabiscado.Application.Notifications;
using Rabiscado.Domain.Contracts.Repositories;

namespace Rabiscado.Application.Services;

public class ForWhoService : BaseService, IForWhoService
{
    private readonly IForWhoRepository _forWhoRepository;
    public ForWhoService(IMapper mapper, INotificator notificator, IForWhoRepository forWhoRepository) : base(mapper, notificator)
    {
        _forWhoRepository = forWhoRepository;
    }

    public async Task<ForWhoDto?> GetById(int id)
    {
        var forWho = await _forWhoRepository.GetById(id);
        if(forWho is not null) return Mapper.Map<ForWhoDto>(forWho);
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<ForWhoDto>> GetAll()
    {
        var forWhos = await _forWhoRepository.GetAll();
        return Mapper.Map<List<ForWhoDto>>(forWhos);
    }
}