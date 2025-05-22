using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.UserService.Infrastructure.Cachings.ICachingService;
public interface ICacheService
{
    Task SetCacheAsync<T>(string key, T value, TimeSpan expirationTime);
    Task<T> GetCacheAsync<T>(string key);
    Task RemoveCacheAsync(string key);
    Task<bool> IsCacheExistsAsync(string key);
}
