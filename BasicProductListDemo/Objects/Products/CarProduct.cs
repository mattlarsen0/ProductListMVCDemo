using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Objects.Products
{
    public class CarProduct : ProductBase
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public string Color { get; set; }

        public override string GetAdditionalInformation()
        {
            return Year + ", " + Color;
        }
    }
}