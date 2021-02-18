using System;
using System.Threading.Tasks;
using Exactus.BookStore.Localization;
using Exactus.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Exactus.BookStore.Books
{
    [Authorize(BookStorePermissions.Books.Default)]
    public class BookAppService : CrudAppService<
            Book, //The Book entity
            BookDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto>, //Used to create/update a book
    IBookAppService //implement the IBookAppService
    {
        private readonly IStringLocalizer<BookStoreResource> _localizer;
        private readonly IRepository<BookComment, Guid> _bookCommentsRepository;

        public BookAppService(
            IRepository<Book, Guid> repository,
            IStringLocalizer<BookStoreResource> localizer,
            IRepository<BookComment, Guid> bookCommentsRepository)
            : base(repository)
        {
            _localizer = localizer;
            _bookCommentsRepository = bookCommentsRepository;
            GetPolicyName = BookStorePermissions.Books.Default;
            GetListPolicyName = BookStorePermissions.Books.Default;
            CreatePolicyName = BookStorePermissions.Books.Create;
            UpdatePolicyName = BookStorePermissions.Books.Edit;
            DeletePolicyName = BookStorePermissions.Books.Delete;
        }

        public override async Task<BookDto> CreateAsync(CreateUpdateBookDto input)
        {
            await CheckCreatePolicyAsync();

            if (await this.Repository.AnyAsync(b => b.Title == input.Title))
            {
                throw new UserFriendlyException(_localizer["BookAlreadyExist"]);
            }

            var entity = await MapToEntityAsync(input);

            TryToSetTenantId(entity);

            await Repository.InsertAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        public async Task CheckoutAsync(Guid bookGuid)
        {
            await CheckPolicyAsync(GetPolicyName);

            var book = await this.Repository.GetAsync(bookGuid);

            if (book is null)
            {
                throw new UserFriendlyException(_localizer["BookDoesNotExist"]);
            }

            if (book.Status == BookStatus.CheckedOut)
            {
                throw new UserFriendlyException(_localizer["BookCheckoutException"]);
            }

            book.CheckOutDate = DateTime.Now;

            // ReSharper disable once PossibleInvalidOperationException
            // User must exist before get this far.
            book.CheckedOutById = this.CurrentUser.Id.Value;

            await this.Repository.UpdateAsync(book, true);
        }

        public async Task ReturnAsync(ReturnBookDto input)
        {
            await CheckPolicyAsync(GetPolicyName);

            var book = await this.Repository.GetAsync(input.BookId);

            if (book is null)
            {
                throw new UserFriendlyException(_localizer["BookDoesNotExist"]);
            }

            book.CheckOutDate = null;
            book.CheckedOutById = null;
            book.Status = BookStatus.Available;

            await this.Repository.UpdateAsync(book, true);

            if (input.Comments is not null && input.Comments != string.Empty)
            {
                var newBookComment = new BookComment
                {
                    Comment = input.Comments,
                    // ReSharper disable once PossibleInvalidOperationException
                    // User must exist before get this far.
                    UserId = this.CurrentUser.Id.Value,
                    BookId = input.BookId
                };

                await this._bookCommentsRepository.InsertAsync(newBookComment);
            }
        }
    }
}