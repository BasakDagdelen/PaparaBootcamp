﻿namespace Patikadev_RestfulApi.Domain;

public class Book
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    //public string Author { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }

    public Guid AuthorId { get; set; }
    public Author Author { get; set; }
    public Guid GenreId { get; set; }
    public Genre Genre { get; set; }
}
