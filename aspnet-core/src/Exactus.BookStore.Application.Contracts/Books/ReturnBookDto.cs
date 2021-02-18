using System;
using System.ComponentModel.DataAnnotations;

namespace Exactus.BookStore.Books
{
    public class ReturnBookDto
    {
        [Required]
        public Guid BookId { get; set; }

        [StringLength(255)]
        public string Comments { get; set; }
    }
}