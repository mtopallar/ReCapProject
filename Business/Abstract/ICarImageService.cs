﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(CarImage carImage);
        IResult Update(CarImage carImage);
        IResult Delete(CarImage carImage);
        IDataResult<List<CarImage>> GetAll();
        IDataResult<List<CarImage>> GetListByCarId(int carId);
    }
}