using DeepIn.Chatting.Application.Commands.Chats;
using DeepIn.Chatting.Application.Queries;
using DeepIn.Service.Common.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Chatting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
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
        [AllowAnonymous]
        public async Task<IActionResult> Get(string id)
        {
            var chat = await _chatQueries.GetChatById(id);
            if (chat == null) return NotFound();
            return Ok(chat);
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var list = await _chatQueries.GetChats(_userContext.UserId);
            return Ok(list);
        } 
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateChatCommand command)
        {
            var chat = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = chat.Id }, chat);
        }
        [HttpPost("dialogue")]
        public async Task<IActionResult> CreateDialogue([FromBody] CreateDialogueCommand command)
        {
            var dialogue = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = dialogue.Id }, dialogue);
        }
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateChat(long id, [FromBody] ChatInfoRequest request)
        //{
        //    await _mediator.Send(new UpdateChatCommand(id, request));
        //    return Ok();
        //}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
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
        public async Task<IActionResult> LeaveChat([FromBody] JoinChatCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}/members")]
        public async Task<IActionResult> GetMembers(string id, int pageIndex = 1, int pageSize = 10)
        {
            var pagedResult = await _chatQueries.GetChatMembers(id, null, null, pageIndex, pageSize);
            return Ok(pagedResult);
        }
    }
}
