using AutoMapper;
using Patikadev_RestfulApi.Domain;
using Patikadev_RestfulApi.DTO;

namespace Patikadev_RestfulApi.Mapping;

public class MappingConfiguration: Profile
{
    public MappingConfiguration()
    {
        CreateMap<BookRequest, Book>().ReverseMap();
        CreateMap<Book, BookResponse>().ReverseMap();
    }
}
