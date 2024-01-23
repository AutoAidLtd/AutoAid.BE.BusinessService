using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutoAid.Domain.Dto.GarageService
{
    public class CreateGarageServiceReq
    {
        [StringLength(255)]
        public string ServiceName { get; set; } = null!;

        public string? Description { get; set; }

        [Precision(10, 2)]
        public decimal? Price { get; set; }

        public int GarageId { get; set; }
    }
}
