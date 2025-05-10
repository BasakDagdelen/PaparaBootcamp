namespace Patikadev_RestfulApi.DTO;

public class BookResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }
}
