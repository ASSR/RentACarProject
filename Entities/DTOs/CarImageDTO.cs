using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CarImageDTO : IDTO
    {
        public int CarImageId { get; set; }
        public string ImagePath { get; set; }
        public IFormFile ImageFile { get; set; }
        public DateTime? Date { get; set; }
        public int CarId { get; set; }
    }
}