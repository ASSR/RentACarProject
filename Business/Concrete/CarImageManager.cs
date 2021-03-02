using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Mapster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public async Task<IResult> Add(CarImageDTO carImageDTO)
        {
            IResult result = BusinessRules.Run(CheckImageCountLimitExceeded(carImageDTO));

            if (result != null)
            {
                return result;
            }

            var carImageUploadResult = await CarImageUpload(carImageDTO);

            if (carImageUploadResult.Success)
            {
                _carImageDal.Add(carImageDTO.Adapt<CarImage>());
                return new SuccessResult(Messages.CarImageAdded);
            }

            return carImageUploadResult;
        }

        public async Task<IResult> Update(CarImageDTO carImageDTO)
        {
            IResult result = BusinessRules.Run(CheckImageCountLimitExceeded(carImageDTO));


            var carImageUploadResult = await CarImageUpload(carImageDTO);

            if (carImageUploadResult.Success)
            {
                _carImageDal.Update(carImageDTO.Adapt<CarImage>());
                return new SuccessResult(Messages.CarImageModified);
            }

            return carImageUploadResult;
        }

        public IResult Delete(int carImageId)
        {
            var carImage = _carImageDal.Get(c => c.CarImageId == carImageId);
            var deleteImageResult = CarImageDelete(carImage);

            if (deleteImageResult.Success)
            {
                _carImageDal.Delete(carImage);
            }

            return new SuccessResult(Messages.CarImageDeleted);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.CarImageId ==carImageId));
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var carImages = _carImageDal.GetAll(c => c.CarId == carId).ToList();
            if (carImages.Count > 0)
            {
                return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
            }

            foreach (var carImage in carImages)
            {
                carImage.ImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", "carRental.jpg");
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

        private IResult CheckImageCountLimitExceeded(CarImageDTO carImageDTO)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carImageDTO.CarId).Count;

            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageCountLimitExceeded);
            }

            return new SuccessResult();
        }

        private async Task<IDataResult<CarImage>> CarImageUpload(CarImageDTO carImageDTO){
            carImageDTO.Date = DateTime.Now;
            carImageDTO.ImagePath = Guid.NewGuid().ToString() + Path.GetExtension(carImageDTO.ImageFile.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", carImageDTO.ImagePath);

            if (carImageDTO.CarImageId != 0)
            {
                var oldCarImage = _carImageDal.Get(c => c.CarImageId == carImageDTO.CarImageId);
                var deleteImageResult = CarImageDelete(oldCarImage);

                if (!deleteImageResult.Success)
                {
                    return deleteImageResult;
                }
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await carImageDTO.ImageFile.CopyToAsync(stream);
            };

            return new SuccessDataResult<CarImage>(carImageDTO.Adapt<CarImage>());
        }

        private IDataResult<CarImage> CarImageDelete(CarImage carImage)
        {
            string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", carImage.ImagePath);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            return new SuccessDataResult<CarImage>(carImage);
        }
    }
}