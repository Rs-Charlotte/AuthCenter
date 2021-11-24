// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using AuthCenter.Admin.Extensions;
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

            //services.UseAdminUI();
            //services.AddScoped<IdentityExpressDbContext, SqliteIdentityDbContext>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();

            //app.UseAdminUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
