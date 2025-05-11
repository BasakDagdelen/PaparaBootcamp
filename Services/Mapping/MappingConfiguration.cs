using AutoMapper;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.DTO.Request;
using Patikadev_RestfulApi.DTO.Response;

namespace Patikadev_RestfulApi.Services.Mapping;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<BookRequest, Book>().ReverseMap();
        CreateMap<Book, BookResponse>().ReverseMap();

        CreateMap<AuthorRequest, Author>().ReverseMap();
        CreateMap<Author, AuthorResponse>().ReverseMap();

        CreateMap<GenreRequest, Genre>().ReverseMap();
        CreateMap<Genre, GenreResponse>().ReverseMap();
    }
}
