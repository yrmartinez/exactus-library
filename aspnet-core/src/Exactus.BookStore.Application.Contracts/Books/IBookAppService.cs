using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Exactus.BookStore.Books
{
    public interface IBookAppService : ICrudAppService<
        BookDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateBookDto>
    {
        Task CheckoutAsync(Guid bookGuid);

        Task ReturnAsync(ReturnBookDto input);
    }
}