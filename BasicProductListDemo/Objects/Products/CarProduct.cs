using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Classes.Products
{
    public class CarProduct : ProductBase
    {
        [Required]
        public int Year { get; set; }

        [Required]
        public int Color { get; set; }
    }
}