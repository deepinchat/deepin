namespace DeepIn.Caching
{
    public interface ICacheManager
    {
        Task<T> GetAsync<T>(string key);
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> acquire);
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> acquire, int cacheTime);
        Task<bool> IsSetAsync(string key);
        Task SetAsync<T>(string key, T data);
        Task SetAsync<T>(string key, T data, int cacheTime);
        Task RemoveAsync(string key);
        Task RemoveByPrefix(string prefix);
        Task ClearAsync();
    }
}
