using FluentValidation;
using Patikadev_RestfulApi.DTO.Request;

namespace Patikadev_RestfulApi.Services.Validations;

public class GetByIdValidator : AbstractValidator<GetByIdRequest>
{
    public GetByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID cannot be empty.");
    }
}
