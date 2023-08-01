using DeepIn.Chatting.Application.Queries;
using DeepIn.Chatting.SignalrHub.Models;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DeepIn.Chatting.SignalrHub.Hubs
{
    [Authorize]
    public class ChatsHub : Hub
    {
        private readonly IChatQueries _chatQueries;
        private string _userId = null; 
        public ChatsHub(IChatQueries chatQueries)
        {
                _chatQueries = chatQueries;
        }
        public string UserId
        {
            get
            {
                if (string.IsNullOrEmpty(_userId))
                {
                    _userId = Context.User.FindFirst("sub")?.Value;
                }
                if (string.IsNullOrEmpty(_userId))
                {
                    _userId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                }
                return _userId;
            }
        }

        public override async Task OnConnectedAsync()
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                var chats = await _chatQueries.GetChats(UserId);

                foreach (var chat in chats)
                {
                    await this.Groups.AddToGroupAsync(this.Context.ConnectionId, chat.Id.ToString());
                }
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                var chats = await _chatQueries.GetChats(UserId);

                foreach (var chat in chats)
                {
                    await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, chat.Id.ToString());
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

    }
}
