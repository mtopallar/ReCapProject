using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImage carImage, IFormFile file);
        IResult Update(CarImage carImage,IFormFile file);
        IResult Delete(CarImage carImage);
        IDataResult<CarImage> GetCarMainImageByCarId(int carId);
        IDataResult<CarImage> GetById(int id);
        IDataResult<List<CarImage>> GetListByCarId(int carId);
    }
}
