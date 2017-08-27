using ProductListMVCDemo.Models;
using ProductListMVCDemo.Objects;
using ProductListMVCDemo.Objects.Internal;
using ProductListMVCDemo.Objects.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using ProductListMVCDemo.Objects.Enums;
using ProductListMVCDemo.Objects.Products;
using System.Threading.Tasks;

namespace ProductListMVCDemo.Controllers
{
    public class ProductsController : Controller
    {
        public ViewResult Index(ProductListModel model)
        {
            ProductListViewModel viewModel;

            using (ProductListContext productsContext = new ProductListContext())
            {
                viewModel = ProductListViewModel.GetModel(productsContext, model.AddedProduct);
            }

            return View(viewModel);
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
                // Error, show the message
                ProductListViewModel viewModel;
                using (ProductListContext productContext = new ProductListContext())
                {
                    viewModel = ProductListViewModel.GetModel(productContext, errorMessage: errorMessage);
                }

                viewResult = View("Index", viewModel);
            }
            else
            {
                // Load the Add/Update view
                AddUpdateProductViewModel viewModel = null;

                viewModel = AddUpdateProductViewModel.GetModel(productToUpdate, model.ProductType);
                viewResult = View(viewModel);
            }

            return viewResult;
        }

        [HttpPost]
        public ActionResult AddGameProduct(AddGameProductModel model)
        {
            bool addedProduct;
            string errorMessage;
            GameProduct formProductData = null;

            try
            {
                if (model.IsValid(out errorMessage))
                {
                    // round price
                    model.Price = Math.Round(model.Price, 2);

                    using (ProductListContext productContext = new ProductListContext())
                    {
                        // insert the product
                        productContext.AddGameProduct(model.Name,
                                                      model.Price, 
                                                      model.Quantity,
                                                      model.GameType,
                                                      model.YearOfRelease,
                                                      model.RecommendedAge);

                        productContext.SaveChanges();
                    }

                    addedProduct = true;
                }
                else
                {
                    // save form data for when we return
                    formProductData = new GameProduct
                    {
                        ProductID = -1,
                        Name = model.Name,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        GameType = model.GameType,
                        YearOfRelease = model.YearOfRelease,
                        RecommendedAge = model.RecommendedAge,
                    };

                    addedProduct = false;
                }
            }
            catch (Exception e)
            {
                addedProduct = false;
                errorMessage = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            ActionResult result;

            if (addedProduct)
            {
                // Added product, return to list
                ProductListModel viewModel = new ProductListModel
                {
                    AddedProduct = true
                };

                result = RedirectToAction("Index", viewModel);
            }
            else
            {
                // Error, show message
                AddUpdateProductViewModel viewModel = AddUpdateProductViewModel.GetModel(formProductData, ProductType.Game, errorMessage);
                result = View("AddUpdateProduct", viewModel);
            }

            return result;
        }

        [HttpPost]
        public ActionResult AddCarProduct(AddCarProductModel model)
        {
            bool addedProduct;
            string errorMessage;
            CarProduct formProductData = null;

            try
            {
                if (model.IsValid(out errorMessage))
                {
                    // round the price
                    model.Price = Math.Round(model.Price, 2);
                    using (ProductListContext productContext = new ProductListContext())
                    {
                        // insert the product
                        productContext.AddCarProduct(model.Name,
                                                     model.Price,
                                                     model.Quantity,
                                                     model.Year,
                                                     model.Color);

                        productContext.SaveChanges();
                    }

                    addedProduct = true;
                }
                else
                {
                    // save form data for when we return
                    formProductData = new CarProduct
                    {
                        ProductID = -1,
                        Name = model.Name,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        Year = model.Year,
                        Color = model.Color
                    };

                    addedProduct = false;
                }
            }
            catch (Exception e)
            {
                addedProduct = false;
                errorMessage = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            ActionResult result;

            if (addedProduct)
            {
                // Added the product, return to list
                ProductListModel viewModel = new ProductListModel
                {
                    AddedProduct = true
                };

                result = RedirectToAction("Index", viewModel);
            }
            else
            {
                // Error, show the message
                AddUpdateProductViewModel viewModel = AddUpdateProductViewModel.GetModel(formProductData, ProductType.Car, errorMessage);
                result = View("AddUpdateProduct", viewModel);
            }

            return result;
        }

        [HttpPost]
        public JsonResult RemoveProduct(RemoveProductModel model)
        {
            BasicResponseModel responseModel = new BasicResponseModel();

            try
            {
                ProductListViewModel partialViewModel;
                using (ProductListContext productContext = new ProductListContext())
                {
                    ProductBase productToRemove = productContext.AllProducts.FirstOrDefault(p => p.ProductID == model.ProductID);
    
                    if (productToRemove != null)
                    {
                        productToRemove.Removed = true;
                    }

                    productContext.SaveChanges();

                    // create the model so we can render the new version of the table
                    partialViewModel = ProductListViewModel.GetModel(productContext);
                    
                    responseModel.Success = true;
                    // render the table
                    responseModel.Content = MvcHelper.RenderControllerPartialViewToString(this, "_ProductListTable", partialViewModel);
                }
            }
            catch (Exception e)
            {
                responseModel.Success = false;
                responseModel.Content = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            return Json(responseModel);
        }
    }
}