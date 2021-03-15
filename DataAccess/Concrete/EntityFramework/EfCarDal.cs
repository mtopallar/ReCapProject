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
        

        public CarDetailsByCarIdDto GetCarDetailsByCarId(int carId)
        {

            using (ReCapContext context = new ReCapContext())
            {

                var result = from car in context.Cars.Where(c => c.Id == carId)
                             join color in context.Colors on car.ColorId equals color.Id
                             join brand in context.Brands on car.BrandId equals brand.Id
                             select new CarDetailsByCarIdDto
                             {
                                 BrandName = brand.Name,
                                 CarName = car.CarName,
                                 ColorName = color.Name,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 ModelYear = car.ModelYear,
                                 
                                 //DTO'daki List<CarImage> a burada atama yapmadım, çünkü araç resmi adedi kadar aracı tekrar tekrar dönüyor. Bu gereksiz birşey. Bunun yerine buradan dönen DTO'nun List<CarImage> propertysine Controllerda set ediyorum.
                             };

                return result.SingleOrDefault();
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
