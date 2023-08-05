using DeepIn.Messaging.API.Models.Messages;
using DeepIn.Messaging.API.Services;
using DeepIn.Service.Common.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Messaging.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IUserContext _userContext;
        public MessagesController(
            IMessageService messageService,
            IUserContext userContext)
        {
            _messageService = messageService;
            _userContext = userContext;
        }
        //TODO Need to check chat permission
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _messageService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        //TODO Need to check chat permission
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] MessageQuery query)
        {
            var list = await _messageService.GetPagedListAsync(query.PageIndex, query.PageSize, query.ChatId, query.From, query.Keywords);
            return Ok(list);
        }

        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] MessageRequest request)
        //{
        //    var list = await _messageService.InsertAsync(request, _userContext.UserId);
        //    return Ok(list);
        //}
    }
}
