using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        //DateTime'ın default değeri null değil.
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(CheckRentability(rental));
            if (result!= null)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RantalAddedSuccessfully);
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(Messages.RentalDeletedSuccessfully);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.GetAllRentalsSuccessfully);
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id), Messages.GetRentalByIdSuccessfully);
        }

        public IDataResult<List<CarRentalDto>> GetRentalDetailsList()
        {
            return new SuccessDataResult<List<CarRentalDto>>(_rentalDal.GetRentalDetailsList(),
                Messages.RentDetailsListedSuccessfully);
        }

        public IResult Update(Rental rental)
        {
            var result = BusinessRules.Run(CheckRentability(rental));
            if (result!= null)
            {
                return result;
            }
            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdatedSuccessfully);
        }

        public IResult CheckRentability(Rental rental)
        {
            var rentals = _rentalDal.GetAll(r => r.CarId == rental.CarId);
            if (rentals.Any(r=>r.ReturnDate>=rental.RentDate && r.RentDate<=rental.ReturnDate))
            {
                return new ErrorResult(Messages.RentalDateError);
            }

            return new SuccessResult(Messages.CarIsRentable);
        }
    }
}
