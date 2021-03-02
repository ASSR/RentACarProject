using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Brand:IEntity
    {
        public int BrandId { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 2)]
        public string BrandName { get; set; }
    }
}