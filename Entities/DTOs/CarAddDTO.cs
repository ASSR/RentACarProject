using Core.Entities;

namespace Entities.DTOs
{
    public class CarAddDTO:IDTO
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
    }
}