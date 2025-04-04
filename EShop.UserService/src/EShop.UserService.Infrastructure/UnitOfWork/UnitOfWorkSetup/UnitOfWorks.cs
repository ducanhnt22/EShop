using EShop.UserService.Infrastructure.Persistence;
using EShop.UserService.Infrastructure.Repository.IRepositories;
using EShop.UserService.Infrastructure.UnitOfWork.IUnitOfWorkSetup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure.UnitOfWork.UnitOfWorkSetup;

public class UnitOfWorks : IUnitOfWorks
{
    private readonly UserDbContext _userDbContext;
    private readonly IUserRepository _userRepository;
    public UnitOfWorks(UserDbContext userDbContext, IUserRepository userRepository)
    {
        _userDbContext = userDbContext;
        _userRepository = userRepository;
    }

    public IUserRepository UserRepository => _userRepository;

    public async Task<int> SaveChangeAsync()
    {
        return await _userDbContext.SaveChangesAsync();
    }
}
