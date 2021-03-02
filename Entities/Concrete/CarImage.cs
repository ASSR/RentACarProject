using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class CarImage:IEntity
    {
        public int CarImageId { get; set; }

        [Required]
        [StringLength(180, MinimumLength = 5)]
        public string ImagePath { get; set; }

        public DateTime? Date { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}