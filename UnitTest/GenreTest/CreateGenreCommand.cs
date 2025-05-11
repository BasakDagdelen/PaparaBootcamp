using Microsoft.EntityFrameworkCore;
using Moq;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Services;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.GenreTest;


public class CreateGenreCommand
{
    private readonly GenreService _genreService;
    private readonly Mock<AppDbContext> _mockContext;
    private readonly Mock<DbSet<Genre>> _mockSet;

    public CreateGenreCommand()
    {
        _mockContext = new Mock<AppDbContext>();
        _mockSet = new Mock<DbSet<Genre>>();
        _mockContext.Setup(m => m.Genres).Returns(_mockSet.Object);
        _genreService = new GenreService(_mockContext.Object);
    }

    [Fact]
    public async Task CreateGenreAsync_Should_Add_New_Genre()
    {
        var genre = new Genre
        {
            Id = Guid.NewGuid(),
            Name = "Fantasy",
            IsActive = true
        };

        var result = await _genreService.CreateGenreAsync(genre);

        _mockSet.Verify(m => m.Add(It.IsAny<Genre>()), Times.Once);
        _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(genre, result);
    }
}
