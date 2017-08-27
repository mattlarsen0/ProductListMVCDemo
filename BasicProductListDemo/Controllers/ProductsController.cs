using ProductListMVCDemo.Models;
using ProductListMVCDemo.Objects;
using ProductListMVCDemo.Objects.Internal;
using ProductListMVCDemo.Objects.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using ProductListMVCDemo.Objects.Enums;
using ProductListMVCDemo.Objects.Products;

namespace ProductListMVCDemo.Controllers
{
    public class ProductsController : Controller
    {
        public ViewResult Index()
        {
            ProductListViewModel model;

            using (ProductListContext productsContext = new ProductListContext())
            { 
                model = ProductListViewModel.GetModel(productsContext);
            }

            return View(model);
        }

        public ViewResult AddUpdateProduct(AddUpdateProductModel model)
        {
            string errorMessage;
            ProductBase productToUpdate = null;

            try
            {
                if (model.IsValid(out errorMessage))
                {
                    // If we're updating a product
                    if (model.ProductID.HasValue)
                    {
                        using (ProductListContext productContext = new ProductListContext())
                        {
                            // find the product to update
                            switch (model.ProductType)
                            {
                                case ProductType.Game:
                                    productToUpdate = productContext.GameProducts.FirstOrDefault(p => p.ProductID == model.ProductID);
                                    break;
                                case ProductType.Car:
                                    productToUpdate = productContext.CarProducts.FirstOrDefault(p => p.ProductID == model.ProductID);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errorMessage = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            ViewResult viewResult;

            if (errorMessage != null)
            {
                ProductListViewModel viewModel;
                using (ProductListContext productContext = new ProductListContext())
                {
                    viewModel = ProductListViewModel.GetModel(productContext, errorMessage: errorMessage);
                }

                viewResult = View("Index", viewModel);
            }
            else
            {
                AddUpdateProductViewModel viewModel = null;

                viewModel = AddUpdateProductViewModel.GetModel(productToUpdate, model.ProductType);
                viewResult = View(viewModel);
            }

            return viewResult;
        }

        public ViewResult AddGameProduct(AddGameProductModel model)
        {
            bool addedProduct;
            string errorMessage;

            try
            {
                if (model.IsValid(out errorMessage))
                { 
                    using (ProductListContext productContext = new ProductListContext())
                    {
                        productContext.AddGameProduct(model.Name,
                                                      model.Price, 
                                                      model.Quantity,
                                                      model.GameType,
                                                      model.YearOfRelease,
                                                      model.ReccomendedAge);

                        productContext.SaveChanges();
                    }

                    addedProduct = true;
                }
                else
                {
                    addedProduct = false;
                }
            }
            catch (Exception e)
            {
                addedProduct = false;
                errorMessage = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            ViewResult viewResult;

            if (addedProduct)
            {
                ProductListViewModel viewModel;
                using (ProductListContext productContext = new ProductListContext())
                {
                    viewModel = ProductListViewModel.GetModel(productContext, addedProduct: true);
                }

                viewResult = View("Index", viewModel);
            }
            else
            {
                AddUpdateProductViewModel viewModel = AddUpdateProductViewModel.GetModel(null, ProductType.Game, errorMessage);
                viewResult = View("AddProduct", viewModel);
            }

            return viewResult;
        }
    }
}