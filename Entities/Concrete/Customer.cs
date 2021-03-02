using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Customer:IEntity
    {
        public int CustomerId { get; set; }

        [StringLength(120, MinimumLength = 2)]
        public string CompanyName { get; set; }
    }
}