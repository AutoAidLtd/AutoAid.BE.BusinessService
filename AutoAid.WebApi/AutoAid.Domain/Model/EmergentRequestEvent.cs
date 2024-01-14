using System;
using System.Collections.Generic;

namespace AutoAid.Domain.Model;

public partial class EmergentRequestEvent
{
    public int EmergentRequestId { get; set; }

    public int EventId { get; set; }

    public DateTime TsCreated { get; set; }
}
