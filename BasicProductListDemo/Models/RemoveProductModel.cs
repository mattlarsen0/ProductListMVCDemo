using ProductListMVCDemo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class RemoveProductModel : IActionModel
    {
        public int ProductID { get; set; }

        public bool IsValid(out string errorMessage)
        {
            bool isValid = true;
            errorMessage = null;

            if (ProductID <= 0)
            {
                isValid = false;
                errorMessage = Errors.CannotFindProduct;
            }

            return isValid;
        }
    }
}