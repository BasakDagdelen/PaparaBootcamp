using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Interfaces;

namespace Patikadev_RestfulApi.Services.Services;

public class AuthorService : IAuthorService
{
    private readonly AppDbContext _context;
    public AuthorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Author> CreateAuthorAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public async Task<bool> DeleteAuthorAsync(Guid id)
    {
        var author = await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        if (author.Books.Any())
            return false;

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _context.Authors.ToListAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(Guid id)
    {
        return await _context.Authors.Where(p => p.Id == id && p.IsActive).FirstOrDefaultAsync();
    }

    public async Task<Author?> UpdateAuthorAsync(Guid id, Author updatedAuthor)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author is null)
            return null;

        author.Name = updatedAuthor.Name;
        author.Surname = updatedAuthor.Surname;
        author.BirthDate = updatedAuthor.BirthDate;

        await _context.SaveChangesAsync();
        return author;
    }
}
