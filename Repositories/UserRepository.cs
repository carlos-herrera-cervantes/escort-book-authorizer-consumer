using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EscortBookAuthorizerConsumer.Models;
using MongoDB.Driver;

namespace EscortBookAuthorizerConsumer.Repositories;

public class UserRepository : IUserRepository
{
    #region snippet_Properties

    private readonly IMongoCollection<User> _collection;

    #endregion

    #region snippet_Constructors

    public UserRepository(MongoClient client)
    {
        _collection = client
            .GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DEFAULT_DB"))
            .GetCollection<User>("users");
    }

    #endregion

    #region snippet_ActionMethods

    public async Task<User> GetByIdAsync(string id)
        => await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task UpdateAsync(Expression<Func<User, bool>> expression, User user)
        => await _collection.ReplaceOneAsync(expression, user);

    #endregion
}
