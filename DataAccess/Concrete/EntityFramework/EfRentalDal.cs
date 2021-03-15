using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public Rental GetLastRentalByCarId(int carId)
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var rental = from r in context.Rentals
                            where r.CarId == carId
                            orderby r.RentDate descending
                            select r;
                return (Rental)rental.FirstOrDefault();
            }
        }

        public List<RentalWithDetailDto> GetAllWithDetails()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars on r.CarId equals c.CarId
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join cu in context.Customers on r.CustomerId equals cu.CustomerId
                             join u in context.Users on cu.UserId equals u.UserId
                             select new RentalWithDetailDto
                             {
                                 RentalId = r.RentalId,
                                 CarId = r.CarId,
                                 RentDate = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 BrandName = b.BrandName,
                                 CustomerId = r.CustomerId,
                                 CustomerFirstAndLastName = u.FirstName + ' ' + u.LastName
                             };
                return result.ToList();
            }
            
        }
    }
}