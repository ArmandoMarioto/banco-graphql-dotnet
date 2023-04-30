using Microsoft.EntityFrameworkCore;
using challenge.Database;
using challenge.Interfaces;
using challenge.Repositories;
using challenge.Interfaces.Services;
using challenge.Services;
using challenge.Api;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();
builder.Services.AddControllers();

builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("ctx")));

//Repositories
builder.Services.AddScoped<IContaRepository, ContaRepository>();
//Services
builder.Services.AddScoped<IContaService, ContaService>();
var app = builder.Build();

app.UseRouting();
app.MapGraphQL();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<BancoContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.Run();

