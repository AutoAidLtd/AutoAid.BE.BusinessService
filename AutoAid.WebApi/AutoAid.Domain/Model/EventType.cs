using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("event_type", Schema = "app")]
public partial class EventType
{
    [Key]
    [Column("event_type_id")]
    public int EventTypeId { get; set; }

    [Column("created_date", TypeName = "timestamp(6) without time zone")]
    public DateTime CreatedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp(6) without time zone")]
    public DateTime UpdatedDate { get; set; }

    [Column("created_user")]
    public int CreatedUser { get; set; }

    [Column("updated_user")]
    public int UpdatedUser { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("event_name", TypeName = "character varying")]
    public string EventName { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }
}
