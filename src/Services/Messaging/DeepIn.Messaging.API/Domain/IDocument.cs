using MongoDB.Bson;

namespace DeepIn.Messaging.API.Domain
{
    public interface IDocument
    {
        ObjectId Id { get; set; }
    }
}
