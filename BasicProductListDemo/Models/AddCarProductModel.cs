using ProductListMVCDemo.Models;
using ProductListMVCDemo.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class AddCarProductModel : AddProductBaseModel
    {
        public int Year { get; set; }

        public string Color { get; set; }

        public override bool IsValid(out string errorMessage)
        {
            // check base validation
            bool isValid = base.IsValid(out errorMessage);

            // base is valid, continue validation
            if (isValid)
            {
                if (Year <= 0)
                {
                    errorMessage = Errors.ProductsNegativeYearOfRelease;
                    isValid = false;
                }
                else if (string.IsNullOrEmpty(Color))
                {
                    errorMessage = Errors.ProductsEmptyColor;
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}