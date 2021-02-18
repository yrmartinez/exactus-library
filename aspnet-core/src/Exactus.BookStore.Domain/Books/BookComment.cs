using System;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace Exactus.BookStore.Books
{
    public class BookComment : AuditedAggregateRoot<Guid>
    {
        public string Comment { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        [ForeignKey("BookId")]
        public Guid BookId { get; set; }
    }
}