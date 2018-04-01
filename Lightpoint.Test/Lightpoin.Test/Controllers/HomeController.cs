using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lightpoin.Test.Models;
using Lightpoin.Test.Web.Models;
using Lightpoint.Test.Business.Interface;
using Lightpoint.Test.Business.Structure;
using Lightpoint.Test.Business.Exceptions;
using System.Net;

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


            return View(storeModel);
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
                    return PartialView("SingleProduct",productModel);
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
        public async Task<IActionResult> AddProductToStore(AddProductToStoreModel AddPtoSModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await manager.AddProductToStoreAsync(AddPtoSModel.StoreId, AddPtoSModel.ProductName);
                    return RedirectToAction(nameof(AllStores));
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
            return View(AddPtoSModel);
        }


        public IActionResult AddProductToStore()
        {
            return PartialView();
        }

        public async Task<IActionResult> StoreProducts(int storeId)
        {
            StoreStruct storeStruct = await storeManager.GetOneAsync(storeId);
            ViewData.Add("StoreName", storeStruct.Name);
            List<ProductModel> productModels = new List<ProductModel>();
            foreach (var product in storeStruct.Products)
            {
                ProductModel productModel = new ProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description
                };
                productModels.Add(productModel);
            }

            return PartialView(productModels);
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
