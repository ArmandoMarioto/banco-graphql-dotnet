
var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGraphQLServer();
builder.Services.AddControllers();
var app = builder.Build();

app.UseRouting();
app.MapGraphQL();
app.Run();
