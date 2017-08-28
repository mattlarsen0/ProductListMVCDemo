using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class RemoveSupplierJsonModel : BasicResponseModel
    {
        public bool SuppliersRemain { get; set; }
    }
}