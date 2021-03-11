using DNBMarket.BusinessLogic.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DNBMarket.WebUI.Contollers
{
    public class CartController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICartProductService _cartProductService;

        public CartController(IProductService productService, ICartService cartService, ICartProductService cartProductService)
        {
            _productService = productService;
            _cartService = cartService;
            _cartProductService = cartProductService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCurrentCartAsync());
        }
        public async Task<IActionResult> AddToCart(int productId)
        {
            if (productId > 0)
            {
                TempData["CartMessage"] = await _cartProductService.AddToCartAsync(productId);
            }
            return RedirectToAction("Index", "Product");
        }
        public async Task<IActionResult> Pay(int Id)
        {
            if (Id > 0)
            {
                var result = await _cartProductService.PayAsync(Id);
                TempData["PayMessage"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
