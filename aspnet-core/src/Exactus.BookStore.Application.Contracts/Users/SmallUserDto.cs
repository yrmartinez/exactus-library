using System;
using Volo.Abp.Application.Dtos;

namespace Exactus.BookStore.Users
{
    public class SmallUserDto : EntityDto<Guid>

    {
        public string Name { get; set; }
    }
}