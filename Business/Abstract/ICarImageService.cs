using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        Task<IResult> Add(IFormFile file, CarImageAddDto carImageAddDto);
        Task<IResult> Update(IFormFile file, CarImageUpdateDto carImageUpdate);
        IResult Delete(CarImageUpdateDto carImageUpdateDto);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetById(int brandId);
        IDataResult<List<CarImage>> GetByCarId(int carId);
    }
}