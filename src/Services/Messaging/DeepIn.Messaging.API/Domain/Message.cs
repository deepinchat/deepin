using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeepIn.Messaging.API.Domain
{
    public class Message : IDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string ChatId { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
        public string ReplyTo { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsRead { get; set; }
        public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public long ModifiedAt { get; set; } 
    }
}
