using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        IDataResult<Car> GetById(int id);
        IDataResult<List<Car>> GetAll();
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarDetailsByCarIdDto>> GetCarDetailsByCarId(int carId);
        IDataResult<CarDetailsByCarIdWithDefaultPhotoDto> GetCarDetailsByCarIdWithDefaultImage(int carId, CarImage carImage);
        IResult Add(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);
    }
}
