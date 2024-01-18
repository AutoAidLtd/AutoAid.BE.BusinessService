using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("spare_part", Schema = "app")]
[Index("PartNumber", Name = "spare_part_part_number_key", IsUnique = true)]
public partial class SparePart
{
    [Key]
    [Column("spare_part_id")]
    public int SparePartId { get; set; }

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

    [Column("part_name")]
    [StringLength(255)]
    public string PartName { get; set; } = null!;

    [Column("part_number")]
    [StringLength(50)]
    public string? PartNumber { get; set; }

    [Column("manufacturer")]
    [StringLength(100)]
    public string? Manufacturer { get; set; }

    [Column("quantity_in_stock")]
    public int? QuantityInStock { get; set; }

    [Column("unit_price")]
    [Precision(10, 2)]
    public decimal? UnitPrice { get; set; }

    [Column("spare_part_category_id")]
    public int? SparePartCategoryId { get; set; }

    [ForeignKey("SparePartCategoryId")]
    [InverseProperty("SpareParts")]
    public virtual SparePartCategory? SparePartCategory { get; set; }
}
