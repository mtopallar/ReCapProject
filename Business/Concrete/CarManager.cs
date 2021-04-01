﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private ICarImageService _carImageService;

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
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

        public IDataResult<List<CarDetailDto>> GetCarsDetails()
        {
            var carDetailList = new List<CarDetailDto>();
            var allCars = _carDal.GetCarsDetails();
            
            foreach (var carDetailDto in allCars)
            {
                var mainImage = _carImageService.GetCarMainImageByCarId(carDetailDto.CarId);
                if (mainImage.Data==null)
                {
                    CarDetailDto carDetailWithDefault = new CarDetailDto
                    {
                        CarId = carDetailDto.CarId,
                        BrandId = carDetailDto.BrandId,
                        ColorId = carDetailDto.ColorId,
                        CarName = carDetailDto.CarName,
                        BrandName = carDetailDto.BrandName,
                        ColorName = carDetailDto.ColorName,
                        DailyPrice = carDetailDto.DailyPrice,
                        Description = carDetailDto.Description,
                        ModelYear = carDetailDto.ModelYear,
                        MainImage = new CarImage
                        {
                            ImagePath = "CarRentalDefault.jpg"
                        } 
                    };
                    carDetailList.Add(carDetailWithDefault);
                    
                }
                CarDetailDto carDetail = new CarDetailDto
                {
                    CarId = carDetailDto.CarId,
                    BrandId = carDetailDto.BrandId,
                    ColorId = carDetailDto.ColorId,
                    CarName = carDetailDto.CarName,
                    BrandName = carDetailDto.BrandName,
                    ColorName = carDetailDto.ColorName,
                    DailyPrice = carDetailDto.DailyPrice,
                    Description = carDetailDto.Description,
                    ModelYear = carDetailDto.ModelYear,
                    MainImage = mainImage.Data
                };
               carDetailList.Add(carDetail);
            }

            return new SuccessDataResult<List<CarDetailDto>>(carDetailList, Messages.GetCarDetailDtoSuccessfully);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails(c => c.BrandId == brandId),
                Messages.GetCarDetailsByBrandIdSuccessfully);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(
                _carDal.GetCarsDetails(c => c.ColorId == colorId), Messages.GetCarDetailsByColorIdSuccessfully);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailsByBrandIdAndColorId(int brandId, int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarsDetails(c =>
                c.BrandId == brandId && c.ColorId == colorId));
        }


        public IDataResult<CarDetailDto> GetCarDetailsByCarId(int carId)
        {
            return new SuccessDataResult<CarDetailDto>(_carDal.GetCarsDetails(c => c.CarId == carId).FirstOrDefault(), Messages.GetCarDetailDtoSuccessfully);
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
