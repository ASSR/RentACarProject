using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        Task<IResult> Add(CarImageDTO carImageDTO);
        Task<IResult> Update(CarImageDTO carImageDTO);
        IResult Delete(int carImageId);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int brandId);
        IDataResult<List<CarImage>> GetByCarId(int carId);
    }
}