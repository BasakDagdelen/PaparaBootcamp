using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Services;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.BookTests;

public class GetBookDetailQuery
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetBookByIdAsync_BookExistsAndIsActive_ShouldReturnBook()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new BookService(context);

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Name = "Test Book",
            Author = new Author
            {
                Id = Guid.NewGuid(),
                Name = "Test Author",
                Surname = "Test Surname"
            },
            Description = "Test Description",
            Image = "TestImage.jpg",
            Price = 9.99m,
            Genre = new Genre
            {
                Id = Guid.NewGuid(),
                Name = "Test Genre"
            },
            IsActive = true
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetBookByIdAsync(book.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
    }

    [Fact]
    public async Task GetBookByIdAsync_BookIsInactive_ShouldReturnNull()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new BookService(context);

        var book = new Book
        {
            Id = Guid.NewGuid(),
            Name = "Sample",
            IsActive = false
        };

        context.Books.Add(book);
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetBookByIdAsync(book.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetBookByIdAsync_BookDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new BookService(context);

        // Act
        var result = await service.GetBookByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}
