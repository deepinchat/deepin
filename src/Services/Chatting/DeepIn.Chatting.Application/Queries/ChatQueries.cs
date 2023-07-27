using Dapper;
using DeepIn.Application.Models;
using DeepIn.Caching;
using DeepIn.Chatting.Application.Dtos;
using Npgsql;
using System.Collections.Generic;
using static DeepIn.Chatting.Application.ChattingDefaults;

namespace DeepIn.Chatting.Application.Queries
{
    public class ChatQueries : IChatQueries
    {
        private readonly string _connectionString;
        private readonly ICacheManager _cacheManager;
        public ChatQueries(
            string connectionString,
            ICacheManager cacheManager)
        {
            _connectionString = connectionString;
            _cacheManager = cacheManager;
        }
        private async Task<IEnumerable<ChatDTO>> QueryChats(string userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = $@"SELECT c.id Id,c.type Type,c.created_at CreatedAt, c.updated_at UpdatedAt, c.created_by CreatedBy, 
                            c.avatar_id AvatarBlobId, c.is_private IsPrivate, c.name Name,c.link Link,c.description Description
                            FROM {TableNames.Chat} c 
                            JOIN {TableNames.ChatMember} cm ON c.id = cm.chat_id
                            WHERE c.is_deleted = false AND cm.user_id = @userId";
                var result = await connection.QueryAsync<ChatDTO>(sql, new { userId });
                return result;
            }
        }
        public async Task<IEnumerable<ChatDTO>> GetChats(string userId)
        {
            return await _cacheManager.GetOrCreateAsync(CacheKeys.GetChats(userId), () => this.QueryChats(userId));
        }
        public async Task<ChatDTO> GetChatById(string id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = $@"SELECT id Id, type Type, created_at CreatedAt, updated_at UpdatedAt, created_by CreatedBy FROM {TableNames.Chat} WHERE id = @id";
                var result = await connection.QueryFirstOrDefaultAsync<ChatDTO>(sql, new { id });
                return result;
            }
        }
        public async Task<IPagedResult<ChatMemberDTO>> GetChatMembers(string chatId, string keywords = null, bool? isBot = null, int pageIndex = 1, int pageSize = 10)
        {
            var sql = $@"SELECT * FROM {TableNames.ChatMember}"; //TODO query by field not *
            var sqlForTotalCount = $@"SELECT COUNT(1) FROM {TableNames.ChatMember} ";
            var condation = " WHERE chat_id = @chatId ";
            if (!string.IsNullOrEmpty(keywords))
            {
                condation += "AND title LIKE @keywords ";
            }
            if (isBot.HasValue)
            {
                condation += "AND is_bot = @isBot ";
            }
            sqlForTotalCount += condation;
            sql += $"{condation} ORDER BY created_at OFFSET {(pageIndex - 1) * pageSize} LIMIT {pageSize};";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var list = await connection.QueryAsync<ChatMemberDTO>(sql, new { chatId, keywords = $"%{keywords}%", isBot });
                var count = await connection.QueryFirstAsync<int>(sqlForTotalCount, new { chatId, keywords = $"%{keywords}%", isBot });
                return new PagedResult<ChatMemberDTO>(list, pageIndex, pageSize, count);
            }
        }
        public async Task<bool> IsUserInChat(string userId, string chatId)
        {
            var sql = $@"SELECT COUNT(*) FROM {TableNames.ChatMember} WHERE chat_id = @chatId AND user_id = @userId";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var count = await connection.QueryFirstAsync<int>(sql, new { chatId, userId });
                return count > 0;
            }
        }
        public async Task<IEnumerable<string>> GetChatUserIds(string chatId)
        {
            return await _cacheManager.GetOrCreateAsync(CacheKeys.GetChatUserIds(chatId), () => this.QueryChatUserIds(chatId));
        }
        private async Task<IEnumerable<string>> QueryChatUserIds(string chatId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = $@"SELECT user_id FROM {TableNames.ChatMember} WHERE chat_id = @chatId ";
                var result = await connection.QueryAsync<string>(sql, new { chatId });
                return result;
            }
        }
    }
}
