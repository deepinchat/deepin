using DeepIn.Application.Models;
using DeepIn.Identity.Application.Models;
using IdentityServer4.EntityFramework.Entities;

namespace DeepIn.Identity.Application.Services
{
    public interface IClientService
    {
        Task<Client> Create(ClientDto dto);
        Task<ClientClaim> CreateClaim(int clientId, ClientClaimDto dto);
        Task<ClientProperty> CreateProperty(int clientId, PropertyDto dto);
        Task<ClientSecret> CreateSecret(int clientId, SecretDto dto);
        Task Delete(int id);
        Task DeleteClaim(int id);
        Task DeleteProperty(int id);
        Task DeleteSecret(int id);
        Task<ClientModel> GetById(int id);
        Task<List<ClientClaimModel>> GetClaims(int clientId);
        Task<IPagedResult<ClientModel>> GetList(int pageIndex = 1, int pageSize = 10, string clientId = null, string keywords = null);
        Task<List<PropertyModel>> GetProperties(int clientId);
        Task<List<SecretModel>> GetSecrets(int clientId);
        Task<Client> Update(int id, ClientDto dto);
        Task<ClientClaim> UpdateClaim(int id, ClientClaimDto dto);
        Task<ClientProperty> UpdateProperty(int id, PropertyDto dto);
    }
}