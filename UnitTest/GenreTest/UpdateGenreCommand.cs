using Microsoft.EntityFrameworkCore;
using Moq;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.GenreTest;

public class UpdateGenreCommand
{
    private readonly GenreService _genreService;
    private readonly Mock<AppDbContext> _mockContext;
    private readonly Mock<DbSet<Genre>> _mockSet;

    public UpdateGenreCommand()
    {
        _mockContext = new Mock<AppDbContext>();
        _mockSet = new Mock<DbSet<Genre>>();
        _mockContext.Setup(m => m.Genres).Returns(_mockSet.Object);
        _genreService = new GenreService(_mockContext.Object);
    }

    [Fact]
    public async Task UpdateGenreAsync_Should_Return_Null_When_Genre_Not_Found()
    {
        var result = await _genreService.UpdateGenreAsync(Guid.NewGuid(), new Genre());
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateGenreAsync_Should_Update_Genre_When_Exists()
    {
        var genre = new Genre { Id = Guid.NewGuid(), Name = "Old Name", IsActive = true };
        _mockSet.Setup(m => m.FindAsync(It.IsAny<Guid>())).ReturnsAsync(genre);

        var updatedGenre = new Genre { Name = "Updated Name", IsActive = false };
        var result = await _genreService.UpdateGenreAsync(genre.Id, updatedGenre);

        Assert.NotNull(result);
        Assert.Equal("Updated Name", result.Name);
        Assert.False(result.IsActive);
    }
}
