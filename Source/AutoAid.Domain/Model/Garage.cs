using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("garage", Schema = "app")]
public partial class Garage
{
    [Key]
    [Column("garage_id")]
    public int GarageId { get; set; }

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

    [Column("avatar_url", TypeName = "character varying")]
    public string? AvatarUrl { get; set; }

    [Column("email", TypeName = "character varying")]
    public string? Email { get; set; }

    [Column("address", TypeName = "character varying")]
    public string? Address { get; set; }

    [Column("introduction", TypeName = "character varying")]
    public string? Introduction { get; set; }

    [Column("introduction_url", TypeName = "character varying[]")]
    public List<string>? IntroductionUrl { get; set; }

    [Column("owner_id", TypeName = "character varying")]
    public string? OwnerId { get; set; }

    [Column("place_id")]
    public int PlaceId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string? Name { get; set; }

    [InverseProperty("Garage")]
    public virtual ICollection<EmergentRequest> EmergentRequests { get; set; } = new List<EmergentRequest>();

    [InverseProperty("Garage")]
    public virtual ICollection<GarageAccount> GarageAccounts { get; set; } = new List<GarageAccount>();

    [InverseProperty("Garage")]
    public virtual ICollection<GarageService> GarageServices { get; set; } = new List<GarageService>();

    [ForeignKey("PlaceId")]
    [InverseProperty("Garages")]
    public virtual Place Place { get; set; } = null!;

    [InverseProperty("Garage")]
    public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; } = new List<ServiceSchedule>();
}
