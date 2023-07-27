using AutoMapper;
using DeepIn.Identity.Application;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Identity.Server.API.Controllers
{
    public class IdentityResourcesController : BaseControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IIdentityResourceService _identityResourceService;
        public IdentityResourcesController(
            IMapper mapper,
            IIdentityResourceService IdentityResourceService)
        {
            _mapper = mapper;
            _identityResourceService = IdentityResourceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _identityResourceService.GetById(id);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null)
        {
            var result = await _identityResourceService.GetList(pageIndex, pageSize, keywords);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IdentityResourceDto dto)
        {
            var result = await _identityResourceService.Create(dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<IdentityResourceModel>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] IdentityResourceDto dto)
        {
            await _identityResourceService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _identityResourceService.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}/Properties")]
        public async Task<IActionResult> GetProperties(int id)
        {
            var result = await _identityResourceService.GetProperties(id);
            return Ok(result);
        }

        [HttpPost("{id}/Property")]
        public async Task<IActionResult> PostProperty(int id, [FromBody] PropertyDto dto)
        {
            var result = await _identityResourceService.CreateProperty(id, dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<PropertyModel>(result));
        }

        [HttpDelete("Property/{propertyId}")]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            await _identityResourceService.DeleteProperty(propertyId);
            return NoContent();
        }
    }
}
