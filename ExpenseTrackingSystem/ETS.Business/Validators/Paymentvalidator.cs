using ETS.Schema;
using FluentValidation;

namespace ETS.Business.Validators;
public class CreatePaymentValidator : AbstractValidator<PaymentRequest>
{
    public CreatePaymentValidator()
    {
        RuleFor(x => x.ExpenseId).NotEmpty().WithMessage("ExpenseId cannot be empty.");
        RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Amount must be greater than 0.");
        RuleFor(x => x.PaymentDate).NotEmpty().WithMessage("PaymentDate cannot be empty.");
        RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("PaymentMethod cannot be empty.");
    }
}
