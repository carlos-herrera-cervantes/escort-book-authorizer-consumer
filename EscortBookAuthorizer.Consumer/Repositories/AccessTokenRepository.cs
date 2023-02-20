using System.Threading.Tasks;
using MongoDB.Driver;
using EscortBookAuthorizer.Consumer.Models;
using EscortBookAuthorizer.Consumer.Constants;

namespace EscortBookAuthorizer.Consumer.Repositories;

public class AccessTokenRepository : IAccessTokenRepository
{
    #region snippet_Properties

    private readonly IMongoCollection<AccessToken> _collection;

    #endregion

    #region snippet_Constructors

    public AccessTokenRepository(IMongoClient client)
    {
        _collection = client
            .GetDatabase(MongoDatabase.Authorizer)
            .GetCollection<AccessToken>("accesstokens");
    }

    #endregion

    #region snippet_ActionMethods

    public async Task<long> CountAsync(FilterDefinition<AccessToken> filter)
        => await _collection.CountDocumentsAsync(filter);

    public async Task CreateAsync(AccessToken accessToken)
        => await _collection.InsertOneAsync(accessToken);

    public async Task DeleteManyAsync(FilterDefinition<AccessToken> filter)
        => await _collection.DeleteManyAsync(filter);

    #endregion
}
