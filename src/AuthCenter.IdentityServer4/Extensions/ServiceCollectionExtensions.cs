using AuthCenter.Domain.Entities;
using AuthCenter.IdentityServer4.Application.IntegrationEvents;
using AuthCenter.Infrastructure;
using AuthCenter.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuthCenter.IdentityServer4.Extensions
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

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<UserStore<User, Role, AuthContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>, AppUserStore>();
            services.AddScoped<RoleStore<Role, AuthContext, Guid, UserRole, RoleClaim>, AppRoleStore>();
            services.AddScoped<UserManager<User>, AppUserManager>();
            services.AddScoped<RoleManager<Role>, AppRoleManager>();
            services.AddScoped<SignInManager<User>, AppSignInManager>();

            services.AddIdentity<User, Role>(option =>
            {
                // 安全策略
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 6;
            })
                .AddUserStore<AppUserStore>()
                .AddRoleStore<AppRoleStore>()
                .AddUserManager<AppUserManager>()
                .AddRoleManager<AppRoleManager>()
                .AddSignInManager<AppSignInManager>()
                .AddEntityFrameworkStores<AuthContext>()
                .AddDefaultTokenProviders();

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddAspNetIdentity<User>()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddConfigurationStore<AuthConfigurationDbContext>(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseMySql(configuration.GetConnectionString("MySQLDB"),
                        MySqlServerVersion.LatestSupportedServerVersion,
                        sql =>
                        {
                            sql.MigrationsAssembly(typeof(AuthConfigurationDbContext).GetTypeInfo().Assembly.GetName().Name);
                            sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                })
                .AddOperationalStore<AuthPersistedGrantDbContext>(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseMySql(configuration.GetConnectionString("MySQLDB"),
                        MySqlServerVersion.LatestSupportedServerVersion,
                        sql =>
                        {
                            sql.MigrationsAssembly(typeof(AuthPersistedGrantDbContext).GetTypeInfo().Assembly.GetName().Name);
                            sql.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                        });
                });
            //.AddInMemoryIdentityResources(Config.IdentityResources)
            //.AddInMemoryApiScopes(Config.ApiScopes)
            //.AddInMemoryClients(Config.Clients);

            // 开发用证书
            builder.AddDeveloperSigningCredential();

            return services;
        }
    }
}
