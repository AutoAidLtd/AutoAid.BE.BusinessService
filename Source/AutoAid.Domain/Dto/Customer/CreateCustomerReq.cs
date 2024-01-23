using System.ComponentModel.DataAnnotations;

namespace AutoAid.Domain.Dto.Customer
{
    public class CreateCustomerReq
    {
        //account
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = null!;
        //customer
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Gender { get; set; }
    }
}
