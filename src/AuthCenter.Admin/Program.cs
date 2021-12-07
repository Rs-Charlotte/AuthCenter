using AuthCenter.Admin.Extensions;
using AuthCenter.Infrastructure.Seed;

Console.Title = "AuthCenter.IdentityServer4";

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddControllers();
services.AddSwaggerGen();

services.AddMySqlDomainContext(configuration.GetConnectionString("MySQLDB"));
services.AddRepositories();
services.AddServices();
services.AddMediatRServices();
services.AddEventBus(configuration);
services.AddCustomAuthentication(configuration);

var app = builder.Build();

GenerateSeedData seed = new GenerateSeedData();
seed.GenerateSeedDataAsync(app).Wait();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute()
        .RequireAuthorization();
});

app.Run();
