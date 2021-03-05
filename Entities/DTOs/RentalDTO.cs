using Core.Entities;
using System;

namespace Entities.DTOs
{
    public class RentalDto : IDTO
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}