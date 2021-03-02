using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, CarRentalContext>, IRentalDal
    {
        public Rental GetRentalByCarId(int carId)
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
    }
}