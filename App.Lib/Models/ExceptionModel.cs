using System;
using System.Collections.Generic;
using System.Text;

namespace App.Lib.Models
{
    public class ErrorContent 
    {
        public ExceptionModel Errors { get; set; }
    }
    public partial class ExceptionModel
    {
        public List<string> Mark { get; set; }
        public List<string> Model { get; set; }
        public List<string> Fuel { get; set; }
        public List<string> Year { get; set; }
        public List<string> Capacity { get; set; }
    }

    public partial class ExceptionModel 
    {
        public string GetExceptionInLine() 
        {
            string res = "";
            if (this.Mark != null) 
            {
                foreach (var item in this.Mark) 
                {
                    res += item + "\n";
                }
            }

            if (this.Model != null)
            {
                foreach (var item in this.Model)
                {
                    res += item + "\n";
                }
            }

            if (this.Year != null)
            {
                foreach (var item in this.Year)
                {
                    res += item + "\n";
                }
            }
            if (this.Capacity != null)
            {
                foreach (var item in this.Capacity)
                {
                    res += item + "\n";
                }
            }
            if (this.Fuel != null)
            {
                foreach (var item in this.Fuel)
                {
                    res += item + "\n";
                }
            }
            return res;
        }
    }
}
