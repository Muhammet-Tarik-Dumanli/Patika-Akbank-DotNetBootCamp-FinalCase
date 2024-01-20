using ETS.Schema;
using FluentValidation;

namespace ETS.Business.Validators;
public class CreateExpenseCategoryValidator : AbstractValidator<ExpenseCategoryRequest>
{
    public CreateExpenseCategoryValidator()
    {
        RuleFor(x => x.CategoryName).NotEmpty().MinimumLength(3).WithMessage("Category Name cannot be empty and must be 3 characters.");
        RuleFor(x => x.Description).NotEmpty().MinimumLength(3).WithMessage("Description cannot be empty and must be 3 characters.");
    }
}
