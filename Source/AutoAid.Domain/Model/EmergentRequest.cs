using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("emergent_request", Schema = "app")]
public partial class EmergentRequest
{
    [Key]
    [Column("emergent_request_id")]
    public int EmergentRequestId { get; set; }

    [Column("created_date", TypeName = "timestamp(6) without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp(6) without time zone")]
    public DateTime UpdatedDate { get; set; }

    [Column("created_user")]
    public int? CreatedUser { get; set; }

    [Column("updated_user")]
    public int? UpdatedUser { get; set; }

    [Column("is_deleted")]
    public bool? IsDeleted { get; set; }

    [Column("remark")]
    [StringLength(255)]
    public string? Remark { get; set; }

    [Column("vehicle_id")]
    public int? VehicleId { get; set; }

    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("garage_id")]
    public int? GarageId { get; set; }

    [Column("place_id")]
    public int PlaceId { get; set; }

    [Column("room_uid")]
    public string RoomUid { get; set; } = null!;

    [Column("uid")]
    public string Uid { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("EmergentRequests")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("GarageId")]
    [InverseProperty("EmergentRequests")]
    public virtual Garage? Garage { get; set; }

    [ForeignKey("PlaceId")]
    [InverseProperty("EmergentRequests")]
    public virtual Place Place { get; set; } = null!;
}
