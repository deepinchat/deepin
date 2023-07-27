using DeepIn.Application.Models;
using DeepIn.Chatting.Application.Dtos;

namespace DeepIn.Chatting.Application.Queries
{
    public interface IChatQueries
    {
        Task<ChatDTO> GetChatById(string id);
        Task<IPagedResult<ChatMemberDTO>> GetChatMembers(string chatId, string keywords = null, bool? isBot = null, int pageIndex = 1, int pageSize = 10);
        Task<IEnumerable<ChatDTO>> GetChats(string userId);
        Task<bool> IsUserInChat(string userId, string chatId);
        Task<IEnumerable<string>> GetChatUserIds(string chatId);
    }
}