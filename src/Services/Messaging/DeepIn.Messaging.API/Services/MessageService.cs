using DeepIn.Application.Models;
using DeepIn.EventBus.Shared.Events;
using DeepIn.Messaging.API.Domain;
using DeepIn.Messaging.API.Models.Messages;
using MassTransit;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeepIn.Messaging.API.Services
{
    public class MessageService : IMessageService
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMongoRepository<Message> _repository;
        public MessageService(
            IPublishEndpoint publishEndpoint,
            IMongoRepository<Message> repository)
        {
            _publishEndpoint = publishEndpoint;
            _repository = repository;
        }
        public async Task<Message> InsertAsync(MessageRequest request, string userId, DateTime? createdAt = null)
        {
            var message = await _repository.InsertAsync(new Message()
            {
                ChatId = request.ChatId,
                Content = request.Content,
                CreatedAt = createdAt ?? DateTime.UtcNow,
                From = userId,
                ReplyTo = request.ReplyTo
            });
            await _publishEndpoint.Publish(new PushMessageIntegrationEvent()
            {
                ChatId = message.ChatId,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                ReplyTo = message.ReplyTo,
                From = message.From,
                MessageId = message.Id
            });
            return message;
        }

        public async Task<MessageResponse> GetByIdAsync(string id)
        {
            var message = await _repository.FindByIdAsync(id);
            if (message == null) return null;
            return new MessageResponse(message);
        }

        public IFindFluent<Message, Message> Query(string chatId = null, string from = null, string keywords = null)
        {
            var filterBuilder = Builders<Message>.Filter;
            var filters = new List<FilterDefinition<Message>>();
            if (!string.IsNullOrEmpty(chatId))
            {
                filters.Add(filterBuilder.Eq(d => d.ChatId, chatId));
            }
            if (!string.IsNullOrEmpty(from))
            {
                filters.Add(filterBuilder.Eq(d => d.From, from));
            }
            if (!string.IsNullOrEmpty(keywords))
            {
                filters.Add(filterBuilder.Regex(d => d.Content, new BsonRegularExpression(keywords, "i")));
            }
            var filter = filterBuilder.And(filters);

            var query = _repository.Collection.Find(filter);
            return query;
        }
        public async Task<IPagedResult<MessageResponse>> GetPagedListAsync(int pageIndex = 1, int pageSize = 10, string chatId = null, string from = null, string keywords = null)
        {
            var query = Query(chatId, from, keywords);
            var documents = await query.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync();
            var totalCount = await query.CountDocumentsAsync();

            return new PagedResult<MessageResponse>(documents.Select(m => new MessageResponse(m)), pageIndex, pageSize, (int)totalCount);
        }
    }
}
