namespace DeepIn.Caching
{
    public class RedisCacheOptions : CacheOptions
    {
        public static string ProviderKey => "Redis";
        public string ConnectionString { get; set; }
        public int Database { get; set; }
    }
}
