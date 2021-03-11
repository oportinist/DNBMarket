using DNBMarket.BusinessLogic.Abstract;
using DNBMarket.Entities.DNBMarket;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNBMarket.WebUI.Contollers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICartProductService _cartProductService;
        public ProductController(IProductService productService,ICartProductService cartProductService)
        {
            _productService = productService;
            _cartProductService = cartProductService;
        }
        public async Task<IActionResult> Index()
        { 
            return View(await _productService.GetAllProduct());
        }
        public async Task<IActionResult> AddOrUpdate(int? Id)
        {
            if (Id != null && Id > 0)
            {
                var data = await _productService.GetProductAsync((int)Id);
                return View(data);
            }
            else return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(Product model)
        {
            if (await _productService.AddOrUpdateProductAsync(model) > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "İşlem Sırasında Hata";
                return Redirect("/Product/Add");
            }
        }
        public async Task<IActionResult> Delete(int Id)
        {
            await _productService.DeleteAsync(Id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddToCart(int Id)
        {
            if (Id > 0)
            {
                TempData["CartMessage"] = await _cartProductService.AddToCartAsync(Id);
            }
            return RedirectToAction("Index");
        }
    }
}
