using DeepIn.Application.Models;
using DeepIn.Identity.Application.Models;
using IdentityServer4.EntityFramework.Entities;

namespace DeepIn.Identity.Application.Services
{
    public interface IApiScopeService
    {
        Task<ApiScope> Create(ApiScopeDto dto);
        Task<ApiScopeProperty> CreateProperty(int apiScopeId, PropertyDto dto);
        Task Delete(int id);
        Task DeleteProperty(int id);
        Task<ApiScopeModel> GetById(int id);
        Task<IPagedResult<ApiScopeModel>> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null);
        Task<List<PropertyModel>> GetProperties(int apiScopeId);
        Task<ApiScope> Update(int id, ApiScopeDto dto);
        Task<ApiScopeProperty> UpdateProperty(int id, PropertyDto dto);
    }
}