using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class ProductListModel : IActionModel
    {
        public bool AddedProduct { get; set; }

        public bool IsValid(out string errorMessage)
        {
            // nothing to validate
            throw new NotImplementedException();
        }
    }
}