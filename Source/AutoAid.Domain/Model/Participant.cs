using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[PrimaryKey("ChannelId", "UserId")]
[Table("participant", Schema = "app")]
public partial class Participant
{
    [Column("participant_id")]
    public int ParticipantId { get; set; }

    [Key]
    [Column("channel_id")]
    public int ChannelId { get; set; }

    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("joined_date", TypeName = "timestamp(6) without time zone")]
    public DateTime? JoinedDate { get; set; }

    [Column("updated_date", TypeName = "timestamp(6) without time zone")]
    public DateTime? UpdatedDate { get; set; }

    [ForeignKey("ChannelId")]
    [InverseProperty("Participants")]
    public virtual ChatChannel Channel { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("Participants")]
    public virtual Account User { get; set; } = null!;
}
