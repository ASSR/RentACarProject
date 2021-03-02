using Core.Entities;

namespace Entities.DTOs
{
    public class CarDetailDTO : IDTO
    {
        public int CarDetailId { get; set; }
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
    }
}