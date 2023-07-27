using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DeepIn.Messaging.API.Domain
{
    public interface IMongoRepository<TDocument> where TDocument : class, IDocument
    {
        IMongoCollection<TDocument> Collection { get; }
        Task DeleteByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<TDocument> FindByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<TDocument> InsertAsync([NotNull] TDocument document, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDocument>> InsertRangeAsync([NotNull] IEnumerable<TDocument> documents, CancellationToken cancellationToken = default);
    }
}