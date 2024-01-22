using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("place", Schema = "app")]
public partial class Place
{
    [Key]
    [Column("place_id")]
    public int PlaceId { get; set; }

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

    [Column("lat")]
    public double Lat { get; set; }

    [Column("lng")]
    public double Lng { get; set; }

    [InverseProperty("Place")]
    public virtual ICollection<EmergentRequest> EmergentRequests { get; set; } = new List<EmergentRequest>();

    [InverseProperty("Place")]
    public virtual ICollection<Garage> Garages { get; set; } = new List<Garage>();
}
