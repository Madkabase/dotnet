using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Utilities.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;

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
    public DbSet<FieldUser> FieldUsers { get; set; }
    public DbSet<Alert> Alerts { get; set; }

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

        modelBuilder.Entity<FieldUser>()
        .HasOne(fu => fu.User)
        .WithMany(u => u.FieldUsers);

        modelBuilder.Entity<FieldUser>()
        .HasOne(fu => fu.Field)
        .WithMany(f => f.FieldUsers);

        // on Device, the FieldId is nullable
        modelBuilder.Entity<Device>()
            .HasOne(d => d.Field)
            .WithMany(f => f.Devices)
            .HasForeignKey(d => d.FieldId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<DeviceData>()
            .Property(dd => dd.BatteryLevel)
            .IsRequired(false);

        // on Threshold, The FieldId is nullable
        modelBuilder.Entity<Threshold>()
            .HasOne(t => t.Field)
            .WithOne(f => f.Threshold)
            .HasForeignKey<Field>(f => f.ThresholdId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // on Alert, the FieldId is nullable
        modelBuilder.Entity<Alert>()
            .HasOne(a => a.Field)
            .WithMany(f => f.Alerts)
            .HasForeignKey(a => a.FieldId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}