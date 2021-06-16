using App.Lib.Models;
using App.Lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TooLargeException.Entities;

namespace TooLargeException.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private EFDataContext _context { get; set; }
        public CarsController(EFDataContext context)
        {
            _context = context;
        }
        [HttpPost, Route("send")]
        public async Task<IActionResult> SendData([FromBody]CarModel car) 
        {
            return await Task.Run(() => { 
                AppCar appcar = new AppCar
                {
                    Mark = car.Mark,
                    Model = car.Model,
                    Year = int.Parse(car.Year),
                    Capacity = float.Parse(car.Capacity),
                    Fuel = car.Fuel,
                    Image = car.Image
                };

                Bitmap bmp = ConvertImageService.ConvertToBitmap(appcar.Image);
                var Extension = car.Image.EndsWith(".jpg") ? ".jpg" : ".png";
                var path = Path.Combine("uploads", Path.GetRandomFileName() + Extension);
                bmp.Save(path);

                appcar.Image = path;

                _context.Cars.Add(appcar);

                _context.SaveChanges();
                return Ok(JsonConvert.SerializeObject("Додано у БД!"));
            });
        }

        [HttpGet, Route("get")]
        public async Task<IActionResult> GetData() 
        {
            return await Task.Run(() => { return Ok(_context.Cars.ToList()); });
        }


    }
}
