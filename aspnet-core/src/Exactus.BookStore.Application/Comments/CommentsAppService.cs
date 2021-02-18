using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exactus.BookStore.Books;
using Exactus.BookStore.Users;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Exactus.BookStore.Comments
{
    public class CommentsAppService : CrudAppService<
            BookComment, //The Book entity
            CommentDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateCommentDto>, //Used to create/update a book
        ICommentsAppService
    {
        private readonly IRepository<AppUser, Guid> _usersRepository;

        public CommentsAppService(IRepository<BookComment, Guid> repository, IRepository<AppUser, Guid> usersRepository) : base(repository)
        {
            _usersRepository = usersRepository;
        }

        public override async Task<CommentDto> CreateAsync(CreateUpdateCommentDto input)
        {
            await CheckCreatePolicyAsync();

            var entity = new BookComment
            {
                UserId = this.CurrentUser.Id.Value,
                BookId = input.BookId,
                Comment = input.Comment
            };

            TryToSetTenantId(entity);

            entity.UserId = this.CurrentUser.Id.Value;

            await Repository.InsertAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        public async Task<List<CommentDto>> GetCommentsByBookAsync(Guid bookId)
        {
            await Repository.GetQueryableAsync();

            //Prepare a query to join books and authors
            var query = from comment in (await this.ReadOnlyRepository.GetQueryableAsync()).Where(c => c.BookId == bookId)
                        join user in _usersRepository on comment.UserId equals user.Id
                        where comment.BookId == bookId
                        select new CommentDto
                        {
                            Id = comment.Id,
                            Comment = comment.Comment,
                            User = new SmallUserDto
                            {
                                Id = user.Id,
                                Name = user.Name + ' ' + user.Surname
                            }
                        };

            return query.ToList();
        }
    }
}