using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [SecuredOperation("rental.add,admin")]
        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Add(RentalDto rentalDto)
        {
            IResult result = BusinessRules.Run(CanACarBeRented(rentalDto));

            if (result != null)
            {
                return result;
            }

            _rentalDal.Add(rentalDto.Adapt<Rental>());
            return new SuccessResult(Messages.RentalAdded);
        }

        [CacheAspect]
        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<RentalWithDetailDto>> GetAllWithDetails()
        {
            return new SuccessDataResult<List<RentalWithDetailDto>>(_rentalDal.GetAllWithDetails());
        }

        [CacheAspect]
        public IDataResult<Rental> GetById(int RentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(c => c.RentalId == RentalId));
        }

        [CacheAspect]
        public IDataResult<Rental> GetLastRentalByCarId(RentalDto rentalDto)
        {
            return new SuccessDataResult<Rental>(_rentalDal.GetLastRentalByCarId(rentalDto.CarId));
        }

        private IResult CanACarBeRented(RentalDto rentalDto)
        {
            var anyNotReturnRental = _rentalDal.GetAll(c => c.CarId == rentalDto.CarId & c.ReturnDate == null).Any();
            if (anyNotReturnRental)
            {
                return new ErrorResult(Messages.CarRental); //Aracın kira kaydı var ve henüz geri dönmemiş
            }

            var rentalResult = GetLastRentalByCarId(rentalDto);
            if (rentalResult.Success)
            {
                if (rentalResult.Data == null)
                {
                    return new SuccessResult(); //Aracın hiç kira kaydı yok kiralanabilir
                }

                if (rentalResult.Data.ReturnDate == null)
                {
                    return new ErrorResult(Messages.CarRental); //Aracın kira kaydı var ve hala kirada
                }

                if (rentalDto.RentDate < DateTime.Now.AddDays(-1))
                {
                    return new ErrorResult(Messages.OldHistoryCanNotBeRecorded); //Bir gün öncesi veya daha eski tarih için kira kaydı yapılamaz
                }

                if (rentalDto.RentDate > DateTime.Now.AddDays(1))
                {
                    return new ErrorResult(Messages.CanNotRegisterForTheNextDate); //Bir gün sonrası veya daha ileri tarih için kira kaydı yapılamaz
                }
            }

            return rentalResult;
        }
    }
}