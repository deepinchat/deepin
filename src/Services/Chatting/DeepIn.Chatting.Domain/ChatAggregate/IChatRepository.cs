using DeepIn.Domain;

namespace DeepIn.Chatting.Domain.ChatAggregate
{
    public interface IChatRepository : IRepository<Chat>
    {
        Chat Add(Chat chat);
        Task<Chat> FindById(string id);
        Chat Update(Chat chat);
    }
}
