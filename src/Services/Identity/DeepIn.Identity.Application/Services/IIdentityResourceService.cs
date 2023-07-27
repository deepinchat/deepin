using DeepIn.Application.Models;
using DeepIn.Identity.Application.Models;
using IdentityServer4.EntityFramework.Entities;

namespace DeepIn.Identity.Application.Services
{
    public interface IIdentityResourceService
    {
        Task<IdentityResource> Create(IdentityResourceDto dto);
        Task<IdentityResourceProperty> CreateProperty(int identityResourceId, PropertyDto dto);
        Task Delete(int id);
        Task DeleteProperty(int id);
        Task<IdentityResourceModel> GetById(int id);
        Task<IPagedResult<IdentityResourceModel>> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null);
        Task<List<PropertyModel>> GetProperties(int identityResourceId);
        Task<IdentityResource> Update(int id, IdentityResourceDto dto);
        Task<IdentityResourceProperty> UpdateProperty(int id, PropertyDto dto);
    }
}