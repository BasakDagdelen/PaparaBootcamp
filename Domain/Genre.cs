namespace Patikadev_RestfulApi.Domain;

public class Genre
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public bool IsActive { get; set; }

    public ICollection<Book> Books { get; set; }
}
