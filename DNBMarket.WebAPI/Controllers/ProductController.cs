using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNBMarket.BaseLogic.Data.Interfaces;
using DNBMarket.BusinessLogic.Abstract;
using DNBMarket.Entities.DNBMarket;

namespace DNBMarket.WebAPI.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService; 
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            var data = await _productService.GetAllProduct();

            if (data.Data != null)
            {
                return Ok(data.Data);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(Product input)
        {
            if (input != default(Product))
            {
                if (await _productService.AddOrUpdateProductAsync(input) > 0)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
                
            }
            return BadRequest();
        }
    }
}
