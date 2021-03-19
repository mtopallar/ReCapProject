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
        private int sayac = 0;
        //private string imagePath = _webHostEnvironment.WebRootPath + @"\CarImages\";
        //private string fileFolderPath = AppDomain.CurrentDomain + @"\CarImages\";
        public CarPhotoFileHelper(IWebHostEnvironment webHostEnvironment, ICarImageService carImageService)
        {
            _webHostEnvironment = webHostEnvironment;
            _carImageService = carImageService;
        }

        public string AddImage(ImageForUpload imagesForUpload, int carId)
        {
            var imagePath = _webHostEnvironment.WebRootPath + @"\CarImages\";
            var imageCountByCar = _carImageService.GetListByCarId(carId).Data.Count;

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }


            for (int i = 0; i < imagesForUpload.UploadedImage.Count; i++)
            {
                if (ImageCounter(carId) >= 5)
                {
                    if (sayac!=0)
                    {
                        return $"{sayac} adet araç sisteme yüklenerek bir araç için izin verilen maksimum fotoğraf sayısına ulaşıldı";
                    }
                    return "Bir araç için izin verilen maksimum fotoğraf sayısına ulaşıldı.";
                }
                if (ImageCounter(carId) < 5)
                {
                    var guIdName = Guid.NewGuid().ToString("N") + "_" + carId + "_" + DateTime.Now.Second;
                    var fileExtension = new System.IO.FileInfo(imagesForUpload.UploadedImage[i].FileName).Extension;

                    using (FileStream fileStream = System.IO.File.Create(imagePath + guIdName + fileExtension))
                    {
                        imagesForUpload.UploadedImage[i].CopyTo(fileStream);
                        fileStream.Flush();
                        var carImageForDb = new CarImage
                        { CarId = carId, ImagePath = guIdName + fileExtension, Date = DateTime.Now };
                        _carImageService.Add(carImageForDb);
                    }

                    sayac += 1;

                }

            }
            return $"{sayac} adet araç sisteme yüklendi.";

        }

        private int ImageCounter(int carId)
        {
            var imageCountByCar = _carImageService.GetListByCarId(carId).Data.Count;
            return imageCountByCar;
        }

    }
}
