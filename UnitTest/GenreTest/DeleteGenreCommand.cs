using Microsoft.EntityFrameworkCore;
using Moq;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Services;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.GenreTest;

public class DeleteGenreCommand
{
    private readonly GenreService _genreService;
    private readonly Mock<AppDbContext> _mockContext;
    private readonly Mock<DbSet<Genre>> _mockSet;

    public DeleteGenreCommand()
    {
        _mockContext = new Mock<AppDbContext>();
        _mockSet = new Mock<DbSet<Genre>>();
        _mockContext.Setup(m => m.Genres).Returns(_mockSet.Object);
        _genreService = new GenreService(_mockContext.Object);
    }

    [Fact]
    public async Task DeleteGenreAsync_Should_Return_False_When_Genre_Not_Found()
    {
        var result = await _genreService.DeleteGenreAsync(Guid.NewGuid());
        Assert.False(result);
    }

    [Fact]
    public async Task DeleteGenreAsync_Should_Mark_As_Inactive_When_Genre_Exists()
    {
        var genre = new Genre { Id = Guid.NewGuid(), IsActive = true };
        _mockSet.Setup(m => m.FindAsync(It.IsAny<Guid>())).ReturnsAsync(genre);

        var result = await _genreService.DeleteGenreAsync(genre.Id);

        Assert.True(result);
        Assert.False(genre.IsActive);
    }
}
