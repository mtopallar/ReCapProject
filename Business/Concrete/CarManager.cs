using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
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
        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]//sadece "Get" yazarsak bellekteki tüm getleri siler. (Car için color için brand için tüm servislerdeki tüm getlerin cachelerini siler.)
        [TransactionScopeAspect]
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
        [CacheAspect] // key = Business.Concrete.CarManager.GetAll
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.AllCarsListedSuccessfully);
        }
        
        public IDataResult<Car> GetById(int id) // key = Business.Concrete.CarManager.GetById(idnin değeri)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id), Messages.GetCarByIdSuccessfully);
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.GetCarDetailDtoSuccessfully);
        }



        public IDataResult<CarDetailsByCarIdDto> GetCarDetailsByCarId(int carId)
        {
            return new SuccessDataResult<CarDetailsByCarIdDto>(_carDal.GetCarDetailsByCarId(carId), Messages.GetCarDetailDtoSuccessfully);
        }
        

        
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
