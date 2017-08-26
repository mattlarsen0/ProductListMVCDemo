using ProductListMVCDemo.Models;
using ProductListMVCDemo.Objects.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace ProductListMVCDemo.Controllers
{
    public class ProductsController : Controller
    {
        public ViewResult Index()
        {
            ProductListContext productsContext = new ProductListContext();
            ProductListViewModel model = ProductListViewModel.GetModel(productsContext);

            return View(model);
        }
    }
}