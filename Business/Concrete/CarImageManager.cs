using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.FileHelperForLocalStorage;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage, List<IFormFile> files)
        {
            var carId = carImage.CarId;
            for (int i = 0; i < files.Count; i++)
            {
                var result = BusinessRules.Run(CheckCarImageCount(carImage.CarId));

                if (result != null)
                {
                    return result;
                }

                FileHelperForLocalStorage.Add(files[i], CreateNewPath(files[i],out var pathForDb));
                carImage.CarId = carId;
                carImage.ImagePath = pathForDb; 
                carImage.Date = DateTime.Now;
                _carImageDal.Add(carImage);
                carImage = new CarImage();
            }

            return new SuccessResult(Messages.ImageAddedSuccessfully);
        }


        public IResult Delete(CarImage carImage)
        {
            FileHelperForLocalStorage.Delete(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.ImageDeletedSuccessfully);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == id));
        }

        public IDataResult<List<CarImage>> GetImageForCarList()
        {

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetListByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(i => i.CarId == carId));
        }

        public IResult Update(CarImage carImage,IFormFile file)
        {
            var result = BusinessRules.Run(CheckCarImageCount(carImage.CarId));
            if (result != null)
            {
                return result;
            }

            var carImageForUpdate = _carImageDal.Get(i => i.Id == carImage.Id);
            carImage.CarId = carImageForUpdate.CarId;
            carImage.Date=DateTime.Now;
            FileHelperForLocalStorage.Update(carImageForUpdate.ImagePath, file, CreateNewPath(file, out var pathForDb));
            carImage.ImagePath = pathForDb;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.ImageUpdatedSuccessfully);
        }

        private IResult CheckCarImageCount(int carId)
        {
            var result = _carImageDal.GetAll(i => i.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.MaksimumImageLimitReached);
            }
            return new SuccessResult();
        }

        private string CreateNewPath(IFormFile file, out string pathForDb)
        {

            var fileInfo = new FileInfo(file.FileName);
            pathForDb=$@"{Guid.NewGuid()}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Year}_{DateTime.Now.Millisecond}{fileInfo.Extension}";
            var createdPathForHdd = $@"{Environment.CurrentDirectory}\wwwroot\CarImages\" + pathForDb;
           
            return createdPathForHdd;
        }

        //private CarImage MainPhotoMarker(CarImage carImage)
        //{

        //}
        
    }
}
