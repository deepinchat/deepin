using DeepIn.Application.Models;
using DeepIn.Identity.Application.Models;
using IdentityServer4.EntityFramework.Entities;

namespace DeepIn.Identity.Application.Services
{
    public interface IApiResourceService
    {
        Task<ApiResource> Create(ApiResourceDto dto);
        Task<ApiResourceProperty> CreateProperty(int apiResourceId, PropertyDto dto);
        Task<ApiResourceSecret> CreateSecret(int apiResourceId, SecretDto dto);
        Task Delete(int id);
        Task DeleteProperty(int id);
        Task DeleteSecret(int id);
        Task<ApiResourceModel> GetById(int id);
        Task<IPagedResult<ApiResourceModel>> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null);
        Task<List<PropertyModel>> GetProperties(int apiResourceId);
        Task<List<SecretModel>> GetSecrets(int apiResourceId);
        Task<ApiResource> Update(int id, ApiResourceDto dto);
    }
}