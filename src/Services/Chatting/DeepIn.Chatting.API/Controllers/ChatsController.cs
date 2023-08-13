using DeepIn.Chatting.Application.Commands.Chats;
using DeepIn.Chatting.Application.Queries;
using DeepIn.Service.Common.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Chatting.API.Controllers
{
    public class ChatsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserContext _userContext;
        private readonly IChatQueries _chatQueries;
        public ChatsController(
            IMediator mediator,
            IUserContext userContext,
            IChatQueries chatQueries)
        {
            _mediator = mediator;
            _userContext = userContext;
            _chatQueries = chatQueries;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var chat = await _chatQueries.GetChatById(id);
            if (chat == null) return NotFound();
            return Ok(chat);
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var list = await _chatQueries.GetUserChats(_userContext.UserId);
            return Ok(list.OrderByDescending(x => x.UpdatedAt));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateChatCommand command)
        {
            command.UserId = _userContext.UserId;
            var chat = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = chat.Id }, chat);
        }
        [HttpPost("dialogue")]
        public async Task<IActionResult> CreateDialogue([FromBody] CreateDialogueCommand command)
        {
            var dialogue = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = dialogue.Id }, dialogue);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var chat = await _chatQueries.GetChatById(id);
            if (chat == null)
                return NotFound();
            var chatOwners = await _chatQueries.GetChatMembers(chatId: id, isOwner: true);
            if (chatOwners.Items.Any(m => m.UserId == _userContext.UserId))
                return Forbid();
            await _mediator.Send(new DeleteChatCommand(id));
            return NoContent();
        }

        [HttpPost("{id}/join")]
        public async Task<IActionResult> JoinChat([FromBody] JoinChatCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
        [HttpPost("{id}/leave")]
        public async Task<IActionResult> LeaveChat([FromBody] LeaveChatCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}/members")]
        public async Task<IActionResult> GetMembers(string id, int pageIndex = 1, int pageSize = 10)
        {
            var isUserInChat = await _chatQueries.IsUserInChat(_userContext.UserId, id);
            if (!isUserInChat)
                return Forbid();

            var pagedResult = await _chatQueries.GetChatMembers(chatId: id, pageIndex: pageIndex, pageSize: pageSize);
            return Ok(pagedResult);
        }
    }
}
