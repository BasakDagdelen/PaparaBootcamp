using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Services;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.BookTests;

public class DeleteBookCommand
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // her testte ayrı db
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task DeleteBookAsync_BookExists_ShouldSetIsActiveFalse()
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
        var result = await service.DeleteBookAsync(book.Id);

        // Assert
        var deletedBook = await context.Books.FindAsync(book.Id);
        Assert.True(result);
        Assert.False(deletedBook.IsActive);
    }

    [Fact]
    public async Task DeleteBookAsync_BookDoesNotExist_ShouldReturnFalse()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new BookService(context);

        // Act
        var result = await service.DeleteBookAsync(Guid.NewGuid());

        // Assert
        Assert.False(result);
    }
}
