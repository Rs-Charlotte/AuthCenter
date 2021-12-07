using AuthCenter.Domain.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthCenter.Infrastructure.Seed
{
    public class GenerateSeedData
    {
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private ILogger _logger;

        public async Task GenerateSeedDataAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                _userManager = scope.ServiceProvider.GetService<UserManager<User>>();
                _roleManager = scope.ServiceProvider.GetService<RoleManager<Role>>();
                _logger = scope.ServiceProvider.GetService<ILogger<GenerateSeedData>>();

                await GenerateIdentitySeedAsync(scope.ServiceProvider);
                await GenerateIdentityServerSeedAsync(scope.ServiceProvider);
            }
        }

        private async Task GenerateIdentitySeedAsync(IServiceProvider services)
        {
            await services.GetService<AuthContext>().Database.MigrateAsync();

             if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new Role("Admin");
                var result = await _roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new Exception("初始化角色失败：" + result.Errors.SelectMany(x => x.Description));
                }
            }

            if (await _userManager.FindByNameAsync("Admin") == null)
            {
                var defaultUser = new User() { UserName = "Admin", SecurityStamp = "Admin" };
                var result = await _userManager.CreateAsync(defaultUser, "Admin123123@");
                if (!result.Succeeded)
                {
                    throw new Exception("初始化默认用户失败");
                }
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }

            _logger.LogInformation("初始化Identity成功");
        }

        private async Task GenerateIdentityServerSeedAsync(IServiceProvider services)
        {
            services.GetRequiredService<AuthPersistedGrantDbContext>().Database.Migrate();
            var configurationDbContext = services.GetRequiredService<AuthConfigurationDbContext>();
            configurationDbContext.Database.Migrate();

            if (!configurationDbContext.Clients.Any())
            {
                foreach (var client in Config.GetClients())
                {
                    await configurationDbContext.Clients.AddAsync(client.ToEntity());
                }
                await configurationDbContext.SaveChangesAsync();
            }

            if (!configurationDbContext.ApiResources.Any())
            {
                foreach (var api in Config.GetApiResources())
                {
                    await configurationDbContext.ApiResources.AddAsync(api.ToEntity());
                }
                await configurationDbContext.SaveChangesAsync();
            }

            if (!configurationDbContext.ApiScopes.Any())
            {
                foreach (var api in Config.GetApiScopes())
                {
                    await configurationDbContext.ApiScopes.AddAsync(api.ToEntity());
                }
                await configurationDbContext.SaveChangesAsync();
            }

            if (!configurationDbContext.IdentityResources.Any())
            {
                foreach (var identity in Config.GetIdentityResources())
                {
                    await configurationDbContext.IdentityResources.AddAsync(identity.ToEntity());
                }
                await configurationDbContext.SaveChangesAsync();
            }

            _logger.LogInformation("初始化IdentityServer4成功");
        }
    }
}
