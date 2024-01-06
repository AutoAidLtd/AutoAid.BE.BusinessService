using System;
using System.Collections.Generic;

namespace AutoAid.Infrastructure.Models;

public partial class SparePartCategory
{
    public int SparePartCategoryId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<SparePart> SpareParts { get; set; } = new List<SparePart>();
}
