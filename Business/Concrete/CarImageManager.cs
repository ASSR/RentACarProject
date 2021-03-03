using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.FileHelper.Abstract;
using Core.Utilities.FileHelper.Concrete;
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
        IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }

        public async Task<IResult> Add(CarImageDTO carImageDTO)
        {
            IResult result = BusinessRules.Run(CheckImageCountLimitExceeded(carImageDTO));

            if (result != null)
            {
                return result;
            }

            var carImageUploadResult = await CarImageFileAdd(carImageDTO);

            if (carImageUploadResult.Success)
            {
                carImageDTO.Date = DateTime.Now;
                _carImageDal.Add(carImageDTO.Adapt<CarImage>());
                return new SuccessResult(Messages.CarImageAdded);
            }

            return carImageUploadResult;
        }

        public async Task<IResult> Update(CarImageDTO carImageDTO)
        {
            IResult result = BusinessRules.Run(CheckImageCountLimitExceeded(carImageDTO));

            var carImageUploadResult = await CarImageFileAdd(carImageDTO);

            if (carImageUploadResult.Success)
            {
                carImageDTO.Date = DateTime.Now;
                _carImageDal.Update(carImageDTO.Adapt<CarImage>());
                return new SuccessResult(Messages.CarImageModified);
            }

            return carImageUploadResult;
        }

        public IResult Delete(CarImageDTO carImageDTO)
        {
            //var carImage = _carImageDal.Get(c => c.CarImageId == carImageId);
            var deleteImageResult = CarImageFileDelete(carImageDTO);

            if (deleteImageResult.Success)
            {
                //var carImage = _carImageDal.Get(c => c.CarImageId == carImageDTO.CarImageId);
                _carImageDal.Delete(carImageDTO.Adapt<CarImage>());
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

        private async Task<IDataResult<CarImage>> CarImageFileAdd(CarImageDTO carImageDTO){
            var uploadImageResult = await _fileHelper.Upload(carImageDTO.ImageFile, "wwwroot/CarImages");
            if (uploadImageResult.Success)
            {
                carImageDTO.ImagePath = uploadImageResult.Data;
            }

            return new SuccessDataResult<CarImage>(carImageDTO.Adapt<CarImage>());
        }

        private async Task<IDataResult<CarImage>> CarImageFileUpdate(CarImageDTO carImageDTO)
        {
            var deleteImageResult = CarImageFileDelete(carImageDTO);
            if (!deleteImageResult.Success)
            {
                return new ErrorDataResult<CarImage>(carImageDTO.Adapt<CarImage>(), deleteImageResult.Message);
            }

            return await CarImageFileAdd(carImageDTO);
        }

        private IResult CarImageFileDelete(CarImageDTO carImageDTO)
        {
            var oldCarImage = _carImageDal.Get(c => c.CarImageId == carImageDTO.CarImageId);
            var deleteImageResult = _fileHelper.Delete(oldCarImage.ImagePath, "wwwroot/CarImages");

            if (!deleteImageResult.Success)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}