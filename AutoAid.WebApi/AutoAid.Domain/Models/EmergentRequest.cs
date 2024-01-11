using System;
using System.Collections.Generic;

namespace AutoAid.Domain.Models;


public partial class EmergentRequest
{
    public int EmergentRequestId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int? CreatedUser { get; set; }

    public int? UpdatedUser { get; set; }

    public bool? IsDeleted { get; set; }

    public string? Remark { get; set; }

    public int? VehicleId { get; set; }

    public int CustomerId { get; set; }

    public int? GarageId { get; set; }

    public int PlaceId { get; set; }

    public string RoomUid { get; set; } = null!;

    public string Uid { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Garage? Garage { get; set; }

    public virtual Place Place { get; set; } = null!;
}
