using System;
using Business.Concrete;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            // GetAll();

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }

            //GetById();

            Console.WriteLine(carManager.GetById(2).Description);


            Car carToAdd = new Car { Id = 6, BrandId = 6, ColorId = 6, DailyPrice = 800, ModelYear = 2017, Description = "Herhangi bir kusur bulunmamaktadır. Eklendi" };
            Car carToUpdate = new Car { Id = 4, BrandId = 2, ColorId = 3, ModelYear = 2002, DailyPrice = 120, Description = "Krom kısımlar paslı.Güncellendi" };
            Car carToDelete = new Car { Id = 5, BrandId = 3, ColorId = 2, ModelYear = 2010, DailyPrice = 450, Description = "Sağ ön kapıda ufak bir darbe mevcut." };


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
