using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence;

public class AgroditDbContext : DbContext
{
    public AgroditDbContext(DbContextOptions<AgroditDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ThresholdPreset> ThresholdPresets { get; set; }
    public DbSet<GlobalThresholdPreset> GlobalThresholdPresets { get; set; }
    public DbSet<Threshold> Thresholds { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Farm> Farms { get; set; }
    public DbSet<FarmUser> FarmUsers { get; set; }
    public DbSet<Field> Fields { get; set; }
    public DbSet<SubscriptionRequest> SubscriptionRequests { get; set; }
    public DbSet<DeviceData> DeviceData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Threshold>()
            .Property(t => t.MainSensor)
            .HasDefaultValue(MainSensor.SensorDown);

        modelBuilder.Entity<Threshold>()
            .HasOne(t => t.Field)
            .WithOne(f => f.Threshold)
            .HasForeignKey<Field>(f => f.ThresholdId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FarmUser>()
        .HasOne(fu => fu.Farm)
        .WithMany(f => f.FarmUsers);

        modelBuilder.Entity<FarmUser>()
        .HasOne(fu => fu.User)
        .WithMany(u => u.FarmUsers);
    }
}