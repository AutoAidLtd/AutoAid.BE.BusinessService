using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[PrimaryKey("EmergentRequestId", "EventId")]
[Table("emergent_request_event", Schema = "app")]
public partial class EmergentRequestEvent
{
    [Key]
    [Column("emergent_request_id")]
    public int EmergentRequestId { get; set; }

    [Key]
    [Column("event_id")]
    public int EventId { get; set; }

    [Column("ts_created", TypeName = "timestamp(6) without time zone")]
    public DateTime TsCreated { get; set; }
}
