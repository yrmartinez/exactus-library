using System;

namespace Exactus.BookStore.Comments
{
    public class CreateUpdateCommentDto
    {
        public string Comment { get; set; }

        public Guid BookId { get; set; }
    }
}