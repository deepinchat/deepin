using DeepIn.WebChat.HttpAggregator.Dtos;
using DeepIn.WebChat.HttpAggregator.Models;
using DeepIn.WebChat.HttpAggregator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.WebChat.HttpAggregator.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MessageData>> Post([FromBody]PostMessageDTO dto)
        {
            var result = await _messageService.SendAsync(dto);
            return Ok(result);
        }
    }
}
