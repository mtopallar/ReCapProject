using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAddedSuccessfully);
            
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeletedSuccessfully);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.AllCarsListedSuccessfully);
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.GetCarByIdSuccessfully);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.GetCarDetailDtoSuccessfully);
        }



        /// burada oynuyorum
        public IDataResult<List<CarDetailsByCarIdDto>> GetCarDetailsByCarId(int carId)
        {
            return new SuccessDataResult<List<CarDetailsByCarIdDto>>(_carDal.GetCarDetailsByCarId(carId), Messages.GetCarDetailDtoSuccessfully);
        }
        /////

        
        public IDataResult<CarDetailsByCarIdWithDefaultPhotoDto> GetCarDetailsByCarIdWithDefaultImage(int carId, CarImage carImage)
        {
            return new SuccessDataResult<CarDetailsByCarIdWithDefaultPhotoDto>(
                _carDal.GetCarDetailsByCarIdWithDefaultImage(carId, carImage), Messages.GetCarDetailDtoSuccessfully);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId), Messages.GetCarsByBrandIdSuccessfully);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId), Messages.GetCarsByColorIdSuccessfully);
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdatedSuccessfully);
        }
    }
}
