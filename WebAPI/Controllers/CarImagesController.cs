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

        private string CheckCarImageByCarId(int carId)
        {
            var imagesPerCar = _carImageService.GetListByCarId(carId);

            if (imagesPerCar.Data.Count >= 5)
            {
                return imagesPerCar.Message;
            }

            return (5 - (imagesPerCar.Data.Count)).ToString() + " adet resim sisteme eklendi";
        }
        [HttpPost]
        //Postman'da post ederken Key olarak UploadedImage Value olarak resimler eklenecek. Çoklu eklemeye izin var. bir de carId değeri vermelisin.
        public string Add([FromForm] ImageForUpload imageForUploads, [FromForm] int carId)
        {
            var imagesPerCar = _carImageService.GetListByCarId(carId);
            
            try
            {
                if (imageForUploads.UploadedImage!=null)
                {
                    
                }
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

    }
}

