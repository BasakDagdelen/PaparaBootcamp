using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Services;
using Xunit;

namespace Patikadev_RestfulApi.UnitTest.BookTests;

public class UpdateBookCommand
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task UpdateBookAsync_BookExists_ShouldUpdateFields()
    {
        var context = GetInMemoryDbContext();
        var service = new BookService(context);

        var existingBook = new Book
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

        context.Books.Add(existingBook);
        await context.SaveChangesAsync();

        var updatedBook = new Book
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


        var result = await service.UpdateBookAsync(existingBook.Id, updatedBook);

        Assert.NotNull(result);
        Assert.Equal("New Name", result.Name);
        Assert.Equal("New Author", result.Author);
        Assert.Equal(20, result.Price);
        Assert.Equal("New Desc", result.Description);
        Assert.False(result.IsActive);
        Assert.Equal("new.jpg", result.Image);
    }

    [Fact]
    public async Task UpdateBookAsync_BookDoesNotExist_ShouldReturnNull()
    {
        var context = GetInMemoryDbContext();
        var service = new BookService(context);

        var updatedBook = new Book
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

        // Act
        var result = await service.UpdateBookAsync(Guid.NewGuid(), updatedBook);

        // Assert
        Assert.Null(result);
    }
}
