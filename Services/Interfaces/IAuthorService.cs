using Patikadev_RestfulApi.Domain;

namespace Patikadev_RestfulApi.Services.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(Guid id);
    Task<Author> CreateAuthorAsync(Author author);
    Task<Author?> UpdateAuthorAsync(Guid id, Author updatedauthor);
    Task<bool> DeleteAuthorAsync(Guid id);
}
