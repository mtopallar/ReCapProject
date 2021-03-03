using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using WebAPI.Helpers;
using WebApiFieUpload.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private static IWebHostEnvironment _webHostEnvironment;
        private ICarImageService _carImageService;
        private ICarPhotoFileHelper _carPhotoFileHelper;
        private ICarService _carService;

        public CarImagesController(IWebHostEnvironment webHostEnvironment, ICarImageService carImageService, ICarPhotoFileHelper carPhotoFileHelper, ICarService carService)
        {
            _webHostEnvironment = webHostEnvironment;
            _carImageService = carImageService;
            _carPhotoFileHelper = carPhotoFileHelper;
            _carService = carService;
        }


        [HttpPost("add")]
        //Postman'da post ederken Key olarak UploadedImage Value olarak resimler eklenecek. Çoklu eklemeye izin var. bir de carId değeri vermelisin.
        public string Add([FromForm] ImageForUpload imageForUploads, [FromForm] int carId)
        {
            if (carId==0)
            {
                return "Lütfen işlem yapmak istediğiniz aracı seçin.";
            }

            if (imageForUploads.UploadedImage != null)
            {
                _carPhotoFileHelper.AddImage(imageForUploads, carId);
            }
            return "Lütfen yüklemek istediğiniz fotoğraf(lar)ı seçin.";
            
        }

        [HttpPost("update")]
        public string Update([FromBody] CarImage carImage)
        {
            var imagesPerCar = _carImageService.GetListByCarId(carImage.CarId);
            var imageToUpdated = _carImageService.GetById(carImage.Id);

            if (imageToUpdated.Data == null)
            {
                return "Resim sistemde kayıtlı değil.";
            }

            imageToUpdated.Data.CarId = carImage.CarId;
            imageToUpdated.Data.Date = carImage.Date;
            if (imagesPerCar.Data.Count == 5)
            {
                return _carImageService.Update(imageToUpdated.Data).Message;
                //return "Bu araç için maksimum sayıda resim sistemde yüklü.";
            }
            var result = _carImageService.Update(imageToUpdated.Data);
            //_carPhotoFileHelper.RemoveDefaultImageWhileAnyImageAdded(carImage.CarId);
            return result.Message;
        }

        [HttpPost("delete")]
        public string Delete([FromBody] CarImage carImage)
        {
            var imageForDelete = _carImageService.GetById(carImage.Id);
            if (imageForDelete.Data == null)
            {
                return "Resim bulunamadı.";
            }
            string deletedImagePath = imageForDelete.Data.ImagePath;
            var tryDeleteImageFromDb = _carImageService.Delete(carImage);
            if (tryDeleteImageFromDb.Success)
            {
                System.IO.File.Delete(deletedImagePath);
            }

            return tryDeleteImageFromDb.Message;
        }
        //[HttpGet("getlistbycarid")]

        //public IDataResult<List<CarImage>> GetListByCarId(int carId)
        //{
        //    string imagePath = _webHostEnvironment.WebRootPath + @"\CarImages\";
        //    var checkIfCarExist = _carService.GetById(carId).Data;
        //    var imageCountByCar = _carImageService.GetListByCarId(carId);
        //    if (checkIfCarExist == null)
        //    {
        //        return new ErrorDataResult<List<CarImage>>("Araç bulunamadı");
        //    }
        //    if (imageCountByCar.Data.Count == 0)
        //    {
        //        var defaultCarImage = new CarImage
        //        {
        //            CarId = carId,
        //            ImagePath = imagePath + "CarRentalDefault.jpg",
        //            Date = DateTime.Now
        //        };
        //        List<CarImage> carImageList = new List<CarImage>();
        //        carImageList.Add(defaultCarImage);
        //        return new SuccessDataResult<List<CarImage>>(carImageList);
        //    }
        //    return imageCountByCar;
        //}

        [HttpGet("getimagebyid")]
        public IDataResult<CarImage> GetImage(int id)
        {
            var result = _carImageService.GetById(id);
            if (result.Data==null)
            {
                return new ErrorDataResult<CarImage>("Resim bulunamadı");
            }

            return result;
        }
    }
}

