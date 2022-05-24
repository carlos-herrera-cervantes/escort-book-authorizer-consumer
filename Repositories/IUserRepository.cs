using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using EscortBookAuthorizerConsumer.Models;

namespace EscortBookAuthorizerConsumer.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);

        Task UpdateAsync(Expression<Func<User, bool>> expression, User user);
    }
}
