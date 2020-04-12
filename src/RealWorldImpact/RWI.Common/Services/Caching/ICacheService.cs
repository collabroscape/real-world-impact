using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RWI.Common.Services.Caching
{
    public interface ICacheService
    {
        Task<T> GetCachedItemAsync<T>(string key, Func<Task<T>> fallback) where T : class;
        T GetCachedItem<T>(string key, Func<T> fallback) where T : class;
        Task AddItemAsync(object objectToCache, string key);
        void AddItem(object objectToCache, string key);
        Task DeleteItemAsync(string key);
        void DeleteItem(string key);
    }
}
