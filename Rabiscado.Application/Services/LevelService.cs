using AutoMapper;
using Rabiscado.Application.Contracts;
using Rabiscado.Application.Dtos.V1.Level;
using Rabiscado.Application.Notifications;
using Rabiscado.Domain.Contracts.Repositories;

namespace Rabiscado.Application.Services;

public class LevelService : BaseService, ILevelService
{
    private readonly ILevelRepository _levelRepository;
    public LevelService(IMapper mapper, INotificator notificator, ILevelRepository levelRepository) : base(mapper, notificator)
    {
        _levelRepository = levelRepository;
    }

    public async Task<LevelDto?> GetById(int id)
    {
        var level = await _levelRepository.GetById(id);
        if(level is not null) return Mapper.Map<LevelDto>(level);
        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<List<LevelDto>> GetAll()
    {
        var levels = await _levelRepository.GetAll();
        return Mapper.Map<List<LevelDto>>(levels);
    }
}