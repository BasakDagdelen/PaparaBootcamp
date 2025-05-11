using Microsoft.EntityFrameworkCore;
using Patikadev_RestfulApi.Context;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.Services.Interfaces;

namespace Patikadev_RestfulApi.Services.Services;

public class GenreService : IGenreService
{
    private readonly AppDbContext _context;
    public GenreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Genre> CreateGenreAsync(Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return genre;
    }

    public async Task<bool> DeleteGenreAsync(Guid id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre is null)
            return false;

        genre.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Genre>> GetAllGenreAsync()
    {
        return await _context.Genres.ToListAsync();
    }

    public async Task<Genre?> GetGenreByIdAsync(Guid id)
    {
        return await _context.Genres.Where(p => p.Id == id && p.IsActive).FirstOrDefaultAsync();
    }

    public async Task<Genre?> UpdateGenreAsync(Guid id, Genre updatedGenre)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre is null)
            return null;

        genre.Name = updatedGenre.Name;
        genre.IsActive = updatedGenre.IsActive;

        await _context.SaveChangesAsync();
        return genre;
    }
}
