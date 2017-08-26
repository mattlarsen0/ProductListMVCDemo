using ProductListMVCDemo.Objects.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicProductListDemo.Models
{
    public class AddUpdateProductViewModel
    {
        public bool IsUpdate { get; set; }

        public ProductBase Product { get; set; }

        public static AddUpdateProductViewModel GetModel(ProductBase product)
        {
            AddUpdateProductViewModel model = new AddUpdateProductViewModel();

            model.IsUpdate = product != null;
            model.Product = product;

            return model;
        }
    }
}