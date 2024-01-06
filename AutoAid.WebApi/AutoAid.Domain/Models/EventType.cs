using System;
using System.Collections.Generic;

namespace AutoAid.Infrastructure.Models;

public partial class EventType
{
    public int EventTypeId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public string EventName { get; set; } = null!;

    public string? Description { get; set; }
}
