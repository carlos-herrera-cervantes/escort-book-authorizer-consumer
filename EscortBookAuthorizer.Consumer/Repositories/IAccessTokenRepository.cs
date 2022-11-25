using System.Threading.Tasks;
using MongoDB.Driver;
using EscortBookAuthorizer.Consumer.Models;

namespace EscortBookAuthorizer.Consumer.Repositories;

public interface IAccessTokenRepository
{
    Task<long> CountAsync(FilterDefinition<AccessToken> filter);

    Task CreateAsync(AccessToken accessToken);

    Task DeleteManyAsync(FilterDefinition<AccessToken> filter);
}
