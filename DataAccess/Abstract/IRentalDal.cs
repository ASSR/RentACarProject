using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IRentalDal : IEntityRepository<Rental>
    {
        Rental GetLastRentalByCarId(int carId);
        List<RentalWithDetailDto> GetAllWithDetails();
    }
}