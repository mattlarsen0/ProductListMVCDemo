using ProductListMVCDemo.Objects;
using ProductListMVCDemo.Objects.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class UpdateGameProductModel : UpdateProductBaseModel
    {
        [EnumDataType(typeof(GameProductType))]
        public GameProductType GameType { get; set; }

        public int YearOfRelease { get; set; }

        public int RecommendedAge { get; set; }

        public override bool IsValid(out string errorMessage)
        {
            // check base validation
            bool isValid = base.IsValid(out errorMessage);

            // base is valid, continue validation
            if (isValid)
            {
                if (YearOfRelease <= 0)
                {
                    errorMessage = Errors.ProductsNegativeYear;
                    isValid = false;
                }
                else if (RecommendedAge < 0)
                {
                    errorMessage = Errors.ProductsNegativeAge;
                    isValid = false;
                }
            }

            return isValid;
        }
    }
}