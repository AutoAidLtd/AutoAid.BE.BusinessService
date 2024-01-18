using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Domain.Model;

[Table("_prisma_migrations")]
public partial class PrismaMigration
{
    [Key]
    [Column("id")]
    [StringLength(36)]
    public string Id { get; set; } = null!;

    [Column("checksum")]
    [StringLength(64)]
    public string Checksum { get; set; } = null!;

    [Column("finished_at")]
    public DateTime? FinishedAt { get; set; }

    [Column("migration_name")]
    [StringLength(255)]
    public string MigrationName { get; set; } = null!;

    [Column("logs")]
    public string? Logs { get; set; }

    [Column("rolled_back_at")]
    public DateTime? RolledBackAt { get; set; }

    [Column("started_at")]
    public DateTime StartedAt { get; set; }

    [Column("applied_steps_count")]
    public int AppliedStepsCount { get; set; }
}
