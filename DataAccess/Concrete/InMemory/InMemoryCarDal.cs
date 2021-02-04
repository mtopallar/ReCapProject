using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id=1,BrandId=1,ColorId=1,ModelYear=2001,DailyPrice=100,Description="Gövdede ufak paslanmalar mecvut."},
                new Car{Id=2,BrandId=1,ColorId=2,ModelYear=2005,DailyPrice=320,Description="Dikiz aynasında ufak bir çatlak var."},
                new Car{Id=3,BrandId=2,ColorId=1,ModelYear=2007,DailyPrice=375,Description="Ön çamurlukarın biri yırtık."},
                new Car{Id=4,BrandId=2,ColorId=3,ModelYear=2002,DailyPrice=120,Description="Krom kısımlar paslı."},
                new Car{Id=5,BrandId=3,ColorId=2,ModelYear=2010,DailyPrice=450,Description="Sağ ön kapıda ufak bir darbe mevcut."}
            };
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            var carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            //Ef'ye geçildi.
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            //Ef'ye geçildi.
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c => c.Id == id);

        }

        public void Update(Car car)
        {
            var carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            carToUpdate.ModelYear = car.ModelYear;
        }
    }
}
