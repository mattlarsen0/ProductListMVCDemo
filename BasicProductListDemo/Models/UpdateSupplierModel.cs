using ProductListMVCDemo.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class UpdateSupplierModel
    {
        public int SupplierID { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsValid(out string errorMessage)
        {
            bool isValid = true;
            errorMessage = null;

            // fix empty phone number
            if (PhoneNumber == "")
            {
                PhoneNumber = null;
            }

            if (string.IsNullOrEmpty(Name))
            {
                errorMessage = Errors.SupplierEmptyName;
                isValid = false;
            }
            else if (Name.Length > 50)
            {
                errorMessage = Errors.SupplierNameTooLong;
                isValid = false;
            }
            else if (PhoneNumber != null && PhoneNumber.Length > 50)
            {
                errorMessage = Errors.SupplierPhoneNumberTooLong;
                isValid = false;
            }

            return isValid;
        }
    }
}