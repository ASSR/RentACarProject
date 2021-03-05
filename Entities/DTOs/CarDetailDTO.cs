using Core.Entities;

namespace Entities.DTOs
{
    public class CarDetailDto : IDTO
    {
        public int CarDetailId { get; set; }
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
    }
}