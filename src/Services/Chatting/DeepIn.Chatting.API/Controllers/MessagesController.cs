using DeepIn.Chatting.Application.Models;
using DeepIn.Chatting.Application.Queries;
using DeepIn.EventBus.Shared.Events;
using DeepIn.Service.Common.Services;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Chatting.API.Controllers
{
    public class MessagesController : BaseController
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IChatQueries _chatQueries;
        private readonly IUserContext _userContext;
        public MessagesController(
            IPublishEndpoint publishEndpoint,
            IChatQueries chatQueries,
            IUserContext userContext)
        {
            _publishEndpoint = publishEndpoint;
            _chatQueries = chatQueries;
            _userContext = userContext;
        }

        [HttpPost("{chatId}")]
        public async Task<IActionResult> Send(string chatId, [FromBody] PostMessageModel model)
        {
            var isUserInChat = await _chatQueries.IsUserInChat(_userContext.UserId, chatId);
            if (!isUserInChat)
                return Forbid();
            await _publishEndpoint.Publish(new SaveMessageIntegrationEvent(chatId, model.Content, model.ReplyTo, _userContext.UserId, DateTime.Now));
            return Ok();
        }
    }
}
