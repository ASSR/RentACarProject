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

        public IResult Add(RentalDTO rentalDTO)
        {
            IResult result = BusinessRules.Run(CanACarBeRented(rentalDTO));

            if (result != null)
            {
                return result;
            }

            _rentalDal.Add(rentalDTO.Adapt<Rental>());
            return new SuccessResult(Messages.RentalAdded);
        }

        private IResult CanACarBeRented(RentalDTO rentalDTO)
        {
            var rentalResult = GetRentalByCarId(rentalDTO);
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

                if (rentalDTO.RentDate < DateTime.Now.AddDays(-1))
                {
                    return new ErrorResult(Messages.OldHistoryCanNotBeRecorded); //Bir gün öncesi veya daha eski tarih için kira kaydı yapılamaz
                }

                if (rentalDTO.RentDate > DateTime.Now.AddDays(1))
                {
                    return new ErrorResult(Messages.CanNotRegisterForTheNextDate); //Bir gün sonrası veya daha ileri tarih için kira kaydı yapılamaz
                }
            }

            return rentalResult;
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int RentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(c => c.RentalId == RentalId));
        }

        public IDataResult<Rental> GetRentalByCarId(RentalDTO rentalDTO)
        {
            return new SuccessDataResult<Rental>(_rentalDal.GetRentalByCarId(rentalDTO.CarId));
        }
    }
}