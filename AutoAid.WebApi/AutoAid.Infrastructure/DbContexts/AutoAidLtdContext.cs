using AutoAid.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Infrastructure.DbContexts;

public partial class AutoAidLtdContext : DbContext
{
    public AutoAidLtdContext()
    {
    }

    public AutoAidLtdContext(DbContextOptions<AutoAidLtdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<EmergentRequest> EmergentRequests { get; set; }

    public virtual DbSet<EmergentRequestEvent> EmergentRequestEvents { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Garage> Garages { get; set; }

    public virtual DbSet<GarageAccount> GarageAccounts { get; set; }

    public virtual DbSet<GarageService> GarageServices { get; set; }

    public virtual DbSet<Place> Places { get; set; }

    public virtual DbSet<PrismaMigration> PrismaMigrations { get; set; }

    public virtual DbSet<ServiceSchedule> ServiceSchedules { get; set; }

    public virtual DbSet<SparePart> SpareParts { get; set; }

    public virtual DbSet<SparePartCategory> SparePartCategories { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=postgres.wyvernpserver.tech;Port=5432;Database=auto_aid_ltd;Username=sa;Password=ThanhPhong2506;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pkey");

            entity.ToTable("account", "ids");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.AccessToken)
                .HasColumnType("character varying")
                .HasColumnName("access_token");
            entity.Property(e => e.AccountRole)
                .HasMaxLength(50)
                .HasColumnName("account_role");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.ExpAccessToken)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("exp_access_token");
            entity.Property(e => e.ExpRefreshTokenn)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("exp_refresh_tokenn");
            entity.Property(e => e.HashPassword)
                .HasColumnType("character varying")
                .HasColumnName("hash_password");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.RefreshToken)
                .HasColumnType("character varying")
                .HasColumnName("refresh_token");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.ToTable("customer", "app");

            entity.HasIndex(e => e.Email, "customer_email_key").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");

