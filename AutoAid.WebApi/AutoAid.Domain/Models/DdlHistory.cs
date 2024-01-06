using System;
using System.Collections.Generic;
using System.Net;

namespace AutoAid.Infrastructure.Models;

public partial class DdlHistory
{
    public int Id { get; set; }

    public DateTime? ExecutionTime { get; set; }

    public string? CommandTag { get; set; }

    public string? ObjectType { get; set; }

    public string? ObjectName { get; set; }

    public IPAddress? ClientAddress { get; set; }

    public string? UserName { get; set; }

    public string? DdlStatement { get; set; }
}
