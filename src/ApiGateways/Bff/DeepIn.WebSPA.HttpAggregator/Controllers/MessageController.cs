using DeepIn.WebSPA.HttpAggregator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.WebSPA.HttpAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public MessageController()
        {
                
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SendMessageRequest model)
        {
            //Setp 1, Save message
            //Setp 2, Get chat users
            //Setp 3, Push notifications
            return Ok();
        }
    }
}
