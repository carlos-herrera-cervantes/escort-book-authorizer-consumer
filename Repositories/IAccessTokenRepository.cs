using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EscortBookAuthorizerConsumer.Models;

namespace EscortBookAuthorizerConsumer.Repositories
{
    public interface IAccessTokenRepository
    {
        Task DeleteAsync(Expression<Func<AccessToken, bool>> expression);
    }
}
