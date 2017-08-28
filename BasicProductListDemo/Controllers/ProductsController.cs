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
                viewModel = ProductListViewModel.GetModel(productsContext, model.AddedOrUpdatedProduct);
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

                using (ProductListContext productContext = new ProductListContext())
                {
                    viewModel = AddUpdateProductViewModel.GetModel(productContext, productToUpdate, model.ProductType);
                }

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
                                                      model.RecommendedAge,
                                                      model.SupplierID);

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
                    AddedOrUpdatedProduct = true
                };

                result = RedirectToAction("Index", viewModel);
            }
            else
            {
                // Error, show message
                AddUpdateProductViewModel viewModel;

                using (ProductListContext productContext = new ProductListContext())
                {
                    viewModel = AddUpdateProductViewModel.GetModel(productContext, formProductData, ProductType.Game, errorMessage);
                }

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
                                                     model.Color,
                                                     model.SupplierID);

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
                    AddedOrUpdatedProduct = true
                };

                result = RedirectToAction("Index", viewModel);
            }
            else
            {
                // Error, show the message
                AddUpdateProductViewModel viewModel;

                using (ProductListContext productContext = new ProductListContext())
                {
                    viewModel = AddUpdateProductViewModel.GetModel(productContext, formProductData, ProductType.Car, errorMessage);
                }

                result = View("AddUpdateProduct", viewModel);
            }

            return result;
        }

        [HttpPost]
        public ActionResult UpdateGameProduct(UpdateGameProductModel model)
        {
            bool updatedProduct;
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
                        // update the product
                        productContext.UpdateGameProduct(model.ProductID,
                                                         model.Name,
                                                         model.Price,
                                                         model.Quantity,
                                                         model.GameType,
                                                         model.YearOfRelease,
                                                         model.RecommendedAge,
                                                         model.SupplierID);

                        productContext.SaveChanges();
                    }

                    updatedProduct = true;
                }
                else
                {
                    // save form data for when we return
                    formProductData = new GameProduct
                    {
                        ProductID = model.ProductID,
                        Name = model.Name,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        GameType = model.GameType,
                        YearOfRelease = model.YearOfRelease,
                        RecommendedAge = model.RecommendedAge,
                    };

                    updatedProduct = false;
                }
            }
            catch (Exception e)
            {
                updatedProduct = false;
                errorMessage = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            ActionResult result;

            if (updatedProduct)
            {
                // Updated product, return to list
                ProductListModel viewModel = new ProductListModel
                {
                    AddedOrUpdatedProduct = true
                };

                result = RedirectToAction("Index", viewModel);
            }
            else
            {
                // Error, show message
                AddUpdateProductViewModel viewModel;

                using (ProductListContext productContext = new ProductListContext())
                {
                    viewModel = AddUpdateProductViewModel.GetModel(productContext, formProductData, ProductType.Game, errorMessage);
                }

                result = View("AddUpdateProduct", viewModel);
            }

            return result;
        }

        [HttpPost]
        public ActionResult UpdateCarProduct(UpdateCarProdutModel model)
        {
            bool updatedProduct;
            string errorMessage;
            CarProduct formProductData = null;

            try
            {
                if (model.IsValid(out errorMessage))
                {
                    // round price
                    model.Price = Math.Round(model.Price, 2);

                    using (ProductListContext productContext = new ProductListContext())
                    {
                        // update the product
                        productContext.UpdateCarProduct(model.ProductID,
                                                        model.Name,
                                                        model.Price,
                                                        model.Quantity,
                                                        model.Year,
                                                        model.Color,
                                                        model.SupplierID);

                        productContext.SaveChanges();
                    }

                    updatedProduct = true;
                }
                else
                {
                    // save form data for when we return
                    formProductData = new CarProduct
                    {
                        ProductID = model.ProductID,
                        Name = model.Name,
                        Price = model.Price,
                        Quantity = model.Quantity,
                        Year = model.Year,
                        Color = model.Color
                    };

                    updatedProduct = false;
                }
            }
            catch (Exception e)
            {
                updatedProduct = false;
                errorMessage = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            ActionResult result;

            if (updatedProduct)
            {
                // Updated product, return to list
                ProductListModel viewModel = new ProductListModel
                {
                    AddedOrUpdatedProduct = true
                };

                result = RedirectToAction("Index", viewModel);
            }
            else
            {
                // Error, show message
                AddUpdateProductViewModel viewModel;

                using (ProductListContext productContext = new ProductListContext())
                {
                    viewModel = AddUpdateProductViewModel.GetModel(productContext, formProductData, ProductType.Car, errorMessage);
                }

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

        [HttpPost]
        public ActionResult AddSupplier(AddSupplierModel model)
        {
            bool addedSupplier;
            string errorMessage;
            Supplier formSupplierData = null;

            try
            {
                if (model.IsValid(out errorMessage))
                {
                    using (ProductListContext productContext = new ProductListContext())
                    {
                        // insert the supplier
                        productContext.AddSupplier(model.Name,
                                                   model.PhoneNumber);

                        productContext.SaveChanges();
                    }

                    addedSupplier = true;
                }
                else
                {
                    // save form data for when we return
                    formSupplierData = new Supplier
                    {
                        SupplierID = -1,
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber
                    };

                    addedSupplier = false;
                }
            }
            catch (Exception e)
            {
                addedSupplier = false;
                errorMessage = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            ActionResult result;

            if (addedSupplier)
            {
                // Added supplier, return to list
                ProductListModel viewModel = new ProductListModel
                {
                    AddedOrUpdatedProduct = true
                };

                result = RedirectToAction("Index", viewModel);
            }
            else
            {
                // Error, show message
                AddUpdateSupplierViewModel viewModel = AddUpdateSupplierViewModel.GetModel(formSupplierData, errorMessage);
                result = View("AddUpdateSupplier", viewModel);
            }

            return result;
        }

        public ViewResult AddUpdateSupplier(AddUpdateSupplierModel model)
        {
            string errorMessage;
            Supplier supplierToUpdate = null;

            try
            {
                if (model.IsValid(out errorMessage))
                {
                    // If we're updating a supplier
                    if (model.SupplierID.HasValue)
                    {
                        using (ProductListContext productContext = new ProductListContext())
                        {
                            supplierToUpdate = productContext.Suppliers.FirstOrDefault(s => s.SupplierID == model.SupplierID);
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
                AddUpdateSupplierViewModel viewModel = null;

                viewModel = AddUpdateSupplierViewModel.GetModel(supplierToUpdate);
                viewResult = View(viewModel);
            }

            return viewResult;
        }

        [HttpPost]
        public JsonResult RemoveSupplier(RemoveSupplierModel model)
        {
            RemoveSupplierJsonModel responseModel = new RemoveSupplierJsonModel();

            try
            {
                ProductListViewModel partialViewModel;
                using (ProductListContext productContext = new ProductListContext())
                {
                    Supplier supplierToRemove = productContext.Suppliers.FirstOrDefault(s => s.SupplierID == model.SupplierID);

                    if (supplierToRemove != null)
                    {
                        // Remove any products associated with the supplier
                        foreach (ProductBase product in productContext.AllProducts.Where(p => p.SupplierID == supplierToRemove.SupplierID))
                        {
                            product.Removed = true;
                        }

                        // Remove the supplier
                        supplierToRemove.Removed = true;

                        productContext.SaveChanges();
                    }

                    responseModel.SuppliersRemain = productContext.Suppliers.Count(s => !s.Removed) > 0;

                    // create the model so we can render the new version of the table
                    partialViewModel = ProductListViewModel.GetModel(productContext);
                }

                responseModel.Success = true;
                // render the table
                responseModel.Content = MvcHelper.RenderControllerPartialViewToString(this, "_SupplierListTable", partialViewModel);
            }
            catch (Exception e)
            {
                responseModel.Success = false;
                responseModel.Content = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            return Json(responseModel);
        }

        [HttpPost]
        public ActionResult UpdateSupplier(UpdateSupplierModel model)
        {
            bool updatedSupplier;
            string errorMessage;
            Supplier formSupplierData = null;

            try
            {
                if (model.IsValid(out errorMessage))
                {
                    using (ProductListContext productContext = new ProductListContext())
                    {
                        // update the supplier
                        productContext.UpdateSupplier(model.SupplierID,
                                                      model.Name,
                                                      model.PhoneNumber);

                        productContext.SaveChanges();
                    }

                    updatedSupplier = true;
                }
                else
                {
                    // save form data for when we return
                    formSupplierData = new Supplier
                    {
                        SupplierID = model.SupplierID,
                        Name = model.Name,
                        PhoneNumber = model.PhoneNumber
                    };

                    updatedSupplier = false;
                }
            }
            catch (Exception e)
            {
                updatedSupplier = false;
                errorMessage = Errors.GenericMVCInternalError;
                ErrorLog.LogError(e);
            }

            ActionResult result;

            if (updatedSupplier)
            {
                // Updated product, return to list
                ProductListModel viewModel = new ProductListModel
                {
                    AddedOrUpdatedProduct = true
                };

                result = RedirectToAction("Index", viewModel);
            }
            else
            {
                // Error, show message
                AddUpdateSupplierViewModel viewModel = AddUpdateSupplierViewModel.GetModel(formSupplierData, errorMessage);
                result = View("AddUpdateSupplier", viewModel);
            }

            return result;
        }
    }
}