using IoDit.WebAPI.Persistence.Entities;
using IoDit.WebAPI.Persistence.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace IoDit.WebAPI.Persistence;

public class IoDitDbContext : DbContext
{
    public IoDitDbContext(DbContextOptions<IoDitDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<CompanyThresholdPreset> CompanyThresholdPreset { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyUser> CompanyUsers { get; set; }
    public DbSet<CompanyFarmUser> CompanyFarmUsers { get; set; }
    public DbSet<CompanyUserDeviceData> CompanyUserDeviceData { get; set; }
    public DbSet<CompanyDevice> CompanyDevices { get; set; }
    public DbSet<CompanyDeviceData> CompanyDeviceData { get; set; }
    public DbSet<CompanyFarm> CompanyFarms { get; set; }
    public DbSet<CompanyField> CompanyFields { get; set; }
    public DbSet<SubscriptionRequest> SubscriptionRequests { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //--------------------------------------------------------------------
        //USER
        modelBuilder.Entity<User>().HasKey(x => x.Id);
        modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        
        // User - RefreshToken (One-to-Many)
        modelBuilder.Entity<User>()
            .HasMany(user => user.RefreshTokens)
            .WithOne(token => token.User)
            .HasForeignKey(token => token.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasMany(user => user.Companies)
            .WithOne(company => company.Owner)
            .HasForeignKey(company => company.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        // User - CompanyUser (One-to-Many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.CompanyUsers)
            .WithOne(cu => cu.User)
            .HasForeignKey(cu => cu.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // User - SubscriptionRequest (One-to-Many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.SubscriptionRequests)
            .WithOne(cu => cu.User)
            .HasForeignKey(cu => cu.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //Subscription Request
        modelBuilder.Entity<SubscriptionRequest>().HasKey(req => req.Id);
        
        //--------------------------------------------------------------------
        //REFRESH TOKEN
        modelBuilder.Entity<RefreshToken>().HasKey(token => token.Id);
        
        //--------------------------------------------------------------------
        //COMPANY
        modelBuilder.Entity<Company>().HasKey(x => x.Id);

        // Company - CompanyUser (One-to-Many)
        modelBuilder.Entity<Company>()
            .HasMany(company => company.Users)
            .WithOne(companyUsers => companyUsers.Company)
            .HasForeignKey(companyUsers => companyUsers.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Company - CompanyFarm (One-to-Many)
        modelBuilder.Entity<Company>()
            .HasMany(company => company.Farms)
            .WithOne(companyFarm => companyFarm.Company)
            .HasForeignKey(companyFarm => companyFarm.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Company - CompanyDevice (One-to-Many)
        modelBuilder.Entity<Company>()
            .HasMany(company => company.Devices)
            .WithOne(cd => cd.Company)
            .HasForeignKey(cd => cd.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Company - CompanyThresholdPreset (One-to-Many)
        modelBuilder.Entity<Company>()
            .HasMany(company => company.ThresholdPresets)
            .WithOne(cd => cd.Company)
            .HasForeignKey(cd => cd.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Company - CompanyFarmUser (One-to-Many)
        modelBuilder.Entity<Company>()
            .HasMany(company => company.FarmUsers)
            .WithOne(fu => fu.Company)
            .HasForeignKey(fu => fu.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //--------------------------------------------------------------------
        //COMPANY USER
        modelBuilder.Entity<CompanyUser>().HasKey(companyUser => companyUser.Id);
        
        // CompanyUser - CompanyUserDeviceData (One-to-Many)
        modelBuilder.Entity<CompanyUser>()
            .HasMany(companyUser => companyUser.CompanyUserDeviceData)
            .WithOne(companyUserDeviceData => companyUserDeviceData.User)
            .HasForeignKey(companyUserDeviceData => companyUserDeviceData.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // CompanyUser - CompanyUserData (One-to-Many)
        modelBuilder.Entity<CompanyUser>()
            .HasMany(companyUser => companyUser.CompanyFarmUsers)
            .WithOne(companyFarmUsers => companyFarmUsers.CompanyUser)
            .HasForeignKey(companyFarmUsers => companyFarmUsers.CompanyUserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //--------------------------------------------------------------------
        //COMPANY FARM USER
        modelBuilder.Entity<CompanyFarmUser>().HasKey(companyFarmUser => companyFarmUser.Id);
        
        //--------------------------------------------------------------------
        //COMPANY USER DEVICE DATA
        modelBuilder.Entity<CompanyUserDeviceData>().HasKey(companyUserDeviceData => companyUserDeviceData.Id);
        
        //--------------------------------------------------------------------
        //COMPANY THRESHOLD PRESET  
        modelBuilder.Entity<CompanyThresholdPreset>().HasKey(preset => preset.Id);

        //--------------------------------------------------------------------
        //COMPANY DEVICE
        modelBuilder.Entity<CompanyDevice>().HasKey(device => device.Id);
        
        // CompanyDevice - DeviceData (One-to-Many)
        modelBuilder.Entity<CompanyDevice>()
            .HasMany(device => device.DeviceData)
            .WithOne(companyDeviceData => companyDeviceData.Device)
            .HasForeignKey(companyDeviceData => companyDeviceData.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //--------------------------------------------------------------------
        //COMPANY DEVICE DATA
        modelBuilder.Entity<CompanyDeviceData>().HasKey(companyDeviceData => companyDeviceData.Id);
        
        //--------------------------------------------------------------------
        //COMPANY FARM
        modelBuilder.Entity<CompanyFarm>().HasKey(companyFarm => companyFarm.Id);
        modelBuilder.Entity<CompanyFarm>()
            .HasMany(companyFarm => companyFarm.Devices)
            .WithOne(device => device.Farm)
            .HasForeignKey(device => device.FarmId)
            .OnDelete(DeleteBehavior.Cascade);

        // CompanyFarm - CompanyField (One-to-Many)
        modelBuilder.Entity<CompanyFarm>()
            .HasMany(companyFarm => companyFarm.Fields)
            .WithOne(companyField => companyField.CompanyFarm)
            .HasForeignKey(companyField => companyField.CompanyFarmId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //--------------------------------------------------------------------
        //COMPANY FIELD
        modelBuilder.Entity<CompanyField>().HasKey(companyFarm => companyFarm.Id);
        
        // CompanyField - CompanyDevice (One-to-Many)
        modelBuilder.Entity<CompanyField>()
            .HasMany(companyField => companyField.Devices)
            .WithOne(companyDevice => companyDevice.Field)
            .HasForeignKey(companyDevice => companyDevice.FieldId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}