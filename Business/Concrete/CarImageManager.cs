using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarImageCount(carImage.CarId));
            if (result != null)
            {
                return result;
            }
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.ImageAddedSuccessfully);
        }

        public IResult Delete(CarImage carImage)
        {
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.ImageDeletedSuccessfully);
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetListByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(i => i.CarId == carId));

            //string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName + @"\CarImages\default.jpg");

            //var result = _carImageDal.GetAll(c => c.CarId == carId).Any();

            //if (!result)
            //{
            //    return new SuccessDataResult<List<CarImage>>(new List<CarImage>{new CarImage {CarId = carId, ImagePath = path, AddedDate = DateTime.Now }});
            //}
            //return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(i => i.CarId == carId));
        }

        public IResult Update(CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarImageCount(carImage.CarId));
            if (result != null)
            {
                return result;
            }
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

        ////////////////////////////////////////////////////////////////////

        //private List<CarImage> CheckIfCarImageNull(int id)
        //{
        //    string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName + @"\CarImages\default.jpg");
        //    var result = _carImageDal.GetAll(c => c.CarId == id).Any();




        //    if (!result)
        //    {
        //        return new List<CarImage> { new CarImage { CarId = id, ImagePath = path, AddedDate = DateTime.Now } };
        //    }
        //    return _carImageDal.GetAll(p => p.CarId == id);
        //}

        private IDataResult<CarImage> CreatedFile(CarImage carImage, string extension)
        {

            string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName + @"\CarImages");

            var creatingUniqueFilename = Guid.NewGuid().ToString("D")
                + "_" + DateTime.Now.Month + "_"
                + DateTime.Now.Day + "_"
                + DateTime.Now.Year + extension;

            string source = Path.Combine(carImage.ImagePath);

            string result = $@"{path}\{creatingUniqueFilename}";

            try
            {

                File.Move(source, path + @"\" + creatingUniqueFilename);
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<CarImage>(exception.Message);
            }

            return new SuccessDataResult<CarImage>(new CarImage
            {
                Id = carImage.Id, CarId = carImage.CarId, ImagePath = result, Date = DateTime.Now
            }); // Messages.ImagesAdded);
        }

        private IDataResult<CarImage> UpdatedFile(CarImage carImage)
        {
            var creatingUniqueFilename = Guid.NewGuid().ToString("N") + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Year + ".jpeg";

            string path = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).FullName + @"\Images");

            string result = $"{path}\\{creatingUniqueFilename}";

            File.Copy(carImage.ImagePath, path + "\\" + creatingUniqueFilename);

            File.Delete(carImage.ImagePath);

            return new SuccessDataResult<CarImage>(new CarImage { Id = carImage.Id, CarId = carImage.CarId, ImagePath = result, Date = DateTime.Now });
        }

        private IResult CarImageDelete(CarImage carImage)
        {
            try
            {
                File.Delete(carImage.ImagePath);
            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }

            return new SuccessResult();
        }
    }
}
