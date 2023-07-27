using AutoMapper;
using DeepIn.Application.Models;
using DeepIn.Domain.Exceptions;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Infrastructure;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Identity.Application.Services
{
    public class IdentityResourceService : IIdentityResourceService
    {
        private readonly IMapper _mapper;
        private readonly ConfigurationObjectContext _dbContext;
        public IdentityResourceService(
            IMapper mapper,
            ConfigurationObjectContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IdentityResource> Create(IdentityResourceDto dto)
        {
            var entity = _mapper.Map<IdentityResource>(dto);
            entity.Created = DateTime.UtcNow;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IdentityResource> Update(int id, IdentityResourceDto dto)
        {
            var entity = await _dbContext.IdentityResources.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(IdentityResource)} was not found, id:{id}");
            _mapper.Map(dto, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task Delete(int id)
        {
            var entity = await _dbContext.IdentityResources.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(IdentityResource)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IdentityResourceModel> GetById(int id)
        {
            var entity = await _dbContext.IdentityResources.AsNoTracking()
                .Include(s => s.UserClaims)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                return null;
            return _mapper.Map<IdentityResourceModel>(entity);
        }
        public async Task<IPagedResult<IdentityResourceModel>> GetList(int pageIndex = 1, int pageSize = 10, string keywords = null)
        {
            var query = _dbContext.IdentityResources.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                query = query.Where(x => x.Name.Contains(keywords) || x.DisplayName.Contains(keywords));
            }
            return _mapper.ProjectToPaged<IdentityResourceModel>(query, pageIndex, pageSize);
        }
        public async Task<IdentityResourceProperty> CreateProperty(int identityResourceId, PropertyDto dto)
        {
            var entity = _mapper.Map<IdentityResourceProperty>(dto);
            entity.IdentityResourceId = identityResourceId;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<IdentityResourceProperty> UpdateProperty(int id, PropertyDto dto)
        {
            var entity = await _dbContext.IdentityResourceProperties.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(IdentityResourceProperty)} was not found, id:{id}");
            _mapper.Map(dto, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteProperty(int id)
        {
            var entity = await _dbContext.IdentityResourceProperties.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(IdentityResourceProperty)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<PropertyModel>> GetProperties(int identityResourceId)
        {
            var query = _dbContext.IdentityResourceProperties.AsNoTracking().Where(x => x.IdentityResourceId == identityResourceId);
            return await _mapper.ProjectTo<PropertyModel>(query).ToListAsync();
        }
    }
}
