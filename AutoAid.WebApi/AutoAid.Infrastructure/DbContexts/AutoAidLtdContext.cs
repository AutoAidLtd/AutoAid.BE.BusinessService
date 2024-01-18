using System;
using System.Collections.Generic;
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
        => optionsBuilder.UseNpgsql("Host=wyvernp.id.vn;Port=5432;Database=auto_aid_ltd;Username=sa;Password=ThanhPhong2506;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("account_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_pkey");

            entity.HasOne(d => d.Account).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("customer_account_id_fkey");
        });

        modelBuilder.Entity<EmergentRequest>(entity =>
        {
            entity.HasKey(e => e.EmergentRequestId).HasName("emergent_request_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Customer).WithMany(p => p.EmergentRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emergent_request_customer_id_fkey");

            entity.HasOne(d => d.Garage).WithMany(p => p.EmergentRequests).HasConstraintName("emergent_request_garage_id_fkey");

            entity.HasOne(d => d.Place).WithMany(p => p.EmergentRequests)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("emergent_request_place_id_fkey");
        });

        modelBuilder.Entity<EmergentRequestEvent>(entity =>
        {
            entity.HasKey(e => new { e.EmergentRequestId, e.EventId }).HasName("emergent_request_event_pk");

            entity.Property(e => e.TsCreated).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("event_type_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Garage>(entity =>
        {
            entity.HasKey(e => e.GarageId).HasName("garage_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Place).WithMany(p => p.Garages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("garage_place_id_fkey");
        });

        modelBuilder.Entity<GarageAccount>(entity =>
        {
            entity.HasKey(e => e.GarageAccountId).HasName("garage_account_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Account).WithMany(p => p.GarageAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("garage_account_account_id_fkey");

            entity.HasOne(d => d.Garage).WithMany(p => p.GarageAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("garage_account_garage_id_fkey");
        });

        modelBuilder.Entity<GarageService>(entity =>
        {
            entity.HasKey(e => e.GarageServiceId).HasName("garage_service_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Garage).WithMany(p => p.GarageServices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("garage_service_garage_id_fkey");
        });

        modelBuilder.Entity<Place>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("place_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<PrismaMigration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("_prisma_migrations_pkey");

            entity.Property(e => e.AppliedStepsCount).HasDefaultValue(0);
            entity.Property(e => e.StartedAt).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<ServiceSchedule>(entity =>
        {
            entity.HasKey(e => e.ServiceScheduleId).HasName("service_schedule_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Garage).WithMany(p => p.ServiceSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_schedule_garage_id_fkey");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.ServiceSchedules)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_schedule_vehicle_id_fkey");
        });

        modelBuilder.Entity<SparePart>(entity =>
        {
            entity.HasKey(e => e.SparePartId).HasName("spare_part_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.SparePartCategory).WithMany(p => p.SpareParts).HasConstraintName("spare_part_spare_part_category_id_fkey");
        });

        modelBuilder.Entity<SparePartCategory>(entity =>
        {
            entity.HasKey(e => e.SparePartCategoryId).HasName("spare_part_category_pkey");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("vehicle_pkey");

            entity.HasOne(d => d.Owner).WithMany(p => p.Vehicles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vehicle_owner_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
