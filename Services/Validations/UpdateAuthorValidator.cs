using FluentValidation;
using Patikadev_RestfulApi.DTO.Request;

namespace Patikadev_RestfulApi.Services.Validations;

public class UpdateAuthorValidator : AbstractValidator<AuthorRequest>
{
    public UpdateAuthorValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("First name is required.")
                             .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.Surname).NotEmpty().WithMessage("First name is required.")
                             .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.BirthDate).NotEmpty().WithMessage("Birth date is required.")
                             .LessThan(DateTime.Now).WithMessage("Birth date must be in the past.");
    }
}

