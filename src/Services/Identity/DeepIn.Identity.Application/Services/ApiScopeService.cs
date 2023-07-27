using AutoMapper;
using DeepIn.Application.Models;
using DeepIn.Domain.Exceptions;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Infrastructure;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Identity.Application.Services
{
    public class ApiScopeService : IApiScopeService
    {
        private readonly IMapper _mapper;
        private readonly ConfigurationObjectContext _dbContext;
        public ApiScopeService(
            IMapper mapper,
            ConfigurationObjectContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ApiScope> Create(ApiScopeDto dto)
        {
            var entity = _mapper.Map<ApiScope>(dto);
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<ApiScope> Update(int id, ApiScopeDto dto)
        {
            var entity = await _dbContext.ApiScopes.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ApiScope)} was not found, id:{id}");
            _mapper.Map(dto, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task Delete(int id)
        {
            var entity = await _dbContext.ApiScopes.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ApiScope)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<ApiScopeModel> GetById(int id)
        {
            var entity = await _dbContext.ApiScopes.AsNoTracking()
                .Include(s => s.UserClaims)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                return null;
            return _mapper.Map<ApiScopeModel>(entity);
        }
        public async Task<IPagedResult<ApiScopeModel>> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null)
        {
            var query = _dbContext.ApiScopes.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                query = query.Where(x => x.Name.Contains(keywords) || x.DisplayName.Contains(keywords));
            }
            return _mapper.ProjectToPaged<ApiScopeModel>(query, pageIndex, pageSize);
        }
        public async Task<ApiScopeProperty> CreateProperty(int apiScopeId, PropertyDto dto)
        {
            var entity = _mapper.Map<ApiScopeProperty>(dto);
            entity.ScopeId = apiScopeId;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<ApiScopeProperty> UpdateProperty(int id, PropertyDto dto)
        {
            var entity = await _dbContext.ApiScopeProperties.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ApiScopeProperty)} was not found, id:{id}");
            _mapper.Map(dto, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteProperty(int id)
        {
            var entity = await _dbContext.ApiScopeProperties.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ApiScopeProperty)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<PropertyModel>> GetProperties(int apiScopeId)
        {
            var query = _dbContext.ApiScopeProperties.AsNoTracking().Where(x => x.ScopeId == apiScopeId);
            return await _mapper.ProjectTo<PropertyModel>(query).ToListAsync();
        }
    }
}
