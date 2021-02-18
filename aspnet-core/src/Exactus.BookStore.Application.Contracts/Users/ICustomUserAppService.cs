using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;

namespace Exactus.BookStore.Users
{
    public interface ICustomUserAppService : IApplicationService, ITransientDependency
    {
        Task<List<SmallUserDto>> GetUserByNameAsync(string name);
    }
}