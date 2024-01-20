using AutoMapper;
using ETS.Data.Entity;
using ETS.Data.Enums;
using ETS.Schema;

namespace ETS.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        //User
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();

        // Payment
        CreateMap<PaymentRequest, Payment>()
                 .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                 .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
                 .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                 .ForMember(dest => dest.ExpenseId, opt => opt.MapFrom(src => src.ExpenseId))
                 .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<Payment, PaymentResponse>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
            .ForMember(dest => dest.ExpenseId, opt => opt.MapFrom(src => src.ExpenseId));

        // Expense
        CreateMap<ExpenseRequest, Expense>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.ExpenseCategoryId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ExpenseStatus.Request))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<Expense, ExpenseResponse>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ExpenseDate, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.ExpenseCategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

        // ExpenseCategory
        CreateMap<ExpenseCategoryRequest, ExpenseCategory>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));

        CreateMap<ExpenseCategory, ExpenseCategoryResponse>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryName));
    }
}