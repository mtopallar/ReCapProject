using System;
using System.Collections.Generic;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());

            UserManager userManager = new UserManager(new EfUserDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            RentalManager rentalManager = new RentalManager(new EfRentalDal());


            CarImageManager carImageManager = new CarImageManager(new EfCarImageDal());
            var carimages = carImageManager.GetListByCarId(1).Data;

            EfCarDal carDal = new EfCarDal();


            //Console.WriteLine(result.BrandName);
            //Console.WriteLine(result.ColorName);
            //Console.WriteLine(result.Description);
            //Console.WriteLine(result.DefaultImage.ImagePath);


            var result2 = carDal.GetCarDetailsByCarId(1);

            //Console.WriteLine(result2.CarName);
            //Console.WriteLine(result2.BrandName);
            //Console.WriteLine(result2.ColorName);


            //Console.WriteLine(result2.CarName);
            //Console.WriteLine(result2.BrandName);
            //Console.WriteLine(result2.ColorName);
            //Console.WriteLine(result2.CarImage.ImagePath);


            //for (int i = 0; i < 1; i++)
            //{

            //    foreach (var VARIABLE in result2)
            //    {
            //        foreach (var variableCarImage in VARIABLE.CarImages)
            //        {
            //            Console.WriteLine(variableCarImage.ImagePath);
            //        }
            //    }
            //}

            //foreach (var carDetailsByCarIdDto in result2)
            //{
            //    Console.WriteLine(carDetailsByCarIdDto.CarName);
            //    Console.WriteLine(carDetailsByCarIdDto.BrandName);
            //    Console.WriteLine(carDetailsByCarIdDto.ColorName);

            //    foreach (var carImage in carDetailsByCarIdDto.CarImages)
            //    {
            //        Console.WriteLine(carImage.ImagePath);
            //        Console.WriteLine(carImage.CarId);
            //        Console.WriteLine(carImage.Date);
            //        Console.WriteLine(carImage.Id);
            //    }
            //}



            User user = new User
            {
                FirstName = "Engin",
                LastName = "Demiroğ",
                Email = "engin@mail.com",
                //Password = "12345"
            };

            Customer customer = new Customer
            {
                UserId = 2,
                CompanyName = "Kodlama.io"
            };

            Rental rental = new Rental
            {
                //Id=4,
                CarId = 1,
                CustomerId = 1,
                RentDate = DateTime.Now,
                //ReturnDate=new DateTime(2021,02,26)
            };

            //var result = rentalManager.Add(rental);
            //Console.WriteLine(result.Message);


            Car car2 = new Car();
            car2.BrandId = 2003;
            car2.ColorId = 1;
            car2.CarName = "3.16i";
            car2.ModelYear = 2016;
            car2.DailyPrice = 850;
            car2.Description = "Benzinli. Fiziksel kusur yok.";

            Brand brand1 = new Brand { Name = "BMW" };
            Color color1 = new Color { Name = "Mavi" };

            //var result = brandManager.Add(brand1);

            //if (result.Success)
            //{
            //    Console.WriteLine(result.Message);

            //    foreach (var brand in brandManager.GetAll().Data)
            //    {
            //        Console.WriteLine(brand.Name);
            //    }
            //}
            //brandManager.Update(brand1);
            //brandManager.Delete(brand1);

            //foreach (var brand in brandManager.GetAll())
            //{
            //    Console.WriteLine("{0} --- {1}",brand.Id,brand.Name);
            //}

            //Console.WriteLine(brandManager.GetById(1003).Name);

            //colorManager.Add(color1);
            //colorManager.Update(color1);
            //colorManager.Delete(color1);

            //foreach (var color in colorManager.GetAll())
            //{
            //    Console.WriteLine("{0} --- {1}", color.Id, color.Name);
            //}

            //Console.WriteLine(colorManager.GetById(1003).Name);

            //carManager.Add(car2);
            //carManager.Update(car1);
            //carManager.Delete(car1);

            //foreach (var car in carManager.GetCarsByColorId(2))
            //{
            //    Console.WriteLine("{0} --- {1} --- {2}--- {3}--- {4} --- {5} ", car.BrandId, car.ColorId, car.CarName, car.DailyPrice, car.ModelYear, car.Description);
            //}

            //Console.WriteLine(carManager.GetById(4).CarName);
            //var result = carManager.Add(car2);

            //if (result.Success)
            //{
            //    Console.WriteLine(result.Message);

            //foreach (var carDetail in carManager.GetCarDetails().Data)
            //{
            //    var carlresimlist = carDetail.CarImages;

            //    Console.WriteLine("Araç adı: {0} --- Marka adı: {1} --- Renk: {2} --- Günlük kiralama ücreti: {3}", carDetail.CarName, carDetail.BrandName, carDetail.ColorName, carDetail.DailyPrice);
            //}

            //}
            //else
            //{
            //    Console.WriteLine(result.Message);

            //}









            //InMemoryTest(carManager);

        }

        private static void InMemoryTest(CarManager carManager)
        {
            // GetAll();

            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.Description);
            }

            //GetById();

            Console.WriteLine(carManager.GetById(2).Data.Description);


            Car carToAdd = new Car
            {
                Id = 6,
                BrandId = 6,
                ColorId = 6,
                DailyPrice = 800,
                ModelYear = 2017,
                Description = "Herhangi bir kusur bulunmamaktadır. Eklendi"
            };
            Car carToUpdate = new Car
            {
                Id = 4,
                BrandId = 2,
                ColorId = 3,
                ModelYear = 2002,
                DailyPrice = 120,
                Description = "Krom kısımlar paslı.Güncellendi"
            };
            Car carToDelete = new Car
            {
                Id = 5,
                BrandId = 3,
                ColorId = 2,
                ModelYear = 2010,
                DailyPrice = 450,
                Description = "Sağ ön kapıda ufak bir darbe mevcut."
            };


            Console.WriteLine("Add / Update / Delete öncesi....\n");
            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.Description);
            }

            //Add(),Update(),Delete();

            //carManager.Add(carToAdd);
            //carManager.Update(carToUpdate);
            //carManager.Delete(carToDelete);

            Console.WriteLine("\nAdd / Update / Delete sonrası....\n");
            foreach (var car in carManager.GetAll().Data)
            {
                Console.WriteLine(car.Description);
            }
        }
    }
}
