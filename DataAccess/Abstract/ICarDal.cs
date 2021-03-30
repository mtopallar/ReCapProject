using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.Extensions.Logging;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<CarDetailDto> GetCarsDetails(Expression<Func<CarDetailDto,bool>> filter=null);
        //List<CarDetailDto> GetCarDetailsByBrandId(int brandId);
        //List<CarDetailDto> GetCarDetailsByColorId(int colorId);
        //CarDetailsByCarIdDto GetCarDetailsByCarId(int carId);
        
    }
}
