using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;
        private static IWebHostEnvironment _webHostEnvironment;
        private ICarImageService _carImageService;

        public CarsController(ICarService carService, IWebHostEnvironment webHostEnvironment, ICarImageService carImageService)
        {
            _carService = carService;
            _webHostEnvironment = webHostEnvironment;
            _carImageService = carImageService;
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _carService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcarsbybrandid")]
        public IActionResult GetCarsByBrandId(int brandId)
        {
            var result = _carService.GetCarsByBrandId(brandId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcardetailsbybrandid")]   //bunu kullan
        public IActionResult GetCarsDetailsByBrandId(int brandId)
        {
            var result = _carService.GetCarDetailsByBrandId(brandId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcarsbycolorid")]
        public IActionResult GetCarsByColorId(int colorId)
        {
            var result = _carService.GetCarsByColorId(colorId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcardetailsbycolorid")]   //bunu kullan
        public IActionResult GetCarDetailsByColorId(int colorId)
        {
            var result = _carService.GetCarDetailsByColorId(colorId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getcardetails")]
        public IActionResult GetCarDetails()
        {
            var result = _carService.GetCarDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getcarimagedetails")]
        public IActionResult GetCarImageDetails(int carId)
        {
            var checkCar = _carService.GetById(carId);
            if (checkCar.Data == null)
            {
                return BadRequest("Araç bulunamadı");
            }
            var hasTheCarAnyPhoto = _carImageService.GetListByCarId(carId).Data;
            if (hasTheCarAnyPhoto.Count == 0)
            {
                //string imagePath = _webHostEnvironment.WebRootPath + @"\CarImages\";
                List<CarImage> carImages = new List<CarImage>
                    {new CarImage {CarId = carId, ImagePath = /*imagePath +*/ "CarRentalDefault.jpg"}};
                var resultWithDefaultPhoto = _carService.GetCarDetailsByCarId(carId);
                resultWithDefaultPhoto.Data.CarImages = carImages;
                return Ok(resultWithDefaultPhoto);
            }
            var resultWithCarOwnPhoto = _carService.GetCarDetailsByCarId(carId);
            resultWithCarOwnPhoto.Data.CarImages = hasTheCarAnyPhoto;
            return Ok(resultWithCarOwnPhoto);

        }

        [HttpPost("add")]
        public IActionResult Add(Car car)
        {
            var result = _carService.Add(car);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
