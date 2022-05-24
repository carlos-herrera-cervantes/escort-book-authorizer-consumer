using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EscortBookAuthorizerConsumer.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace EscortBookAuthorizerConsumer.Repositories
{
    public class AccessTokenRepository : IAccessTokenRepository
    {
        #region snippet_Properties

        private readonly IMongoCollection<AccessToken> _collection;

        #endregion

        #region snippet_Constructors

        public AccessTokenRepository(MongoClient client, IConfiguration configuration)
        {
            _collection = client
                .GetDatabase(configuration["MongoDB:Default"])
                .GetCollection<AccessToken>("accesstokens");
        }

        #endregion

        #region snippet_ActionMethods

        public async Task DeleteAsync(Expression<Func<AccessToken, bool>> expression)
            => await _collection.DeleteManyAsync(expression);

        #endregion
    }
}
