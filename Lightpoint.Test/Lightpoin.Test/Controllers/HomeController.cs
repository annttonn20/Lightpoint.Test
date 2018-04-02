using Lightpoin.Test.Models;
using Lightpoin.Test.Web.Models;
using Lightpoint.Test.Business.Exceptions;
using Lightpoint.Test.Business.Interface;
using Lightpoint.Test.Business.Structure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Libuv.Internal.Networking;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Lightpoin.Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IManager manager;
        private readonly IManager<ProductStruct> productManager;
        private readonly IManager<StoreStruct> storeManager;

        public HomeController(IManager manager, IManager<ProductStruct> productManager, IManager<StoreStruct> storeManager)
        {

            this.manager = manager;
            this.productManager = productManager;
            this.storeManager = storeManager;
        }

        public async Task<IActionResult> AllStores()
        {
            StoreModel storeModel = new StoreModel();
            storeModel.StoreStructs = await storeManager.GetAllAsync();


            return View("AllStores", storeModel);
        }

        public async Task<IActionResult> AllProducts()
        {
            ProductModel productModel = new ProductModel();
            productModel.ProductStructs = await productManager.GetAllAsync();

            return View(productModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStore(StoreModel storeModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    (bool isOk, int id) = await storeManager.AddAsync(new StoreStruct
                    {
                        Name = storeModel.Name,
                        Address = storeModel.Address,
                        WorkingHours = storeModel.WorkingHours
                    });
                    storeModel.Id = id;
                    return PartialView("SingleStore", storeModel);
                }
                catch (ExistsInDBException e)
                {
                    ViewData["Errors"] = e.Message;
                }
            }
            else
            {
                ViewData["Errors"] = "The field can not be empty";
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(ViewData["Errors"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    (bool isOk, int id) = await productManager.AddAsync(new ProductStruct
                    {
                        Name = productModel.Name,
                        Description = productModel.Description
                    });
                    productModel.Id = id;
                    return PartialView("SingleProduct", productModel);
                }
                catch (ExistsInDBException e)
                {
                    ViewData["Errors"] = e.Message;
                }
            }
            else
            {
                ViewData["Errors"] = "The field can not be empty";
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(ViewData["Errors"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToStorePartial(AddProductToStoreModel addProductToStore)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await manager.AddProductToStoreAsync(addProductToStore.StoreName, addProductToStore.ProductName);
                    ProductStruct productStruct = await productManager.GetOneAsync(addProductToStore.ProductName);
                    ProductModel productModel = new ProductModel
                    {
                        Id = productStruct.Id,
                        Name = productStruct.Name,
                        Description = productStruct.Description
                    };
                    return PartialView("SingleProduct", productModel);
                }
                catch (ExistsInDBException e)
                {
                    ViewData["Errors"] = e.Message;
                }
                catch (NoExistInDbException e)
                {
                    ViewData["Errors"] = e.Message;

                }
            }
            else
            {
                ViewData["Errors"] = "The field can not be empty";
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Content(ViewData["Errors"].ToString());
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToStore(AddProductToStoreModel addPtoSModel)
        {
            AddProductToStoreModel addProductToStore;
            if (ModelState.IsValid)
            {
                try
                {
                    await manager.AddProductToStoreAsync(addPtoSModel.StoreName, addPtoSModel.ProductName);
            ViewData["Success"] = "Product added to store " + addPtoSModel.StoreName;

                }
                catch (ExistsInDBException e)
                {
                    ViewData["Errors"] = e.Message;
                }
                catch (NoExistInDbException e)
                {
                    ViewData["Errors"] = e.Message;

                }
            }
            else
            {
                ViewData["Errors"] = "Incorrect data entered or some fields are empty";
            }

            //  addProductToStore = new AddProductToStoreModel { ProductStructs = new List<ProductStruct>(), StoreName = storeName };
            try
            {
                StoreStruct storeStruct = await storeManager.GetOneAsync(addPtoSModel.StoreName);
                ViewData.Add("StoreName", storeStruct.Name);
                addProductToStore = new AddProductToStoreModel
                {
                    ProductStructs = storeStruct.Products,
                    StoreName = storeStruct.Name
                };
            }
            catch (NoExistInDbException)
            {
                addProductToStore = new AddProductToStoreModel { ProductStructs = new List<ProductStruct>(), StoreName = addPtoSModel.StoreName };

            }
            return View(addProductToStore);
        }


        public IActionResult AddProductToStore()
        {
            return PartialView();
        }

        public async Task<IActionResult> StoreProducts(string storeName)
        {
            AddProductToStoreModel addProductToStore;
            try
            {
                StoreStruct storeStruct = await storeManager.GetOneAsync(storeName);
                ViewData.Add("StoreName", storeStruct.Name);
                addProductToStore = new AddProductToStoreModel
                {
                    ProductStructs = storeStruct.Products,
                    StoreName = storeStruct.Name
                };

                
            }
            catch (NoExistInDbException)
            {
                addProductToStore = new AddProductToStoreModel { ProductStructs = new List<ProductStruct>(), StoreName = storeName };

            }
            return PartialView(addProductToStore);
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
