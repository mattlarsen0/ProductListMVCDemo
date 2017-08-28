using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductListMVCDemo.Models
{
    public class RemoveProductResponseModel : BasicResponseModel
    {
        public bool ProductsRemain { get; set; }
        public string AdditionalDataTables { get; set; }
    }
}