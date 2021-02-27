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
using WebApiFieUpload.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private static IWebHostEnvironment _webHostEnvironment;
        private ICarImageService _carImageService;

        public CarImagesController(IWebHostEnvironment webHostEnvironment, ICarImageService carImageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _carImageService = carImageService;
        }


        [HttpPost("add")]
        //Postman'da post ederken Key olarak UploadedImage Value olarak resimler eklenecek. Çoklu eklemeye izin var. bir de carId değeri vermelisin.
        public string Add([FromForm] ImageForUpload imageForUploads, [FromForm] int carId)
        {
            var imagesPerCar = _carImageService.GetListByCarId(carId);
            #region ToDos
            // max resim sayısına ulaştıysa klasöre de kopyalamasın yapılacak. db ye yazmıyor ama klasöre fazla resimleri alıyor.
            // araç var ama resim yoksa default resmi kaydetsin. 
            #endregion
            try
            {
                if (imageForUploads.UploadedImage != null)
                {
                    if (imageForUploads.UploadedImage.Count > 0)
                    {
                        foreach (var imageForUpload in imageForUploads.UploadedImage)
                        {

                            if (imagesPerCar.Data.Count >= 5)
                            {
                                var result = _carImageService.Add(new CarImage { CarId = carId });
                                return result.Message;
                            }

                            string imagePath = _webHostEnvironment.WebRootPath + @"\CarImages\";
                            if (!Directory.Exists(imagePath))
                            {
                                Directory.CreateDirectory(imagePath);
                            }

                            var guIdName = Guid.NewGuid().ToString("N") + "_" + DateTime.Now.Second;
                            var fileExtension = new System.IO.FileInfo(imageForUpload.FileName).Extension;


                            if (imageForUpload.FileName == "CarRentalDefault" + fileExtension)
                            {
                                using (FileStream fileStream = System.IO.File.Create(imagePath + "CarRentalDefault" + fileExtension))
                                {
                                    imageForUpload.CopyTo(fileStream);
                                    fileStream.Flush();
                                    var carImageForDb = new CarImage { CarId = carId, ImagePath = imagePath + "CarRentalDefault" + fileExtension, Date = DateTime.Now };
                                    _carImageService.Add(carImageForDb);
                                }
                            }

                            else
                            {
                                using (FileStream fileStream = System.IO.File.Create(imagePath + guIdName + fileExtension))
                                {
                                    imageForUpload.CopyTo(fileStream);
                                    fileStream.Flush();
                                    var carImageForDb = new CarImage { CarId = carId, ImagePath = imagePath + guIdName + fileExtension, Date = DateTime.Now };
                                    _carImageService.Add(carImageForDb);
                                }
                            }
                        }

                    }
                    return "İzin verilen sayıda resim projeye dahil edildi.";
                }

                else
                {
                    return "Lütfen yüklenecek resmi seçin.";
                }

            }
            catch (Exception exception)
            {
                return exception.Message;
            }

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
        [HttpGet("getlistbycarid")]

        public IDataResult<List<CarImage>> GetListByCarId(int carId)
        {
            var result = _carImageService.GetListByCarId(carId);
            if (result.Data.Count==0)
            {
                return new ErrorDataResult<List<CarImage>>("Araca ait resim bulunmamaktadır.");
            }

            return result;
        }

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

