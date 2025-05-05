using EShop.UserService.Domain.Entities;
using EShop.UserService.Infrastructure.Persistence;
using EShop.UserService.Infrastructure.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure.Repository.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserDbContext _userDbContext;
    public UserRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userDbContext.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetById(Guid id)
    {
        return await _userDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetByPhone(string phoneNumber)
    {
        return await _userDbContext.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}
