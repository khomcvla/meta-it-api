using AutoMapper;
using MetaITAPI.Dtos;
using MetaITAPI.Entities;

namespace MetaITAPI.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Author
            CreateMap<Author, AuthorGetDto>()
                .ForMember(dest => dest.BookIds, opt => opt.MapFrom(src => src.Books.Select(book => book.BookId)));

            // Book
            CreateMap<Book, BookGetDto>();
            CreateMap<Book, BookPostDto>();
            CreateMap<Book, BookPatchDto>();

            CreateMap<BookPostDto, Book>()
                .ForAllMembers(opt => opt.UseDestinationValue());

            CreateMap<BookPatchDto, Book>()
                .ForAllMembers(opt => opt.UseDestinationValue());
        }
    }
}
