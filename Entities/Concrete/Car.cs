using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    public class Car:IEntity
    {
        public int CarId { get; set; }

        [Range(1900, 2050)]
        public int ModelYear { get; set; }

        [Range(0, 500)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(19, 2)")]
        public decimal DailyPrice { get; set; }

        [StringLength(180)]
        public string Description { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }

        public List<CarImage> CarImages { get; set; }
    }
}