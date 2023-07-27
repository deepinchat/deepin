using DeepIn.Identity.Application.Models.Profile;
using DeepIn.Identity.Application.Services;
using DeepIn.Service.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Identity.API.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IUserContext _userContext;
        public ProfileController(IUserService userService,
            IUserContext userContext)
        {
            _userService = userService;
            _userContext = userContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetList(string ids)
        {
            var userCliams = await _userService.GetUserClaims(ids.Split(","));
            var profiles = userCliams.GroupBy(x => x.UserId).Select(g =>
            {
                return new ProfileResponse(g.Key, g.ToList());
            });
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _userService.GetProfileByUserId(id);
            return Ok(response);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search(string userNameOrEmail)
        {
            var response = await _userService.GetProfileByUserNameOrEmail(userNameOrEmail?.Trim());
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProfileRequest request)
        {
            var response = await _userService.UpdateProfile(_userContext.UserId, request);
            return Ok(response);
        }
    }
}
