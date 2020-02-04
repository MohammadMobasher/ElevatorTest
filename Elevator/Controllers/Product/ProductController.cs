using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Elevator.Models;
using Service.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Service.Repos.Product;
using DataLayer.SSOT;
using DNTPersianUtils.Core;
namespace Elevator.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductRepostitory _productRepository;
        private readonly ProductDiscountRepository _productDiscountRepository;

        public ProductController(ProductRepostitory productRepostitory,ProductDiscountRepository productDiscountRepository)
        {
            _productRepository = productRepostitory;
            _productDiscountRepository = productDiscountRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// محاسبه تخفیف محصول
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CalculateDiscount(int id)
        {
            var productDiscount = await _productDiscountRepository.GetByConditionAsync(a => a.ProductId == id);
            if (productDiscount == null) return Json(false);

            var product = await _productRepository.GetByIdAsync(id);

            var calculate = productDiscount.DiscountType == ProductDiscountSSOT.Percent ?
                ((product.Price * productDiscount.Discount) / 100)
                : (product.Price - productDiscount.Discount);

            return Json(new Tuple<string, string, int>(calculate.ToString("n0").ToPersianNumbers(), productDiscount.Discount.ToString("n0").ToPersianNumbers(), (int)productDiscount.DiscountType));

        }
    }
}
