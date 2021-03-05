using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Business;
using Core.Utilities.FileHelper.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        IConfiguration _configuration;
        string carImageFolder;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper, IConfiguration configuration)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
            _configuration = configuration;
            carImageFolder = configuration.GetValue<string>("Folders:CarImage");
        }

        [PerformanceAspect(20)]
        public async Task<IResult> Add(IFormFile file, CarImageAddDto carImageAddDto)
        {
            IResult result = BusinessRules.Run(CheckImageCountLimitExceeded(carImageAddDto.CarId));

            if (result != null)
            {
                return result;
            }

            var carImageUploadResult = await CarImageFileAdd(file, carImageAddDto);

            if (carImageUploadResult.Success)
            {
                CarImage carImage = new CarImage
                {
                    Date = DateTime.Now,
                    CarId = carImageAddDto.CarId,
                    ImagePath = carImageUploadResult.Data.ImagePath
                };

                _carImageDal.Add(carImage);
                return new SuccessResult(Messages.CarImageAdded);
            }

            return carImageUploadResult;
        }

        [PerformanceAspect(20)]
        [TransactionScopeAspect]
        public async Task<IResult> Update(IFormFile file, CarImageUpdateDto carImageUpdateDto)
        {
            var carImage = _carImageDal.Get(c => c.CarImageId == carImageUpdateDto.CarImageId);

            IResult result = BusinessRules.Run(CheckImageCountLimitExceeded(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            var carImageDeleteResult = CarImageFileDelete(carImageUpdateDto.CarImageId);
            if (!carImageDeleteResult.Success)
            {
                return new ErrorResult(Messages.OldCarImageCouldNotBeDeleted);
            }

            var carImageUploadResult = await CarImageFileAdd(file, carImage.Adapt<CarImageAddDto>());

            if (carImageUploadResult.Success)
            {
                carImage.Date = DateTime.Now;
                carImage.ImagePath = carImageUploadResult.Data.ImagePath;
                _carImageDal.Update(carImage);
                return new SuccessResult(Messages.CarImageModified);
            }
            else
            {
                throw new Exception(Messages.NewCarImageCouldNotBeAdded);
            }

            //return carImageUploadResult;
        }

        public IResult Delete(CarImageUpdateDto carImageUpdateDto)
        {
            var deleteImageResult = CarImageFileDelete(carImageUpdateDto.CarImageId);

            if (deleteImageResult.Success)
            {
                _carImageDal.Delete(carImageUpdateDto.Adapt<CarImage>());
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
                carImage.ImagePath = Path.Combine(Directory.GetCurrentDirectory(), carImageFolder, "carRental.jpg");
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == carId));
        }

        private IResult CheckImageCountLimitExceeded(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;

            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageCountLimitExceeded);
            }

            return new SuccessResult();
        }

        private async Task<IDataResult<CarImage>> CarImageFileAdd(IFormFile file, CarImageAddDto carImageAddDto)
        {
            CarImage carImage = new CarImage();
            //var uploadImageResult = await _fileHelper.Upload(file, "wwwroot/CarImages");
            var uploadImageResult = await _fileHelper.Upload(file, carImageFolder);
            if (uploadImageResult.Success)
            {
                carImage.ImagePath = uploadImageResult.Message;
            }

            return new SuccessDataResult<CarImage>(carImage);
        }

        private IResult CarImageFileDelete(int carImageId)
        {
            var oldCarImage = _carImageDal.Get(c => c.CarImageId == carImageId);
            var deleteImageResult = _fileHelper.Delete(oldCarImage.ImagePath, carImageFolder);

            if (!deleteImageResult.Success)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}