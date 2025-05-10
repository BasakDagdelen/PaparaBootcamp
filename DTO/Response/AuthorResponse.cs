using System.Text.Json.Serialization;

namespace Patikadev_RestfulApi.DTO.Response;

public class AuthorResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; }
}
