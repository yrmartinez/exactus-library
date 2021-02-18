using System;
using System.ComponentModel.DataAnnotations;
using Exactus.BookStore.Users;
using Volo.Abp.Application.Dtos;

namespace Exactus.BookStore.Comments
{
    public class CommentDto : IEntityDto<Guid>
    {
        public string Comment { get; set; }

        public SmallUserDto User { get; set; }

        public Guid Id { get; set; }
    }
}