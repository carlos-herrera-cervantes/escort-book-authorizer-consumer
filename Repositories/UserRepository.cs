using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EscortBookAuthorizerConsumer.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EscortBookAuthorizerConsumer.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region snippet_Properties

        private readonly IMongoCollection<User> _collection;

        #endregion

        #region snippet_Constructors

        public UserRepository(MongoClient client, IConfiguration configuration)
        {
            _collection = client
                .GetDatabase(configuration["MongoDB:Default"])
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
}
