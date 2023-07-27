using MongoDB.Driver;

namespace DeepIn.Messaging.API;

public class MessagingDbContext
{
    private readonly MongoClient _mongoClient;
    private readonly string _dbName;
    private IMongoDatabase _database;
    public MessagingDbContext(MongoClient mongoClient, string dbName)
    {
        _mongoClient = mongoClient;
        _dbName = dbName;
    }
    public IMongoDatabase Database
    {
        get
        {
            if (_database == null)
                _database = _mongoClient.GetDatabase(_dbName);
            return _database;
        }
    }
}
