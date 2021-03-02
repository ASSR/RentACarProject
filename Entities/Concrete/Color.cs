using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Color:IEntity
    {
        public int ColorId { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 2)]
        public string ColorName { get; set; }
    }
}