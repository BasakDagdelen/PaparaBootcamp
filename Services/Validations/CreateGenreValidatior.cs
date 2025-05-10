using FluentValidation;
using Patikadev_RestfulApi.DTO.Request;

namespace Patikadev_RestfulApi.Services.Validations;

public class CreateGenreValidatior: AbstractValidator<GenreRequest>
{
    public CreateGenreValidatior()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Genre name cannot be empty.")
            .MinimumLength(3).WithMessage("Genre name must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Genre name must not exceed 50 characters.");
    }
}
