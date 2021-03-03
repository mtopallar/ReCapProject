using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from car in context.Cars
                             join color in context.Colors on car.ColorId equals color.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             select new CarDetailDto
                             {
                                 CarName = car.CarName,
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 DailyPrice = car.DailyPrice
                             };
                return result.ToList();
            }

        }

        //Expression<Func<Car, bool>> filter = null kullanılabilir. Ama id verilmezse tüm araçların resimleri aynı anda döner.

        public List<CarDetailsByCarIdDto> GetCarDetailsByCarId(int carId)
        {

            using (ReCapContext context = new ReCapContext())
            {
                
                var result = from car in context.Cars.Where(c => c.Id == carId)
                                 join color in context.Colors on car.ColorId equals color.Id
                                 join brand in context.Brands on car.BrandId equals brand.Id
                                 join image in context.CarImages on car.Id equals image.CarId
                                 select new CarDetailsByCarIdDto
                                 {

                                     BrandName = brand.Name,
                                     CarName = car.CarName,
                                     ColorName = color.Name,
                                     DailyPrice = car.DailyPrice,
                                     Description = car.Description,
                                     ModelYear = car.ModelYear,
                                     CarImages = new List<CarImage>
                                     {
                                         new CarImage
                                         {
                                             CarId = image.CarId,
                                             Date = image.Date,
                                             Id = image.Id,
                                             ImagePath = image.ImagePath

                                         }
                                     }
                                 };

                    return result.ToList();
            }

        }
        
        public CarDetailsByCarIdWithDefaultPhotoDto GetCarDetailsByCarIdWithDefaultImage(int carId, CarImage carImage)
        {
            using (ReCapContext context = new ReCapContext())
            {
                var defaultresult = from car in context.Cars.Where(c => c.Id == carId)
                join color in context.Colors on car.ColorId equals color.Id
                join brand in context.Brands on car.BrandId equals brand.Id
                select new CarDetailsByCarIdWithDefaultPhotoDto
                {

                    BrandName = brand.Name,
                    CarName = car.CarName,
                    ColorName = color.Name,
                    DailyPrice = car.DailyPrice,
                    Description = car.Description,
                    ModelYear = car.ModelYear,
                    DefaultImage = 
                        new CarImage
                        {
                            ImagePath = carImage.ImagePath
                        }
                };
                return defaultresult.Single();
            }

        }
    }
}
