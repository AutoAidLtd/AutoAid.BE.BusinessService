using System.ComponentModel.DataAnnotations;

namespace AutoAid.Domain.Dto.Garage
{
    public class CreateGarageReq
    {
        //account
        public string Username { get; set; } = null!;
        public string? Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; } = null!;
        // garage
        public string? AvatarUrl { get; set; }
        [Required]
        public string? Address { get; set; }
        public string? Introduction { get; set; }
        public List<string>? IntroductionUrl { get; set; }
        [Required]
        public double Lat { get; set; }
        [Required]
        public double Lng { get; set; }
    }
}
