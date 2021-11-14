using AuthCenter.IdentityServer4.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var Configuration = builder.Configuration;

// Add services to the container.

services.AddControllers();
services.AddSwaggerGen();

services.AddMySqlDomainContext(Configuration.GetConnectionString("MySQLDB"));
services.AddRepositories();
services.AddServices();
services.AddMediatRServices();
services.AddEventBus(Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
