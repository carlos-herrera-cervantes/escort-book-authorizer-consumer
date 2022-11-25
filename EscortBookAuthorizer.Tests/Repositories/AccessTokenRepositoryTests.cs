using System;
using Xunit;
using MongoDB.Driver;
using System.Threading.Tasks;
using EscortBookAuthorizer.Consumer.Repositories;
using EscortBookAuthorizer.Consumer.Models;

namespace EscortBookAuthorizer.Tests.Repositories;

[Collection(nameof(AccessTokenRepository))]
public class AccessTokenRepositoryTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient;

    #endregion

    #region snippet_Constructors

    public AccessTokenRepositoryTests()
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGO_DB_HOST");
        _mongoClient = new MongoClient(connectionString);
    }

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
