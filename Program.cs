using Microsoft.EntityFrameworkCore;
using challenge.Database;
using challenge.Interfaces;
using challenge.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer();
builder.Services.AddControllers();
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("ctx")));
builder.Services.AddScoped<IContaRepository, ContaRepository>();
var app = builder.Build();

app.UseRouting();
app.MapGraphQL();
app.Run();

