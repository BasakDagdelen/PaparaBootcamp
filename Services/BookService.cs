using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Interfaces;

namespace Patikadev_RestfulApi.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _context;
    public BookService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Book> CreateBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null)
            return false;

        book.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        var book =  await _context.Books.Where(p => p.Id == id && p.IsActive).FirstOrDefaultAsync();
        if(book is null)
            return null;

        return book;
    }

    public async Task<Book?> UpdateBookAsync(Guid id, Book updatedBook)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null)
            return null;

        book.Name = updatedBook.Name;
        book.Author = updatedBook.Author;
        book.Price = updatedBook.Price;
        book.Description = updatedBook.Description;
        book.IsActive = updatedBook.IsActive;
        book.Image = updatedBook.Image;

        await _context.SaveChangesAsync();
        return book;
    }
}
