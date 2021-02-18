using AutoMapper;
using Exactus.BookStore.Books;
using Exactus.BookStore.Comments;

namespace Exactus.BookStore
{
    public class BookStoreApplicationAutoMapperProfile : Profile
    {
        public BookStoreApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Book, BookDto>();
            CreateMap<CreateUpdateBookDto, Book>();
            CreateMap<BookComment, CommentDto>();
            CreateMap<CreateUpdateCommentDto, Book>();
        }
    }
}
