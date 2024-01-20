using ETS.Schema;
using FluentValidation;

namespace ETS.Business.Validators;

public class CreateExpenseValidator : AbstractValidator<ExpenseRequest>
{
    public CreateExpenseValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId cannot be empty.");
        RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId cannot be empty.");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty.");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Location cannot be empty.");
    }
}
