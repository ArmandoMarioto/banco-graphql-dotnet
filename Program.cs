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
    var retryCount = 3;
    var retryDelay = TimeSpan.FromSeconds(30);

    while (retryCount > 0)
    {
        try
        {
            var db = services.GetRequiredService<BancoContext>();
            db.Database.Migrate();
            break; // se migração bem sucedida, sair do loop
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database. Retrying in {0} seconds...", retryDelay.TotalSeconds);

            retryCount--;
            if (retryCount == 0)
            {
                logger.LogError(ex, "Failed to migrate the database after multiple retries.");
            }
            else
            {
                Thread.Sleep(retryDelay); // esperar um pouco antes de tentar novamente
            }
        }
    }
}

app.Run();

