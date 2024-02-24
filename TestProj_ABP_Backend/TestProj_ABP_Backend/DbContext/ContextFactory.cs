using Microsoft.EntityFrameworkCore;

namespace TestProj_ABP_Backend.DbContext;

public static class ContextFactory
{
    public static MyDbContext New(IConfiguration configuration)
    {
        if (configuration is null) { throw new ArgumentException("configuration was null"); }

        DbContextOptions options = new DbContextOptionsBuilder<MyDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"))
            .UseLazyLoadingProxies()
            .Options;

        return new MyDbContext(options);
    }
}
