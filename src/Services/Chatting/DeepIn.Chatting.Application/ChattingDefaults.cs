using DeepIn.Chatting.Domain.ChatAggregate;
using DeepIn.Chatting.Infrastructure;

namespace DeepIn.Chatting.Application;

public static class ChattingDefaults
{
    public static class CacheKeys
    {
        public static string GetChats(string userId) => $"chats_{userId}";
       // public static string GetChatUserIds(string chatId) => $"chat_users_{chatId}";
    }
    public static class TableNames
    {
        public static string Chat => $"{ChattingDbContext.DEFAULT_SCHEMA}.{Domain.ChatAggregate.Chat.TableName}";
        public static string ChatMember => $"{ChattingDbContext.DEFAULT_SCHEMA}.{Domain.ChatAggregate.ChatMember.TableName}";
    }
}