            entity.HasOne(d => d.Account).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_account_id_fkey");
        });

        modelBuilder.Entity<EmergentRequest>(entity =>
        {
            entity.HasKey(e => e.EmergentRequestId).HasName("emergent_request_pkey");

            entity.ToTable("emergent_request", "app");

            entity.Property(e => e.EmergentRequestId).HasColumnName("emergent_request_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.GarageId).HasColumnName("garage_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.Remark)
                .HasMaxLength(255)
                .HasColumnName("remark");
            entity.Property(e => e.RoomUid).HasColumnName("room_uid");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.EmergentRequests)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emergent_request_customer_id_fkey");

            entity.HasOne(d => d.Garage).WithMany(p => p.EmergentRequests)
                .HasForeignKey(d => d.GarageId)
                .HasConstraintName("emergent_request_garage_id_fkey");

            entity.HasOne(d => d.Place).WithMany(p => p.EmergentRequests)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emergent_request_place_id_fkey");
        });

        modelBuilder.Entity<EmergentRequestEvent>(entity =>
        {
            entity.HasKey(e => new { e.EmergentRequestId, e.EventId }).HasName("emergent_request_event_pk");

            entity.ToTable("emergent_request_event", "app");

            entity.Property(e => e.EmergentRequestId).HasColumnName("emergent_request_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.TsCreated)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("ts_created");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("event_type_pkey");

            entity.ToTable("event_type", "app");

            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EventName)
                .HasColumnType("character varying")
                .HasColumnName("event_name");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");
        });

        modelBuilder.Entity<Garage>(entity =>
        {
            entity.HasKey(e => e.GarageId).HasName("garage_pkey");

            entity.ToTable("garage", "app");

            entity.Property(e => e.GarageId).HasColumnName("garage_id");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.AvatarUrl)
                .HasColumnType("character varying")
                .HasColumnName("avatar_url");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Introduction)
                .HasColumnType("character varying")
                .HasColumnName("introduction");
            entity.Property(e => e.IntroductionUrl)
                .HasColumnType("character varying[]")
                .HasColumnName("introduction_url");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.OwnerId)
                .HasColumnType("character varying")
                .HasColumnName("owner_id");
            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");

            entity.HasOne(d => d.Place).WithMany(p => p.Garages)
                .HasForeignKey(d => d.PlaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("garage_place_id_fkey");
        });

        modelBuilder.Entity<GarageAccount>(entity =>
        {
            entity.HasKey(e => e.GarageAccountId).HasName("garage_account_pkey");

            entity.ToTable("garage_account", "app");

            entity.Property(e => e.GarageAccountId).HasColumnName("garage_account_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.GarageId).HasColumnName("garage_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsPrimaryAccount).HasColumnName("is_primary_account");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");

            entity.HasOne(d => d.Account).WithMany(p => p.GarageAccounts)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("garage_account_account_id_fkey");

            entity.HasOne(d => d.Garage).WithMany(p => p.GarageAccounts)
                .HasForeignKey(d => d.GarageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("garage_account_garage_id_fkey");
        });

        modelBuilder.Entity<GarageService>(entity =>
        {
            entity.HasKey(e => e.GarageServiceId).HasName("garage_service_pkey");

            entity.ToTable("garage_service", "app");

            entity.Property(e => e.GarageServiceId).HasColumnName("garage_service_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GarageId).HasColumnName("garage_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(255)
                .HasColumnName("service_name");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");

            entity.HasOne(d => d.Garage).WithMany(p => p.GarageServices)
                .HasForeignKey(d => d.GarageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("garage_service_garage_id_fkey");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("place_pkey");

            entity.ToTable("place", "app");

            entity.Property(e => e.PlaceId).HasColumnName("place_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Lat).HasColumnName("lat");
            entity.Property(e => e.Lng).HasColumnName("lng");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");
        });

        modelBuilder.Entity<PrismaMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("_prisma_migrations_pkey");

            entity.ToTable("_prisma_migrations");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.AppliedStepsCount)
                .HasDefaultValue(0)
                .HasColumnName("applied_steps_count");
            entity.Property(e => e.Checksum)
                .HasMaxLength(64)
                .HasColumnName("checksum");
            entity.Property(e => e.FinishedAt).HasColumnName("finished_at");
            entity.Property(e => e.Logs).HasColumnName("logs");
            entity.Property(e => e.MigrationName)
                .HasMaxLength(255)
                .HasColumnName("migration_name");
            entity.Property(e => e.RolledBackAt).HasColumnName("rolled_back_at");
            entity.Property(e => e.StartedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("started_at");
        });

        modelBuilder.Entity<ServiceSchedule>(entity =>
        {
            entity.HasKey(e => e.ServiceScheduleId).HasName("service_schedule_pkey");

            entity.ToTable("service_schedule", "app");

            entity.Property(e => e.ServiceScheduleId).HasColumnName("service_schedule_id");
            entity.Property(e => e.CheckInTime)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("check_in_time");
            entity.Property(e => e.CheckOutTime)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("check_out_time");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GarageId).HasColumnName("garage_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ServiceScheduleStatus)
                .HasMaxLength(50)
                .HasColumnName("service_schedule_status");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Garage).WithMany(p => p.ServiceSchedules)
                .HasForeignKey(d => d.GarageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_schedule_garage_id_fkey");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.ServiceSchedules)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_schedule_vehicle_id_fkey");
        });

        modelBuilder.Entity<SparePart>(entity =>
        {
            entity.HasKey(e => e.SparePartId).HasName("spare_part_pkey");

            entity.ToTable("spare_part", "app");

            entity.HasIndex(e => e.PartNumber, "spare_part_part_number_key").IsUnique();

            entity.Property(e => e.SparePartId).HasColumnName("spare_part_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .HasColumnName("manufacturer");
            entity.Property(e => e.PartName)
                .HasMaxLength(255)
                .HasColumnName("part_name");
            entity.Property(e => e.PartNumber)
                .HasMaxLength(50)
                .HasColumnName("part_number");
            entity.Property(e => e.QuantityInStock).HasColumnName("quantity_in_stock");
            entity.Property(e => e.SparePartCategoryId).HasColumnName("spare_part_category_id");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");

            entity.HasOne(d => d.SparePartCategory).WithMany(p => p.SpareParts)
                .HasForeignKey(d => d.SparePartCategoryId)
                .HasConstraintName("spare_part_spare_part_category_id_fkey");
        });

        modelBuilder.Entity<SparePartCategory>(entity =>
        {
            entity.HasKey(e => e.SparePartCategoryId).HasName("spare_part_category_pkey");

            entity.ToTable("spare_part_category", "app");

            entity.Property(e => e.SparePartCategoryId).HasColumnName("spare_part_category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("created_date");
            entity.Property(e => e.CreatedUser).HasColumnName("created_user");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("updated_date");
            entity.Property(e => e.UpdatedUser).HasColumnName("updated_user");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("vehicle_pkey");

            entity.ToTable("vehicle", "app");

            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
            entity.Property(e => e.ChassisNumber)
                .HasMaxLength(50)
                .HasColumnName("chassis_number");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .HasColumnName("color");
            entity.Property(e => e.EngineNumber)
                .HasMaxLength(50)
                .HasColumnName("engine_number");
            entity.Property(e => e.EstYear)
                .HasColumnType("timestamp(6) without time zone")
                .HasColumnName("est_year");
            entity.Property(e => e.Make)
                .HasMaxLength(50)
                .HasColumnName("make");
            entity.Property(e => e.Mileage).HasColumnName("mileage");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.PurchaseDate).HasColumnName("purchase_date");

            entity.HasOne(d => d.Owner).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicle_owner_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
