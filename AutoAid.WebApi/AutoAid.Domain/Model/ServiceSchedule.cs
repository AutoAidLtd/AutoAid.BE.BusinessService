using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("service_schedule", Schema = "app")]
public partial class ServiceSchedule
{
    [Key]
    [Column("service_schedule_id")]
    public int ServiceScheduleId { get; set; }

    [Column("created_date", TypeName = "timestamp(6) without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp(6) without time zone")]
    public DateTime UpdatedDate { get; set; }

    [Column("created_user")]
    public int CreatedUser { get; set; }

    [Column("updated_user")]
    public int UpdatedUser { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("vehicle_id")]
    public int VehicleId { get; set; }

    [Column("check_in_time", TypeName = "timestamp(6) without time zone")]
    public DateTime CheckInTime { get; set; }

    [Column("check_out_time", TypeName = "timestamp(6) without time zone")]
    public DateTime CheckOutTime { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    [Precision(10, 2)]
    public decimal? Price { get; set; }

    [Column("garage_id")]
    public int GarageId { get; set; }

    [Column("service_schedule_status")]
    [StringLength(50)]
    public string ServiceScheduleStatus { get; set; } = null!;

    [ForeignKey("GarageId")]
    [InverseProperty("ServiceSchedules")]
    public virtual Garage Garage { get; set; } = null!;

    [ForeignKey("VehicleId")]
    [InverseProperty("ServiceSchedules")]
    public virtual Vehicle Vehicle { get; set; } = null!;
}
