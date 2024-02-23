using Microsoft.EntityFrameworkCore;

namespace TestProj_ABP_Backend.DbContext;

public class ContextFactory
{
    private static IConfiguration _configuration;

    public ContextFactory(IConfiguration config)
    {
        _configuration = config;
    }

    private static readonly DbContextOptions Options = new DbContextOptionsBuilder<MyDbContext>()
        .UseSqlServer(_configuration.GetConnectionString("Default"))
        .UseLazyLoadingProxies()
        .Options;

    public static MyDbContext CreateNew()
    {
        if (_configuration is null) throw new ArgumentException("configuration was null");
        return new MyDbContext(Options);
    }
}
