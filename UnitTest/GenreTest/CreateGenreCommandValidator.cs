using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.DTO.Request;
using Patikadev_RestfulApi.Services.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.GenreTest;

public class CreateGenreCommandValidator
{
    private readonly CreateGenreValidatior _validator;

    public CreateGenreCommandValidator()
    {
        _validator = new CreateGenreValidatior();
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var model = new GenreRequest { Name = "" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Name_Is_Valid()
    {
        var model = new GenreRequest { Name = "Fantasy" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Exceeds_Max_Length()
    {
        var model = new GenreRequest { Name = new string('a', 51) };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}
