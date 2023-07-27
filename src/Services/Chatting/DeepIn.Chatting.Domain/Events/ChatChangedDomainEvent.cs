using DeepIn.Chatting.Domain.ChatAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepIn.Chatting.Domain.Events
{
    public class ChatChangedDomainEvent : INotification
    {
        public Chat Chat { get; set; }
        public string[] UserIds { get; set; }
        public ChatChangedDomainEvent(Chat chat, string[] userIds)
        {
            Chat = chat;
            UserIds = userIds;
        }
    }
}
