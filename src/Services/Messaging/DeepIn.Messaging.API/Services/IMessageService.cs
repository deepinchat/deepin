using DeepIn.Application.Models;
using DeepIn.Messaging.API.Domain;
using DeepIn.Messaging.API.Models.Messages;
using MongoDB.Driver;

namespace DeepIn.Messaging.API.Services
{
    public interface IMessageService
    {
        Task<IPagedResult<MessageResponse>> GetPagedListAsync(int pageIndex = 1, int pageSize = 10, string chatId = null, string from = null, string keywords = null);
        Task<Message> InsertAsync(MessageRequest request, string userId, DateTime? createdAt = null, string messageId = null);
        Task<MessageResponse> GetByIdAsync(string id);
        IFindFluent<Message, Message> Query(string chatId = null, string from = null, string keywords = null);
    }
}