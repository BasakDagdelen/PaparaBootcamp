namespace Patikadev_RestfulApi.Domain;

public class Author
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; }

    public ICollection<Book> Books { get; set; }
}
