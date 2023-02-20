using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using EscortBookAuthorizer.Consumer.Repositories;
using EscortBookAuthorizer.Consumer.Models;
using EscortBookAuthorizer.Consumer.Constants;

namespace EscortBookAuthorizer.Tests.Repositories;

[Collection(nameof(UserRepository))]
public class UserRepositoryTests
{
    #region snippet_Properties

    private readonly IMongoClient _mongoClient;

    #endregion

    #region snippet_Constructors

    public UserRepositoryTests()
        => _mongoClient = new MongoDB.Driver.MongoClient(MongoClientConfig.ConnectionString);

    #endregion

    #region snippet_Tests

    [Fact(DisplayName = "Should update a user")]
    public async Task UpdateAsyncShouldUpdateUser()
    {
        var roles = new List<string> { "Employee" };
        var user = new User
        {
            VerificationToken = "dummy-verification-token",
            Verified = false,
            Password = "secret",
            Email = "test.user@example.com",
            FirebaseToken = "dummy-firebase-token",
            Type = "Organization",
            Roles = roles,
            Block = false,
            Deactivated = false,
            Delete = false
        };
        var userRepository = new UserRepository(_mongoClient);

        await userRepository.CreateAsync(user);

        User getResultBeforeUpdate = await userRepository.GetAsync(Builders<User>.Filter.Eq(u => u.Email, "test.user@example.com"));
        getResultBeforeUpdate.Verified = true;

        await userRepository.UpdateAsync(Builders<User>.Filter.Eq(u => u.Email, "test.user@example.com"), getResultBeforeUpdate);

        User getResultAfterUpdate = await userRepository.GetAsync(Builders<User>.Filter.Eq(u => u.Email, "test.user@example.com"));
        Assert.True(getResultAfterUpdate.Verified);

        await userRepository.DeleteManyAsync(Builders<User>.Filter.Empty);
    }

    #endregion
}
