using DeepIn.Messaging.API.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DeepIn.Messaging.API.Infrastructure.Repositories;

public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : class, IDocument
{
    private IMongoCollection<TDocument> _collection;
    private readonly MessagingDbContext _dbContext;
    public MongoRepository(MessagingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IMongoCollection<TDocument> Collection
    {
        get
        {
            if (_collection == null)
            {
                _collection = _dbContext.Database.GetCollection<TDocument>(typeof(TDocument).Name);
            }
            return _collection;
        }
    }
    public async Task DeleteByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        await Collection.DeleteOneAsync(x => x.Id == new ObjectId(id), cancellationToken);
    }

    public async Task<TDocument> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await Collection.Find(x => x.Id == new ObjectId(id)).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TDocument> InsertAsync([NotNull] TDocument document, CancellationToken cancellationToken = default)
    {
        await Collection.InsertOneAsync(document, new InsertOneOptions(), cancellationToken);
        return document;
    }

    public async Task<IEnumerable<TDocument>> InsertRangeAsync([NotNull] IEnumerable<TDocument> documents, CancellationToken cancellationToken = default)
    {
        await Collection.InsertManyAsync(documents, new InsertManyOptions(), cancellationToken);
        return documents;
    }
}
