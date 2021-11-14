using EatMeat.Infrastructure.Core.Behaviors;
using Microsoft.Extensions.Logging;

namespace AuthCenter.Infrastructure
{
    public class AuthContextTransactionBehavior<TRequest, TResponse> : TransactionBehavior<AuthContext, TRequest, TResponse>
    {
        public AuthContextTransactionBehavior(AuthContext dbContext, ILogger<AuthContextTransactionBehavior<TRequest, TResponse>> logger) : base(dbContext, logger)
        {
        }
    }
}
