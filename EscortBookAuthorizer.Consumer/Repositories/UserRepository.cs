using System.Threading.Tasks;
using MongoDB.Driver;
using EscortBookAuthorizer.Consumer.Models;
using EscortBookAuthorizer.Consumer.Constants;

namespace EscortBookAuthorizer.Consumer.Repositories;

public class UserRepository : IUserRepository
{
    #region snippet_Properties

    private readonly IMongoCollection<User> _collection;

    #endregion

    #region snippet_Constructors

    public UserRepository(IMongoClient client)
    {
        _collection = client
            .GetDatabase(MongoDatabase.Authorizer)
            .GetCollection<User>("users");
    }

    #endregion

    #region snippet_ActionMethods

    public async Task<long> CountAsync(FilterDefinition<User> filter)
        => await _collection.CountDocumentsAsync(filter);

    public async Task<User> GetAsync(FilterDefinition<User> filter)
        => await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();

    public async Task CreateAsync(User user) => await _collection.InsertOneAsync(user);

    public async Task UpdateAsync(FilterDefinition<User> filter, User user)
        => await _collection.ReplaceOneAsync(filter, user);

    public async Task DeleteManyAsync(FilterDefinition<User> filter)
        => await _collection.DeleteManyAsync(filter);

    #endregion
}
