using AutoMapper;
using DeepIn.Application.Models;
using DeepIn.Domain.Exceptions;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Infrastructure;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Identity.Application.Services
{
    public class ApiResourceService : IApiResourceService
    {
        private readonly IMapper _mapper;
        private readonly ConfigurationObjectContext _dbContext;
        public ApiResourceService(
            IMapper mapper,
            ConfigurationObjectContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ApiResource> Create(ApiResourceDto dto)
        {
            var entity = _mapper.Map<ApiResource>(dto);
            entity.Created = DateTime.UtcNow;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<ApiResource> Update(int id, ApiResourceDto dto)
        {
            var entity = await _dbContext.ApiResources.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ApiResource)} was not found, id:{id}");
            _mapper.Map(dto, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task Delete(int id)
        {
            var entity = await _dbContext.ApiResources.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ApiResource)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<ApiResourceModel> GetById(int id)
        {
            var entity = await _dbContext.ApiResources.AsNoTracking()
                .Include(s => s.Scopes)
                .Include(s => s.UserClaims)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                return null;
            return _mapper.Map<ApiResourceModel>(entity);
        }
        public async Task<IPagedResult<ApiResourceModel>> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null)
        {
            var query = _dbContext.ApiResources.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                query = query.Where(x => x.Name.Contains(keywords) || x.DisplayName.Contains(keywords));
            }
            return _mapper.ProjectToPaged<ApiResourceModel>(query, pageIndex, pageSize);
        }

        public async Task<ApiResourceSecret> CreateSecret(int apiResourceId, SecretDto dto)
        {
            var entity = _mapper.Map<ApiResourceSecret>(dto);
            entity.ApiResourceId = apiResourceId;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteSecret(int id)
        {
            var entity = await _dbContext.ApiSecrets.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ApiResourceSecret)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<SecretModel>> GetSecrets(int apiResourceId)
        {
            var query = _dbContext.ApiSecrets.AsNoTracking().Where(x => x.ApiResourceId == apiResourceId);
            return await _mapper.ProjectTo<SecretModel>(query).ToListAsync();
        }
        public async Task<ApiResourceProperty> CreateProperty(int apiResourceId, PropertyDto dto)
        {
            var entity = _mapper.Map<ApiResourceProperty>(dto);
            entity.ApiResourceId = apiResourceId;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteProperty(int id)
        {
            var entity = await _dbContext.ApiResourceProperties.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ApiResourceProperty)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<PropertyModel>> GetProperties(int apiResourceId)
        {
            var query = _dbContext.ApiResourceProperties.AsNoTracking().Where(x => x.ApiResourceId == apiResourceId);
            return await _mapper.ProjectTo<PropertyModel>(query).ToListAsync();
        }
    }
}
