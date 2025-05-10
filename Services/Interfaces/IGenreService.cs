using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.DTO;

namespace Patikadev_RestfulApi.Services.Interfaces;

public interface IGenreService
{
    Task<IEnumerable<Genre>> GetAllGenreAsync();
    Task<Genre?> GetGenreByIdAsync(Guid id);
    Task<Genre> CreateGenreAsync(Genre genre);
    Task<Genre?> UpdateGenreAsync(Guid id, Genre updatedGenre);
    Task<bool> DeleteGenreAsync(Guid id);
}
