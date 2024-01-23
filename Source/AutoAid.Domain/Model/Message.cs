using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("message", Schema = "app")]
public partial class Message
{
    [Key]
    [Column("message_id")]
    public int MessageId { get; set; }

    [Column("channel_id")]
    public int ChannelId { get; set; }

    [Column("sender_id")]
    public int SenderId { get; set; }

    [Column("content")]
    public string Content { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp(6) without time zone")]
    public DateTime? CreatedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp(6) without time zone")]
    public DateTime? UpdatedDate { get; set; }

    [ForeignKey("ChannelId")]
    [InverseProperty("Messages")]
    public virtual ChatChannel Channel { get; set; } = null!;
}
