using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Exactus.BookStore.Comments
{
    public interface ICommentsAppService : ICrudAppService<
        CommentDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCommentDto>
    {
        Task<List<CommentDto>> GetCommentsByBookAsync(Guid bookId);
    }
}