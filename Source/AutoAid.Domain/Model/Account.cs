using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("account", Schema = "ids")]
public partial class Account
{
    [Key]
    [Column("account_id")]
    public int AccountId { get; set; }

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

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("hash_password", TypeName = "character varying")]
    public string? HashPassword { get; set; }

    [Column("email", TypeName = "character varying")]
    public string? Email { get; set; }

    [Column("phone_number")]
    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [Column("access_token", TypeName = "character varying")]
    public string? AccessToken { get; set; }

    [Column("exp_access_token", TypeName = "timestamp(6) without time zone")]
    public DateTime? ExpAccessToken { get; set; }

    [Column("refresh_token", TypeName = "character varying")]
    public string? RefreshToken { get; set; }

    [Column("exp_refresh_tokenn", TypeName = "timestamp(6) without time zone")]
    public DateTime? ExpRefreshTokenn { get; set; }

    [Column("account_role")]
    [StringLength(50)]
    public string? AccountRole { get; set; }

    [InverseProperty("Account")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Account")]
    public virtual ICollection<GarageAccount> GarageAccounts { get; set; } = new List<GarageAccount>();
}
