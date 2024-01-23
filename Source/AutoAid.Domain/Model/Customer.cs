using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("customer", Schema = "app")]
[Index("Email", Name = "customer_email_key", IsUnique = true)]
public partial class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    [Column("first_name")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("last_name")]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("phone_number")]
    [StringLength(20)]
    public string? PhoneNumber { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("date_of_birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Column("account_id")]
    public int AccountId { get; set; }

    [Column("avatar_url")]
    [StringLength(255)]
    public string? AvatarUrl { get; set; }

    [Column("gender")]
    [StringLength(25)]
    public string? Gender { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Customers")]
    public virtual Account Account { get; set; } = null!;

    [InverseProperty("Customer")]
    public virtual ICollection<EmergentRequest> EmergentRequests { get; set; } = new List<EmergentRequest>();

    [InverseProperty("Owner")]
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
