using ProductListMVCDemo.Objects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Objects.Products
{
    public class GameProduct : ProductBase
    {
        [Required]
        public GameProductType GameType { get; set; }

        [Required]
        public int YearOfRelease { get; set; }

        [Required]
        public int RecommendedAge { get; set; }
    }
}