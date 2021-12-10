using AuthCenter.Admin.Extensions;
using Newtonsoft.Json;

Console.Title = "AuthCenter.IdentityServer4";

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
services.AddControllers();
services.AddSwaggerGen();

services.AddCors(options =>
{
    options.AddPolicy("cors", builder => 
    { 
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod(); 
    });
});

services.AddMySqlDomainContext(configuration.GetConnectionString("MySQLDB"));
services.AddRepositories();
services.AddServices();
services.AddMediatRServices();
services.AddEventBus(configuration);
services.AddCustomAuthentication(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors("cors");

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute()
        .RequireAuthorization();
});

app.Run();
