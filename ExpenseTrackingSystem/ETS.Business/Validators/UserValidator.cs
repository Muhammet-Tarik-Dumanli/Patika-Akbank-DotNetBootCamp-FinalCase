using ETS.Schema;
using FluentValidation;

namespace ETS.Business.Validators;
public class CreateUserValidator : AbstractValidator<UserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50).WithMessage("First name cannot be empty.");
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(50).WithMessage("Last name cannot be empty.");
        RuleFor(x => x.UserName).NotEmpty().WithMessage("Username cannot be empty.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        RuleFor(x => x.Role).NotEmpty().WithMessage("Role cannot be empty.");
        RuleFor(x => x.IBAN).NotEmpty().WithMessage("IBAN cannot be empty.");
    }
}
