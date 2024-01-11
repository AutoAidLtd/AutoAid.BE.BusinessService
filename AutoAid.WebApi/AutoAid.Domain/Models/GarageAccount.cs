using AutoAid.Domain.Models;
using System;
using System.Collections.Generic;

namespace AutoAid.Domain.Models;


public partial class GarageAccount
{
    public int GarageAccountId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public int GarageId { get; set; }

    public int AccountId { get; set; }

    public bool? IsPrimaryAccount { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Garage Garage { get; set; } = null!;
}
