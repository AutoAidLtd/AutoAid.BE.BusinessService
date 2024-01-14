using System;
using System.Collections.Generic;

namespace AutoAid.Domain.Model;

public partial class Place
{
    public int PlaceId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public double Lat { get; set; }

    public double Lng { get; set; }

    public virtual ICollection<EmergentRequest> EmergentRequests { get; set; } = new List<EmergentRequest>();

    public virtual ICollection<Garage> Garages { get; set; } = new List<Garage>();
}
