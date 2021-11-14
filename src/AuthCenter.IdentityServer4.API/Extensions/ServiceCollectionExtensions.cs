using AuthCenter.Domain.Entities;
using AuthCenter.IdentityServer4.API.Application.IntegrationEvents;
using AuthCenter.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthCenter.IdentityServer4.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<AuthContext>(optionsAction);
        }

        public static IServiceCollection AddMySqlDomainContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDomainContext(builder =>
            {
                builder.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion);
            });
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }

        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthContextTransactionBehavior<,>));
            return services.AddMediatR(typeof(User).Assembly, typeof(Program).Assembly);
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ISubscriberService, SubscriberService>();
            services.AddCap(options =>
            {
                options.UseMySql(configuration.GetConnectionString("MySQLDB"));

                options.UseRabbitMQ(options =>
                {
                    configuration.GetSection("RabbitMQ").Bind(options);
                });
                // options.UseDashboard();
            });

            return services;
        }
    }
}
