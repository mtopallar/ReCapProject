using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebAPI.Models
{
    public class ImageForUpload
    {
        public List<IFormFile> UploadedImage { get; set; }
    }
}
