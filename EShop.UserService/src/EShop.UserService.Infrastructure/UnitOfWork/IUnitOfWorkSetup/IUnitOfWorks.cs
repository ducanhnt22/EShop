using EShop.UserService.Infrastructure.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;

public interface IUnitOfWorks
{
    public IUserRepository UserRepository { get; }
    public Task<int> SaveChangeAsync();
}
