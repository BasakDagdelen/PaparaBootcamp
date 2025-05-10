using FluentValidation;
using Patikadev_RestfulApi.DTO;

namespace Patikadev_RestfulApi.Services.Validations;

public class UpdateBookValidator : AbstractValidator<BookRequest>
{
    public UpdateBookValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Book name cannot be empty")
            .MinimumLength(3).WithMessage("Book name must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Book name must not exceed 100 characters");

        RuleFor(x => x.Author).NotEmpty().WithMessage("Author cannot be empty")
            .MinimumLength(3).WithMessage("Author must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Author must not exceed 100 characters");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description cannot be empty")
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.Price).NotEmpty().WithMessage("Price cannot be empty")
            .GreaterThan(0).WithMessage("Price must be greater than 0");

        RuleFor(x => x.Image).NotEmpty().WithMessage("Iamge cannot be empty")
             .MaximumLength(1000).WithMessage("Image must not exceed 1000 characters");

        RuleFor(x => x.IsActive).NotEmpty().WithMessage("IsActive cannot be empty")
            .Must(x => x == true || x == false).WithMessage("IsActive must be true or false");

    }
}
