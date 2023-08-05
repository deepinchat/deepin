using DeepIn.Application.Models;
using DeepIn.Chatting.Application.Dtos;

namespace DeepIn.Chatting.Application.Queries
{
    public interface IChatQueries
    {
        Task<ChatDTO> GetChatById(string id);
        Task<IPagedResult<ChatMemberDTO>> GetChatMembers(string chatId, string keywords = null, bool? isBot = null, bool? isOwner = null, int pageIndex = 1, int pageSize = 10);
        Task<IEnumerable<ChatDTO>> GetUserChats(string userId);
        Task<bool> IsUserInChat(string userId, string chatId);
    }
}