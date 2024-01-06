using System;
using System.Collections.Generic;

namespace AutoAid.Infrastructure.Models;

public partial class Vehicle
{
    public int VehicleId { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public DateTime EstYear { get; set; }

    public string? Color { get; set; }

    public int OwnerId { get; set; }

    public DateOnly? PurchaseDate { get; set; }

    public int? Mileage { get; set; }

    public string? EngineNumber { get; set; }

    public string? ChassisNumber { get; set; }

    public virtual Customer Owner { get; set; } = null!;

    public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; } = new List<ServiceSchedule>();
}
