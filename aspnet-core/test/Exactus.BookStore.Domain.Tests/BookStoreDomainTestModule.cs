using Exactus.BookStore.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Exactus.BookStore
{
    [DependsOn(
        typeof(BookStoreEntityFrameworkCoreTestModule)
        )]
    public class BookStoreDomainTestModule : AbpModule
    {

    }
}