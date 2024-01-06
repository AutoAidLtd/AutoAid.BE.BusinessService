using System;
using System.Collections.Generic;

namespace AutoAid.Infrastructure.Models;

public partial class ServiceSchedule
{
    public int ServiceScheduleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public int VehicleId { get; set; }

    public DateTime CheckInTime { get; set; }

    public DateTime CheckOutTime { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public int GarageId { get; set; }

    public string ServiceScheduleStatus { get; set; } = null!;

    public virtual Garage Garage { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
