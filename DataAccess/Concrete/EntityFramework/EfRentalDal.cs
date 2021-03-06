﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal:EfEntityRepositoryBase<Rental,ReCapContext>,IRentalDal
    {
        public List<CarRentalDto> GetRentalDetailsList()
        {
            using (ReCapContext context = new ReCapContext())
            {
                var result = from rental in context.Rentals
                    join car in context.Cars on rental.CarId equals car.Id
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join customer in context.Customers on rental.CustomerId equals customer.Id
                    join user in context.Users on customer.UserId equals user.Id
                    select new CarRentalDto
                    {
                        BrandName = brand.Name,
                        CarName = car.CarName,
                        CustomerFullName = $"{user.FirstName} {user.LastName}",
                        RentDate = rental.RentDate,
                        ReturnDate = rental.ReturnDate
                    };
                return result.ToList();
            }
        }
    }
}
