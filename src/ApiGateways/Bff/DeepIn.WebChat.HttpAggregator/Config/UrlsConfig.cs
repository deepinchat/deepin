namespace DeepIn.WebChat.HttpAggregator.Config;

public class UrlsConfig
{
    public string Chatting { get; set; }
    public string Messaging { get; set; }
    public string Identity { get; set; }
    public class MessagingAPI
    {
        public static string PostMessage() => "/api/v1/messages";
    }
    public class ChattingAPI
    {
        public static string GetChatById(string id) => $"/api/v1/chats/{id}";
    }
    public class IdentityAPI
    {
        public static string GetCurrentUserProfile() => $"/api/v1/profile";
        public static string GetUserProfileById(string id) => $"/api/v1/profile/{id}";
    }
}
