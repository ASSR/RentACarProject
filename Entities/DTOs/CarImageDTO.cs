using Core.Entities;
using System;

namespace Entities.DTOs
{
    public class CarImageDto : IDTO
    {
        public int CarImageId { get; set; }
        public string ImagePath { get; set; }
        public DateTime? Date { get; set; }
        public int CarId { get; set; }
    }
}