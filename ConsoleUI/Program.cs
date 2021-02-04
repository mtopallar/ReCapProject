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
                BrandId = 2, ColorId = 2, DailyPrice = 0, ModelYear = 2007,
                Description = "Dizel, 2010 model, krom kısımlar paslı"
            };
            Brand brand1 = new Brand {Name="Volkswagen",Id=2};
            Color color1 = new Color {Name="Siyah",Id=2};

            //colorManager.Add(color1);
            //brandManager.Add(brand1);
            //carManager.Add(car1);
            //brandManager.Update(brand1);
            //colorManager.Delete(color1);

            //foreach (var car in carManager.GetAll())
            //{
            //    Console.WriteLine("{0} --- {1} --- {2}--- {3}--- {4} ", car.BrandId, car.ColorId, car.DailyPrice, car.ModelYear, car.Description);
            //}

            //foreach (var brand in brandManager.GetAll())
            //{
            //    Console.WriteLine("{0} --- {1}", brand.Id, brand.Name);
            //}

            //foreach (var color in colorManager.GetAll())
            //{
            //    Console.WriteLine("{0} --- {1}", color.Id, color.Name);
            //}






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
                Id = 6, BrandId = 6, ColorId = 6, DailyPrice = 800, ModelYear = 2017,
                Description = "Herhangi bir kusur bulunmamaktadır. Eklendi"
            };
            Car carToUpdate = new Car
            {
                Id = 4, BrandId = 2, ColorId = 3, ModelYear = 2002, DailyPrice = 120,
                Description = "Krom kısımlar paslı.Güncellendi"
            };
            Car carToDelete = new Car
            {
                Id = 5, BrandId = 3, ColorId = 2, ModelYear = 2010, DailyPrice = 450,
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
