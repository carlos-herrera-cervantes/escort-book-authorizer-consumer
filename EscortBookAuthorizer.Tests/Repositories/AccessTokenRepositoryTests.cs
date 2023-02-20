using Xunit;
using System.Threading.Tasks;
using MongoDB.Driver;
using EscortBookAuthorizer.Consumer.Repositories;
using EscortBookAuthorizer.Consumer.Models;
using EscortBookAuthorizer.Consumer.Constants;

namespace EscortBookAuthorizer.Tests.Repositories;

[Collection(nameof(AccessTokenRepository))]
public class AccessTokenRepositoryTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient;

    #endregion

    #region snippet_Constructors

    public AccessTokenRepositoryTests()
        => _mongoClient = new MongoDB.Driver.MongoClient(MongoClientConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should create a new access token")]
    public async Task CreateAsyncShouldCreateAccessToken()
    {
        var accessToken = new AccessToken
        {
            User = "test.user@example.com",
            Token = "dummy-jwt"
        };
        var accessTokenRepository = new AccessTokenRepository(_mongoClient);

        await accessTokenRepository.CreateAsync(accessToken);

        long counter = await accessTokenRepository.CountAsync(Builders<AccessToken>.Filter.Empty);
        Assert.Equal<long>(1, counter);

        await accessTokenRepository.DeleteManyAsync(Builders<AccessToken>.Filter.Empty);
    }

    #endregion
}
