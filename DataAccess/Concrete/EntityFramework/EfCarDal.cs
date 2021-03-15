using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, CarRentalContext>, ICarDal
    {
        public List<CarWithBrandAndColorDto> GetAllWithBrandAndColor()
        {
            using (CarRentalContext context = new CarRentalContext())
            {
                var result = from c in context.Cars
                           join b in context.Brands on c.BrandId equals b.BrandId
                           join co in context.Colors on c.ColorId equals co.ColorId
                           select new CarWithBrandAndColorDto {
                                CarId = c.CarId,
                                ModelYear = c.ModelYear,
                                DailyPrice = c.DailyPrice,
                                Description = c.Description,
                                BrandId = c.BrandId,
                                BrandName = b.BrandName,
                                ColorId = c.ColorId,
                                ColorName = co.ColorName
                           };

                return result.ToList();
            }
        }
    }
}