using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using WebApiFieUpload.Models;

namespace WebAPI.Helpers
{
    public interface ICarPhotoFileHelper
    {
        public string AddImage(ImageForUpload imageForUploads, int carId);
        
    }
}
