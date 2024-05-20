using AutoMapper;
using Domain.DTO_s.ClassSheduleDtos;
using Domain.DTO_s.MembershipDtos;
using Domain.DTO_s.PaymentDtos;
using Domain.DTO_s.TrainerDtos;
using Domain.DTO_s.UserDtos;
using Domain.DTO_s.WorkoutDtos;
using Domain.Models;

namespace Infrastructure.AutoMapper;

public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<User, AddUserDto>().ReverseMap();
        CreateMap<User, GetUsersDto>().ReverseMap();
        CreateMap<User, UpdateUserDto>().ReverseMap();
        
        
        CreateMap<Payment, AddPaymentDto>().ReverseMap();
        CreateMap<Payment, GetPaymentDto>().ReverseMap();
        CreateMap<Payment, UpdatePaymentDto>().ReverseMap();
        
        CreateMap<ClassSchedule, AddClassScheduleDto>().ReverseMap();
        CreateMap<ClassSchedule, GetClassSchedulesDto>().ReverseMap();
        CreateMap<ClassSchedule, UpdateClassScheduleDto>().ReverseMap();
        
        CreateMap<Membership, AddMembershipDto>().ReverseMap();
        CreateMap<Membership, GetMembershipsDto>().ReverseMap();
        CreateMap<Membership, UpdateMembershipDto>().ReverseMap();
        
        CreateMap<Trainer, AddTrainerDto>().ReverseMap();
        CreateMap<Trainer, GetTrainersDto>().ReverseMap();
        CreateMap<Trainer, UpdateTrainerDto>().ReverseMap();
        
        CreateMap<WorkOut, AddWorkoutDto>().ReverseMap();
        CreateMap<WorkOut, GetWorkoutsDto>().ReverseMap();
        CreateMap<WorkOut, UpdateWorkoutDto>().ReverseMap();


    }
}