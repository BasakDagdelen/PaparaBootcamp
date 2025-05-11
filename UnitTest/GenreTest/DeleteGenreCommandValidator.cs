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

public class DeleteGenreCommandValidator
{
    private readonly GetByIdValidator _validator;

    public DeleteGenreCommandValidator()
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
