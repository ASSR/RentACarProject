using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        Task<IResult> Add(CarImageDTO carImageDTO);
        Task<IResult> Update(CarImageDTO carImageDTO);
        IResult Delete(CarImageDTO carImageDTO);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int brandId);
        IDataResult<List<CarImage>> GetByCarId(int carId);
    }
}