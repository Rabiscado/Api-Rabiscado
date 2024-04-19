using AutoMapper;
using Rabiscado.Core.Extensions;
using Rabiscado.Domain.Contracts.Pagination;
using Rabiscado.Domain.Entities;
using Rabiscado.Domain.Pagination;

namespace Rabiscado.Application.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region User

        CreateMap<Dtos.V1.Users.UserDto, User>()
            .AfterMap((_, dest) => dest.Cpf = dest.Cpf.OnlyNumbers()!)
            .AfterMap((_, dest) => dest.Phone = dest.Phone.OnlyNumbers()!)
            .AfterMap((_, dest) => dest.Cep = dest.Cep.OnlyNumbers()!)
            .ReverseMap();

        CreateMap<Dtos.V1.Users.CreateUserDto, User>()
            .AfterMap((_, dest) => dest.Cpf = dest.Cpf.OnlyNumbers()!)
            .AfterMap((_, dest) => dest.Phone = dest.Phone.OnlyNumbers()!)
            .AfterMap((_, dest) => dest.Cep = dest.Cep.OnlyNumbers()!)
            .ReverseMap();
        
        CreateMap<Dtos.V1.Users.UpdateUserDto, User>()
            .AfterMap((_, dest) => dest.Cpf = dest.Cpf.OnlyNumbers()!)
            .AfterMap((_, dest) => dest.Phone = dest.Phone.OnlyNumbers()!)
            .AfterMap((_, dest) => dest.Cep = dest.Cep.OnlyNumbers()!)
            .ReverseMap();
        
        CreateMap<Dtos.V1.Users.UserDto, ResultPaginated<User>>().ReverseMap();
        CreateMap<Dtos.V1.Base.PagedDto<Dtos.V1.Users.UserDto>, IResultPaginated<User>>().ReverseMap();

        #endregion
        
        #region Plan
        
        CreateMap<Rabiscado.Application.Dtos.V1.Plan.PlanDto, Plan>().ReverseMap();
        CreateMap<Rabiscado.Application.Dtos.V1.Plan.CreatePlanDto, Plan>().ReverseMap();
        CreateMap<Rabiscado.Application.Dtos.V1.Plan.UpdatePlanDto, Plan>().ReverseMap();
        
        #endregion

        #region Course

        CreateMap<Dtos.V1.Courses.CourseDto, Course>().ReverseMap();
        CreateMap<Course, Dtos.V1.Courses.CourseDto>()
            .AfterMap((course, dest) => { dest.Subscribe = course.Subscriptions.Count; })
            .ReverseMap();
        CreateMap<Dtos.V1.Courses.CreateCourseDto, Course>().ReverseMap();
        CreateMap<Dtos.V1.Courses.UpdateCourseDto, Course>().ReverseMap();
        CreateMap<Dtos.V1.Courses.CourseDto, ResultPaginated<Course>>().ReverseMap();
        CreateMap<Dtos.V1.Base.PagedDto<Dtos.V1.Courses.CourseDto>, IResultPaginated<Course>>().ReverseMap();

        #endregion

        #region Level

        CreateMap<Rabiscado.Application.Dtos.V1.Level.LevelDto, Level>().ReverseMap();

        #endregion
        
        #region CourseLevel

        CreateMap <Dtos.V1.Courses.CourseLevelDto, CourseLevel>().ReverseMap();

        #endregion
        
        #region ForWho

        CreateMap<Rabiscado.Application.Dtos.V1.ForWho.ForWhoDto, ForWho>().ReverseMap();

        #endregion
        
        #region CourseForWho

        CreateMap<Dtos.V1.Courses.CourseForWhoDto, CourseForWho>().ReverseMap();

        #endregion
        
        #region Module

        CreateMap<Rabiscado.Application.Dtos.V1.Module.ModuleDto, Module>().ReverseMap();
        CreateMap<Rabiscado.Application.Dtos.V1.Module.CreateModuleDto, Module>().ReverseMap();
        CreateMap<Rabiscado.Application.Dtos.V1.Module.UpdateModuleDto, Module>().ReverseMap();

        #endregion
        
        #region Classe

        CreateMap<Rabiscado.Application.Dtos.V1.Class.ClassDto, Class>().ReverseMap();
        CreateMap<Rabiscado.Application.Dtos.V1.Class.CreateClassDto, Class>().ReverseMap();
        CreateMap<Rabiscado.Application.Dtos.V1.Class.UpdateClassDto, Class>().ReverseMap();

        #endregion
        
        #region Step

        CreateMap<Rabiscado.Application.Dtos.V1.Step.StepDto, Step>().ReverseMap();
        CreateMap<Rabiscado.Application.Dtos.V1.Step.CreateStepDto, Step>().ReverseMap();
        CreateMap<Rabiscado.Application.Dtos.V1.Step.UpdateStepDto, Step>().ReverseMap();

        #endregion

        #region Subscription

        CreateMap<Rabiscado.Application.Dtos.V1.Subscription.SubscriptionDto, Subscription>().ReverseMap();

        #endregion

        #region UserClass

        CreateMap<Rabiscado.Application.Dtos.V1.User.UserClassDTO, UserClass>().ReverseMap();

        #endregion
        
        #region Scheduledpayment

        CreateMap<Dtos.V1.Scheduledpayments.ScheduledpaymentDto, Scheduledpayment>().ReverseMap();
        CreateMap<Dtos.V1.Scheduledpayments.CreateScheduledpaymentDto, Scheduledpayment>().ReverseMap();
        CreateMap<Dtos.V1.Scheduledpayments.UpdateScheduledpaymentDto, Scheduledpayment>().ReverseMap();
        CreateMap<Dtos.V1.Scheduledpayments.ScheduledpaymentDto, ResultPaginated<Scheduledpayment>>().ReverseMap();
        CreateMap<Dtos.V1.Base.PagedDto<Dtos.V1.Scheduledpayments.ScheduledpaymentDto>, IResultPaginated<Scheduledpayment>>().ReverseMap();

        #endregion

        #region Extract

        CreateMap<Dtos.V1.Extracts.ExtractDto, Extract>().ReverseMap();
        CreateMap<Dtos.V1.Extracts.CreateExtractDto, Extract>().ReverseMap();
        CreateMap<Dtos.V1.Extracts.UpdateExtractDto, Extract>().ReverseMap();
        CreateMap<Dtos.V1.Base.PagedDto<Dtos.V1.Extracts.ExtractDto>, IResultPaginated<Extract>>().ReverseMap();

        #endregion

        #region UserPlanSubscription
        
        CreateMap<Dtos.V1.UserPlanSubscriptions.UserPlanSubscriptionDto, UserPlanSubscription>().ReverseMap();

        #endregion
    }
}