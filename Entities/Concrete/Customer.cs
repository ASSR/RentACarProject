using Core.Entities;
using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Customer:IEntity
    {
        public int CustomerId { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        [StringLength(120, MinimumLength = 2)]
        public string CompanyName { get; set; }
    }
}