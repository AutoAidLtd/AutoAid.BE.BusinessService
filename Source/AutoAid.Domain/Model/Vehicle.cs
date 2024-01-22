using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("vehicle", Schema = "app")]
public partial class Vehicle
{
    [Key]
    [Column("vehicle_id")]
    public int VehicleId { get; set; }

    [Column("make")]
    [StringLength(50)]
    public string Make { get; set; } = null!;

    [Column("model")]
    [StringLength(50)]
    public string Model { get; set; } = null!;

    [Column("est_year", TypeName = "timestamp(6) without time zone")]
    public DateTime EstYear { get; set; }

    [Column("color")]
    [StringLength(30)]
    public string? Color { get; set; }

    [Column("owner_id")]
    public int OwnerId { get; set; }

    [Column("purchase_date")]
    public DateOnly? PurchaseDate { get; set; }

    [Column("mileage")]
    public int? Mileage { get; set; }

    [Column("engine_number")]
    [StringLength(50)]
    public string? EngineNumber { get; set; }

    [Column("chassis_number")]
    [StringLength(50)]
    public string? ChassisNumber { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("Vehicles")]
    public virtual Customer Owner { get; set; } = null!;

    [InverseProperty("Vehicle")]
    public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; } = new List<ServiceSchedule>();
}
