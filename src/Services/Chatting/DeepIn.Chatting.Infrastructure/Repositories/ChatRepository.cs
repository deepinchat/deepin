using DeepIn.Chatting.Domain.ChatAggregate;
using DeepIn.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DeepIn.Chatting.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ChattingDbContext _context;
        public ChatRepository(ChattingDbContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public Chat Add(Chat chat)
        {
            return _context.Chats.Add(chat).Entity;
        }

        public async Task<Chat> FindById(string id)
        {
            return await _context.Chats.FindAsync(id);
        }

        public Chat Update(Chat chat)
        {
            chat.UpdatedAt = DateTime.UtcNow;
            return _context.Chats.Update(chat).Entity;
        }
    }
}
