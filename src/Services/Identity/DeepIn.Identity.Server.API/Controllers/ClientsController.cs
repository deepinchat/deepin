using AutoMapper;
using DeepIn.Identity.Server.API.Controllers;
using DeepIn.Identity.Application;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Identity.Server.API.Controllers
{
    public class ClientsController : BaseControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        public ClientsController(
            IMapper mapper,
            IClientService ClientService)
        {
            _mapper = mapper;
            _clientService = ClientService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _clientService.GetById(id);
            if (model == null)
                return NotFound();
            return Ok(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null)
        {
            var result = await _clientService.GetList(pageIndex, pageSize, keywords);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientDto dto)
        {
            var result = await _clientService.Create(dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<ClientModel>(result));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClientDto dto)
        {
            await _clientService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clientService.Delete(id);
            return NoContent();
        }


        [HttpGet("{id}/Secrets")]
        public async Task<IActionResult> GetSecrets(int id)
        {
            var result = await _clientService.GetSecrets(id);
            return Ok(result);
        }

        [HttpPost("{id}/Secrets")]
        public async Task<IActionResult> PostSecret(int id, [FromBody] SecretDto dto)
        {
            var result = await _clientService.CreateSecret(id, dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<SecretModel>(result));
        }

        [HttpDelete("Secrets/{secretId}")]
        public async Task<IActionResult> DeleteSecret(int secretId)
        {
            await _clientService.DeleteSecret(secretId);
            return NoContent();
        }


        [HttpGet("{id}/Properties")]
        public async Task<IActionResult> GetProperties(int id)
        {
            var result = await _clientService.GetProperties(id);
            return Ok(result);
        }

        [HttpPost("{id}/Property")]
        public async Task<IActionResult> PostProperty(int id, [FromBody] PropertyDto dto)
        {
            var result = await _clientService.CreateProperty(id, dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<PropertyModel>(result));
        }

        [HttpDelete("Property/{propertyId}")]
        public async Task<IActionResult> DeleteProperty(int propertyId)
        {
            await _clientService.DeleteProperty(propertyId);
            return NoContent();
        }

        [HttpGet("{id}/Claimes")]
        public async Task<IActionResult> GetClaims(int id)
        {
            var result = await _clientService.GetClaims(id);
            return Ok(result);
        }

        [HttpPost("{id}/Claim")]
        public async Task<IActionResult> PostClaim(int id, [FromBody] ClientClaimDto dto)
        {
            var result = await _clientService.CreateClaim(id, dto);
            if (result == null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = result.Id }, _mapper.Map<ClientClaimModel>(result));
        }

        [HttpDelete("Claim/{claimId}")]
        public async Task<IActionResult> DeleteClaim(int claimId)
        {
            await _clientService.DeleteClaim(claimId);
            return NoContent();
        }
    }
}
