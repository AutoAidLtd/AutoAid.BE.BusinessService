using System;
using System.Collections.Generic;

namespace AutoAid.Domain.Model;

public partial class Garage
{
    public int GarageId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public string? AvatarUrl { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? Introduction { get; set; }

    public List<string>? IntroductionUrl { get; set; }

    public string? OwnerId { get; set; }

    public int PlaceId { get; set; }

    public virtual ICollection<EmergentRequest> EmergentRequests { get; set; } = new List<EmergentRequest>();

    public virtual ICollection<GarageAccount> GarageAccounts { get; set; } = new List<GarageAccount>();

    public virtual ICollection<GarageService> GarageServices { get; set; } = new List<GarageService>();

    public virtual Place Place { get; set; } = null!;

    public virtual ICollection<ServiceSchedule> ServiceSchedules { get; set; } = new List<ServiceSchedule>();
}
