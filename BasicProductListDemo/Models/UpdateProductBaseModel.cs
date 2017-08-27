using ProductListMVCDemo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class UpdateProductBaseModel : AddProductBaseModel
    {
        public int ProductID { get; set; }

        public override bool IsValid(out string errorMessage)
        {
            // check base validation
            bool isValid = base.IsValid(out errorMessage);

            // base is valid, continue validation
            if (isValid)
            {
                if (ProductID <= 0)
                {
                    isValid = false;
                    errorMessage = Errors.CannotFindProduct;
                }
            }

            return isValid;
        }
    }
}