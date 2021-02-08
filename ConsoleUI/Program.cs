using System;
using Business.Concrete;
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

            Car car1 = new Car
            {
                BrandId = 2,
                ColorId = 1003,
                CarName = "Polo",
                DailyPrice = 450,
                ModelYear = 2017,
                Description = "Dizel, fiziksel kusur yok."
            };
            Brand brand1 = new Brand { Name = "Audi" };
            Color color1 = new Color { Name = "Mavi" };

            //brandManager.Add(brand1);
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

            //carManager.Add(car1);
            //carManager.Update(car1);
            //carManager.Delete(car1);

            //foreach (var car in carManager.GetCarsByColorId(2))
            //{
            //    Console.WriteLine("{0} --- {1} --- {2}--- {3}--- {4} --- {5} ", car.BrandId, car.ColorId, car.CarName, car.DailyPrice, car.ModelYear, car.Description);
            //}

            //Console.WriteLine(carManager.GetById(4).CarName);

            foreach (var carDetail in carManager.GetCarDetails())
            {
                Console.WriteLine("Araç adı: {0} --- Marka adı: {1} --- Renk: {2} --- Günlük kiralama ücreti: {3}",carDetail.CarName,carDetail.BrandName,carDetail.ColorName,carDetail.DailyPrice);
            }
            




            //InMemoryTest(carManager);

        }

        private static void InMemoryTest(CarManager carManager)
        {
            // GetAll();

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }

            //GetById();

            Console.WriteLine(carManager.GetById(2).Description);


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
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }

            //Add(),Update(),Delete();

            carManager.Add(carToAdd);
            //carManager.Update(carToUpdate);
            //carManager.Delete(carToDelete);

            Console.WriteLine("\nAdd / Update / Delete sonrası....\n");
            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }
        }
    }
}
