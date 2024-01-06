using System;
using System.Collections.Generic;

namespace AutoAid.Infrastructure.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int CreatedUser { get; set; }

    public int UpdatedUser { get; set; }

    public bool IsDeleted { get; set; }

    public string Username { get; set; } = null!;

    public string? HashPassword { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? AccessToken { get; set; }

    public DateTime? ExpAccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? ExpRefreshTokenn { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<GarageAccount> GarageAccounts { get; set; } = new List<GarageAccount>();
}
