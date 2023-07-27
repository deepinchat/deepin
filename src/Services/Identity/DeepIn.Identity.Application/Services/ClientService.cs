using AutoMapper;
using DeepIn.Application.Models;
using DeepIn.Domain.Exceptions;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Infrastructure;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeepIn.Identity.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly ConfigurationObjectContext _dbContext;
        public ClientService(
            IMapper mapper,
            ConfigurationObjectContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<Client> Create(ClientDto dto)
        {
            var entity = _mapper.Map<Client>(dto);
            entity.Created = DateTime.UtcNow;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<Client> Update(int id, ClientDto dto)
        {
            var entity = await _dbContext.Clients.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(Client)} was not found, id:{id}");
            _mapper.Map(dto, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task Delete(int id)
        {
            var entity = await _dbContext.Clients.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(Client)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<ClientModel> GetById(int id)
        {
            var entity = await _dbContext.Clients.AsNoTracking()
                .Include(s => s.AllowedCorsOrigins)
                .Include(s => s.AllowedGrantTypes)
                .Include(s => s.AllowedScopes)
                .Include(s => s.IdentityProviderRestrictions)
                .Include(s => s.PostLogoutRedirectUris)
                .Include(s => s.RedirectUris)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (entity == null)
                return null;
            return _mapper.Map<ClientModel>(entity);
        }
        public async Task<IPagedResult<ClientModel>> GetList(int pageIndex = 1, int pageSize = 10, string clientId = null, string keywords = null)
        {
            var query = _dbContext.Clients.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keywords))
            {
                query = query.Where(x => x.ClientName.Contains(keywords));
            }
            if (!string.IsNullOrEmpty(clientId))
            {
                query = query.Where(x => x.ClientId == clientId);
            }
            return _mapper.ProjectToPaged<ClientModel>(query, pageIndex, pageSize);
        }

        public async Task<ClientSecret> CreateSecret(int clientId, SecretDto dto)
        {
            var entity = _mapper.Map<ClientSecret>(dto);
            entity.ClientId = clientId;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteSecret(int id)
        {
            var entity = await _dbContext.ClientSecrets.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ClientSecret)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<SecretModel>> GetSecrets(int clientId)
        {
            var query = _dbContext.ClientSecrets.AsNoTracking().Where(x => x.ClientId == clientId);
            return await _mapper.ProjectTo<SecretModel>(query).ToListAsync();
        }
        public async Task<ClientProperty> CreateProperty(int clientId, PropertyDto dto)
        {
            var entity = _mapper.Map<ClientProperty>(dto);
            entity.ClientId = clientId;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<ClientProperty> UpdateProperty(int id, PropertyDto dto)
        {
            var entity = await _dbContext.ClientProperties.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ClientProperty)} was not found, id:{id}");
            _mapper.Map(dto, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteProperty(int id)
        {
            var entity = await _dbContext.ClientProperties.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ClientProperty)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<PropertyModel>> GetProperties(int clientId)
        {
            var query = _dbContext.ClientProperties.AsNoTracking().Where(x => x.ClientId == clientId);
            return await _mapper.ProjectTo<PropertyModel>(query).ToListAsync();
        }

        public async Task<ClientClaim> CreateClaim(int clientId, ClientClaimDto dto)
        {
            var entity = _mapper.Map<ClientClaim>(dto);
            entity.ClientId = clientId;
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<ClientClaim> UpdateClaim(int id, ClientClaimDto dto)
        {
            var entity = await _dbContext.ClientClaims.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ClientClaim)} was not found, id:{id}");
            _mapper.Map(dto, entity);
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task DeleteClaim(int id)
        {
            var entity = await _dbContext.ClientClaims.FindAsync(id);
            if (entity == null)
                throw new DomainException($"Entity {nameof(ClientClaim)} was not found, id:{id}");
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<ClientClaimModel>> GetClaims(int clientId)
        {
            var query = _dbContext.ClientClaims.AsNoTracking().Where(x => x.ClientId == clientId);
            return await _mapper.ProjectTo<ClientClaimModel>(query).ToListAsync();
        }
    }
}
