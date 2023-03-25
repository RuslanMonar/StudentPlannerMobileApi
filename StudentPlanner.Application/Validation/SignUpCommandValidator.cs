
using FluentValidation;
using StudentPlanner.Application.Commands;

namespace StudentPlanner.Application.Validation;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email address is required and must be a valid email.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required aaaaaaaaaaaaaaaaaaaaaaaaaa");
    }
}