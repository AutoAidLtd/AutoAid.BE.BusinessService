using System;
using System.Collections.Generic;

namespace AutoAid.Domain.Model;

public partial class GarageService
{
    public int GarageServiceId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public string ServiceName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int GarageId { get; set; }

    public virtual Garage Garage { get; set; } = null!;
}
