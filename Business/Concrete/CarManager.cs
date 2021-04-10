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
        private readonly ICarDal _carDal;
        private readonly ICarImageService _carImageService;

        public CarManager(ICarDal carDal, ICarImageService carImageService)
        {
            _carDal = carDal;
            _carImageService = carImageService;
        }
        //[SecuredOperation("car.add,admin")]
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
            var carDetailDtoList = _carDal.GetCarsDetails();
            return new SuccessDataResult<List<CarDetailDto>>(MainImageAssigerForCarDetailDtos(carDetailDtoList), Messages.GetCarDetailDtoSuccessfully);
        }

        

        public IDataResult<List<CarDetailDto>> GetCarsDetailsByBrandId(int brandId)
        {
            var carDetailDtoListByBrandId = _carDal.GetCarsDetails(c => c.BrandId == brandId);
            return new SuccessDataResult<List<CarDetailDto>>(MainImageAssigerForCarDetailDtos(carDetailDtoListByBrandId),
                Messages.GetCarDetailsByBrandIdSuccessfully);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailsByColorId(int colorId)
        {
            var carDetailDtoListByColorId = _carDal.GetCarsDetails(c => c.ColorId == colorId);
            return new SuccessDataResult<List<CarDetailDto>>(MainImageAssigerForCarDetailDtos(carDetailDtoListByColorId)
                , Messages.GetCarDetailsByColorIdSuccessfully);
        }

        public IDataResult<List<CarDetailDto>> GetCarsDetailsByBrandIdAndColorId(int brandId, int colorId)
        {
            var carDetailDtoListByBrandIdAndColorId = _carDal.GetCarsDetails(c =>
                c.BrandId == brandId && c.ColorId == colorId);
            return new SuccessDataResult<List<CarDetailDto>>(MainImageAssigerForCarDetailDtos(carDetailDtoListByBrandIdAndColorId));
        }


        public IDataResult<CarDetailDtoWithoutImage> GetCarDetailsByCarId(int carId)
        {
            //var carDetailDto = _carDal.GetCarsDetails(c => c.CarId == carId).FirstOrDefault();
            //var carMainImage = _carImageService.GetCarMainImageByCarId(carDetailDto.CarId);
            //carDetailDto.MainImage = carMainImage==null ? new CarImage {ImagePath = "CarRentalDefault.jpg"} : carMainImage.Data;
            //return new SuccessDataResult<CarDetailDto>(carDetailDto, Messages.GetCarDetailDtoSuccessfully);
            return new SuccessDataResult<CarDetailDtoWithoutImage>(_carDal.GetCarsDetails(c=>c.CarId==carId).Single(), Messages.GetCarDetailDtoSuccessfully);
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

        private List<CarDetailDto> MainImageAssigerForCarDetailDtos(List<CarDetailDto> dtoListToCheck)
        {
            var carDetailDtoList = new List<CarDetailDto>();

            foreach (var carDetailDto in dtoListToCheck)
            {
                var mainImage = _carImageService.GetCarMainImageByCarId(carDetailDto.CarId);

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
                carDetailDtoList.Add(carDetail);
            }

            return carDetailDtoList;
        }
    }
}
