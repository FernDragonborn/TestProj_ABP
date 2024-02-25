using Microsoft.EntityFrameworkCore;
using TestProj_ABP_Backend.Models;

namespace TestProj_ABP_Backend.DbContext;
public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public MyDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<BrowserFingerprint> Fingerprints { get; set; } = null!;

    public DbSet<ColorTestModel> ColorTest { get; set; } = null!;

    public DbSet<PriceTestModel> PriceTest { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        base.ConfigureConventions(builder);
    }
}