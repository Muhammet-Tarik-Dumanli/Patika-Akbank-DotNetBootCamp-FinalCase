using AutoMapper;
using ETS.Data.Entity;
using ETS.Data.Enums;
using ETS.Schema;

namespace ETS.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();

        // Payment
        CreateMap<PaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>();

        // Expense
        CreateMap<ExpenseRequest, Expense>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ExpenseStatus.Request));
        CreateMap<Expense, ExpenseResponse>();

        // ExpenseCategory
        CreateMap<ExpenseCategoryRequest, ExpenseCategory>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));


        CreateMap<ExpenseCategory, ExpenseCategoryResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}