using System.Threading.Tasks;
using MongoDB.Driver;
using EscortBookAuthorizer.Consumer.Models;

namespace EscortBookAuthorizer.Consumer.Repositories;

public interface IUserRepository
{
    Task<long> CountAsync(FilterDefinition<User> filter);

    Task<User> GetAsync(FilterDefinition<User> filter);

    Task CreateAsync(User user);

    Task UpdateAsync(FilterDefinition<User> filter, User user);

    Task DeleteManyAsync(FilterDefinition<User> filter);
}
