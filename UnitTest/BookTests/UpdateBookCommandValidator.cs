using Patikadev_RestfulApi.DTO.Request;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.BookTests;

public class UpdateBookCommandValidator
{
    private readonly UpdateBookCommandValidator _validator;

    public UpdateBookCommandValidator()
    {
        _validator = new UpdateBookCommandValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new BookRequest { Name = "" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Too_Short()
    {
        var model = new BookRequest { Name = "AB" };  // Name is too short
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Empty()
    {
        var model = new BookRequest { Description = "" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Description_Is_Too_Long()
    {
        var model = new BookRequest { Description = new string('x', 1001) }; // Description exceeds max length
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Should_Have_Error_When_Price_Is_Zero()
    {
        var model = new BookRequest { Price = 0 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Should_Have_Error_When_Image_Is_Empty()
    {
        var model = new BookRequest { Image = "" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Image);
    }

    [Fact]
    public void Should_Pass_Validation_When_Model_Is_Valid()
    {
        var model = new BookRequest
        {
            Name = "Clean Code",
            Description = "A book about writing clean software",
            Price = 45,
            Image = "https://example.com/image.jpg"
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
