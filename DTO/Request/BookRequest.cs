namespace Patikadev_RestfulApi.DTO.Request;

public class BookRequest
{
    public string Name { get; set; }
    //public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }

    public Guid AuthorId { get; set; }
    public Guid GenreId { get; set; }
}
