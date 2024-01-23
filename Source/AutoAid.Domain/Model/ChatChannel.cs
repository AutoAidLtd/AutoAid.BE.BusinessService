using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("chat_channel", Schema = "app")]
public partial class ChatChannel
{
    [Key]
    [Column("channel_id")]
    public int ChannelId { get; set; }

    [Column("channel_name")]
    [StringLength(255)]
    public string ChannelName { get; set; } = null!;

    [Column("created_date", TypeName = "timestamp(6) without time zone")]
    public DateTime? CreatedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp(6) without time zone")]
    public DateTime? UpdatedDate { get; set; }

    [InverseProperty("Channel")]
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    [InverseProperty("Channel")]
    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}
