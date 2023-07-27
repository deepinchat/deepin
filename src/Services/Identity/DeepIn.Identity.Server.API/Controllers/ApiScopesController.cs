using AutoMapper;
using DeepIn.Identity.Application;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Identity.Server.API.Controllers
{
    public class ApiScopesController : BaseControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApiScopeService _apiScopeService;
        public ApiScopesController(
            IMapper mapper,
            IApiScopeService ApiScopeService)
        {
            _mapper = mapper;
            _apiScopeService = ApiScopeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _apiScopeService.GetById(id);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null)
        {
            var result = await _apiScopeService.GetList(pageIndex, pageSize, keywords);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApiScopeDto dto)
        {
            var result = await _apiScopeService.Create(dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<ApiScopeModel>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ApiScopeDto dto)
        {
            await _apiScopeService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _apiScopeService.Delete(id);
            return NoContent();
        }

        [HttpGet("{id}/Properties")]
        public async Task<IActionResult> GetProperties(int id)
        {
            var result = await _apiScopeService.GetProperties(id);
            return Ok(result);
        }

        [HttpPost("{id}/Property")]
        public async Task<IActionResult> PostProperty(int id, [FromBody] PropertyDto dto)
        {
            var result = await _apiScopeService.CreateProperty(id, dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<PropertyModel>(result));
        }

        [HttpDelete("Property/{propertyId}")]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            await _apiScopeService.DeleteProperty(propertyId);
            return NoContent();
        }
    }
}
