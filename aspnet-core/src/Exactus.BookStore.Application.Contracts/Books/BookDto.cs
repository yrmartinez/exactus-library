using System;
using Volo.Abp.Application.Dtos;

namespace Exactus.BookStore.Books
{
    public class BookDto : AuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public BookStatus Status { get; set; }

        public BookLocation Location { get; set; }

        public BookType Type { get; set; }

        public string Owner { get; set; }

        public string Authors { get; set; }

        public Guid? CheckedOutById { get; set; }

        public DateTime? CheckOutDate { get; set; }
    }
}