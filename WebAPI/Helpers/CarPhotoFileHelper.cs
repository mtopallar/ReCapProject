using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using WebApiFieUpload.Models;

namespace WebAPI.Helpers
{
    public class CarPhotoFileHelper : ICarPhotoFileHelper
    {
        private static IWebHostEnvironment _webHostEnvironment;
        private ICarImageService _carImageService;
        private int maximumImageCountPerCar = 5;
        private string imagePath = _webHostEnvironment.WebRootPath + @"\CarImages\";

        public CarPhotoFileHelper(IWebHostEnvironment webHostEnvironment, ICarImageService carImageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _carImageService = carImageService;
        }

        public string AddImage(ImageForUpload imagesForUpload, int carId)
        {
            var imageCountByCar = _carImageService.GetListByCarId(carId).Data.Count;

            if (imageCountByCar == 0)
            {
                return "Araç bulunamadı";
            }

            if (imageCountByCar >= 5)
            {
                return "Bir araç için izin verilen maksimum araç sayısına ulaşıldı.";
            }
            
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            for (int i = imageCountByCar; i < maximumImageCountPerCar; i++)
            {
                var guIdName = Guid.NewGuid().ToString("N") + "_" + carId + "_" + DateTime.Now.Second;
                var fileExtension = new System.IO.FileInfo(imagesForUpload.UploadedImage[i].FileName).Extension;

                using (FileStream fileStream = System.IO.File.Create(imagePath + guIdName + fileExtension))
                {
                    imagesForUpload.UploadedImage[i].CopyTo(fileStream);
                    fileStream.Flush();
                    var carImageForDb = new CarImage
                    { CarId = carId, ImagePath = imagePath + guIdName + fileExtension, Date = DateTime.Now };
                    _carImageService.Add(carImageForDb);
                }

            }
            return $"{5 - imageCountByCar} adet araç sisteme yüklendi.";
        }
        
    }
}
