using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Rental:IEntity
    {
        public int RentalId { get; set; }

        [Required]
        public DateTime RentDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}