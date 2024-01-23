using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("notification", Schema = "app")]
public partial class Notification
{
    [Key]
    [Column("uuid")]
    public Guid Uuid { get; set; }

    [Column("content")]
    public string? Content { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string? Title { get; set; }

    [Column("noti_type")]
    [StringLength(50)]
    public string? NotiType { get; set; }

    [Column("send_time", TypeName = "timestamp(6) without time zone")]
    public DateTime? SendTime { get; set; }

    [Column("receive_id")]
    public int? ReceiveId { get; set; }

    [Column("created_date", TypeName = "timestamp(6) without time zone")]
    public DateTime? CreatedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp(6) without time zone")]
    public DateTime? UpdatedDate { get; set; }

    [ForeignKey("ReceiveId")]
    [InverseProperty("Notifications")]
    public virtual Account? Receive { get; set; }
}
