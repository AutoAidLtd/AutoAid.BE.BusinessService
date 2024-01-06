using System;
using System.Collections.Generic;

namespace AutoAid.Infrastructure.Models;

public partial class SparePart
{
    public int SparePartId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public string PartName { get; set; } = null!;

    public string? PartNumber { get; set; }

    public string? Manufacturer { get; set; }

    public int? QuantityInStock { get; set; }

    public decimal? UnitPrice { get; set; }

    public int? SparePartCategoryId { get; set; }

    public virtual SparePartCategory? SparePartCategory { get; set; }
}
