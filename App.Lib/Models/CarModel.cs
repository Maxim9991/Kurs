using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Lib.Models
{
    public class CarModel
    {
        [Required(ErrorMessage = "Поле марка не може бути пустим!")]
        public string Mark { get; set; }
        [Required(ErrorMessage = "Поле модель не може бути пустим!")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Поле рік не може бути пустим!")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Поле паливо не може бути пустим!")]
        public string Fuel { get; set; }
        [Required(ErrorMessage = "Поле об'єм не може бути пустим!")]
        public string Capacity { get; set; }
        public string Image { get; set; }
    }
}
