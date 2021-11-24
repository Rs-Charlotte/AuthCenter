using AuthCenter.Admin.Extensions;
using AuthCenter.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace AuthCenter.Admin
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMySqlDomainContext(Configuration.GetConnectionString("MySQLDB"));
            services.AddRepositories();
            services.AddServices();
            services.AddMediatRServices();
            services.AddEventBus(Configuration);
            services.AddCustomAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                GenerateSeedData seed = new GenerateSeedData();
                seed.GenerateSeedDataAsync(app).Wait();
            }

            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
