using AutoMapper;
using DeepIn.Identity.Application;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DeepIn.Identity.Server.API.Controllers
{
    public class ApiResourcesController : BaseControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApiResourceService _apiResourceService;
        public ApiResourcesController(
            IMapper mapper,
            IApiResourceService apiResourceService)
        {
            _mapper = mapper;
            _apiResourceService = apiResourceService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _apiResourceService.GetById(id);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null)
        {
            var result = await _apiResourceService.GetList(pageIndex, pageSize, keywords);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApiResourceDto dto)
        {
            var result = await _apiResourceService.Create(dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<ApiResourceModel>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ApiResourceDto dto)
        {
            await _apiResourceService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _apiResourceService.Delete(id);
            return NoContent();
        }


        [HttpGet("{id}/Secrets")]
        public async Task<IActionResult> GetSecrets(int id)
        {
            var result = await _apiResourceService.GetSecrets(id);
            return Ok(result);
        }

        [HttpPost("{id}/Secrets")]
        public async Task<IActionResult> PostSecret(int id, [FromBody] SecretDto dto)
        {
            var result = await _apiResourceService.CreateSecret(id, dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<SecretModel>(result));
        }

        [HttpDelete("Secrets/{secretId}")]
        public async Task<IActionResult> DeleteSecret(int secretId)
        {
            await _apiResourceService.DeleteSecret(secretId);
            return NoContent();
        }


        [HttpGet("{id}/Properties")]
        public async Task<IActionResult> GetProperties(int id)
        {
            var result = await _apiResourceService.GetProperties(id);
            return Ok(result);
        }

        [HttpPost("{id}/Property")]
        public async Task<IActionResult> PostProperty(int id, [FromBody] PropertyDto dto)
        {
            var result = await _apiResourceService.CreateProperty(id, dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<PropertyModel>(result));
        }

        [HttpDelete("Property/{propertyId}")]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            await _apiResourceService.DeleteProperty(propertyId);
            return NoContent();
        }
    }
}
