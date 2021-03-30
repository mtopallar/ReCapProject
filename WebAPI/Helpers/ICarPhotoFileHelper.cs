using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using WebApiFieUpload.Models;

namespace WebAPI.Helpers
{
    public interface ICarPhotoFileHelper
    {
        public string AddImage(ImageForUpload imageForUploads, int carId);
        //public IDataResult<List<CarDetailDto>> GetCarListWithSingleImage();
        //public IDataResult<List<CarDetailDto>> GetCarListByBrandIdWithSingleImage(int brandId);
        //public IDataResult<List<CarDetailDto>> GetCarListByColorIdWithSingleImage(int colorId);

    }
}
