using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("garage_account", Schema = "app")]
public partial class GarageAccount
{
    [Key]
    [Column("garage_account_id")]
    public int GarageAccountId { get; set; }

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

    [Column("garage_id")]
    public int GarageId { get; set; }

    [Column("account_id")]
    public int AccountId { get; set; }

    [Column("is_primary_account")]
    public bool? IsPrimaryAccount { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("GarageAccounts")]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey("GarageId")]
    [InverseProperty("GarageAccounts")]
    public virtual Garage Garage { get; set; } = null!;
}
