using Patikadev_RestfulApi.DTO.Request;
using Patikadev_RestfulApi.Services.Validations;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.BookTests;

public class GetBookDetailQueryValidator
{
    private readonly GetByIdValidator _validator;

    public GetBookDetailQueryValidator()
    {
        _validator = new GetByIdValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var model = new GetByIdRequest { Id = Guid.Empty }; 
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Id);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Id_Is_Valid()
    {
        var model = new GetByIdRequest { Id = Guid.NewGuid() };  
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Id);
    }
}
