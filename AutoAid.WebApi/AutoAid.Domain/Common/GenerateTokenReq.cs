namespace AutoAid.Domain.Common
{
    public class GenerateTokenReq 
    {
        public string Id { get; set; }
        public string? Email { get; set; } = null;
        public string? FullName { get; set; } = null;
        public string? AvatarUrl { get; set; } = null;
        public string? Phone { get; set; } = null;
    }
}
