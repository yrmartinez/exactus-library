using System;
using System.ComponentModel.DataAnnotations.Schema;
using Exactus.BookStore.Users;
using Volo.Abp.Domain.Entities.Auditing;

namespace Exactus.BookStore.Books
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        public string Title { get; set; }

        public BookStatus Status { get; set; }

        public BookLocation Location { get; set; }

        public BookType Type { get; set; }

        public string Owner { get; set; }

        public string Authors { get; set; }

        [NotMapped]
        public AppUser CheckedOutBy { get; set; }

        [ForeignKey("CheckedOutById")]
        public Guid? CheckedOutById { get; set; }

        public DateTime? CheckOutDate { get; set; }
    }
}