namespace  DeepIn.Identity.Application
{
    public static class CacheKeyConstants
    {
        public static string GetAllProfiles => "profile_all";
        public static string GetAllUsers => "user_all";
        public static string GetProfileById(string userId) => $"profile_by_userId_{userId}";
    }
}
