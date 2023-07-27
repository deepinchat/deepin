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
        private string _userId = null;
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
        
    }
}
