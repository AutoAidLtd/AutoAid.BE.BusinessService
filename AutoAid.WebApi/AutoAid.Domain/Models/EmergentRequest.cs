using System;
using System.Collections.Generic;

namespace AutoAid.Infrastructure.Models;

public partial class EmergentRequest
{
    public int EmergentRequestId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public string? Remark { get; set; }

    public int? VehicleId { get; set; }

    public int CustomerId { get; set; }

    public int GarageId { get; set; }

    public int PlaceId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Garage Garage { get; set; } = null!;

    public virtual Place Place { get; set; } = null!;
}
