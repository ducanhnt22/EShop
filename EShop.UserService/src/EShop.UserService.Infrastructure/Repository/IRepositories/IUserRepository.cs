using EShop.UserService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure.Repository.IRepositories
{
    //Use for outside query IdentityUser
    public interface IUserRepository
    {
        public Task<User> GetByPhone(string phoneNumber);
        public Task<User> GetById(Guid id);
        public Task<List<User>> GetAllAsync();
        void Update(object user);
    }
}
