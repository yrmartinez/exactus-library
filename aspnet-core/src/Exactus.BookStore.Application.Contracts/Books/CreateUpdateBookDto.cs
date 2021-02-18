using System.ComponentModel.DataAnnotations;

namespace Exactus.BookStore.Books
{
    public class CreateUpdateBookDto
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        [StringLength(128)]
        public string Owner { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.DigitalCopy;

        [Required]
        public BookStatus Status { get; set; } = BookStatus.Available;

        [Required]
        public BookLocation Location { get; set; } = BookLocation.ExactusOffice;


        public string Authors { get; set; }
    }
}