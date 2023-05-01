using challenge.Database;
using challenge.Interfaces.Services;
using challenge.Interfaces;
using challenge.Repositories;
using challenge.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class ContaServiceTestsFixture : IDisposable
{
    private readonly ServiceProvider _serviceProvider;

    public ContaServiceTestsFixture()
    {
        var services = new ServiceCollection()
            .AddDbContext<BancoContext>(options =>
                options.UseInMemoryDatabase("TestDb"))
            .AddScoped<IContaRepository, ContaRepository>()
            .AddScoped<IContaService, ContaService>();

        _serviceProvider = services.BuildServiceProvider();
    }

    public IContaRepository ContaRepository => _serviceProvider.GetService<IContaRepository>();
    public IContaService ContaService => _serviceProvider.GetService<IContaService>();

    public void Dispose()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<BancoContext>();
            context.Database.EnsureDeleted();
        }
    }
    [CollectionDefinition("ContaServiceTests")]
    public class ContaServiceTestsCollection : ICollectionFixture<ContaServiceTestsFixture>
    {
    }
}
