using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Exactus.BookStore.Users
{
    public class CustomUserAppService : ICustomUserAppService
    {
        private readonly IRepository<AppUser, Guid> _appUsersRepository;

        public CustomUserAppService(IRepository<AppUser, Guid> appUsersRepository)
        {
            _appUsersRepository = appUsersRepository;
        }

        public async Task<List<SmallUserDto>> GetUserByNameAsync(string name)
        {
            var appUsers = (await this._appUsersRepository.GetQueryableAsync()).Where(x => x.Name.Contains(name) || x.Surname.Contains(name));

            if (appUsers.Any())
            {
                return (from appUser in appUsers
                        select new SmallUserDto
                        {
                            Id = appUser.Id,
                            Name = appUser.Name + ' ' + appUser.Surname
                        }).ToList();
            }

            return null;
        }
    }
}