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
using DataLayer.ViewModels.Products;
using Core.Utilities;

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

        /// <summary>
        /// لیست تمامی محصولات
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(ProductSearchListViewModel vm)
        {
            var model = await _productRepository.TableNoTracking
                .Where(a=>a.IsActive == true)
                .WhereIf(vm.Titile != null, a => a.Title.Contains(vm.Titile))
                .WhereIf(vm.Group != null, a => a.ProductGroupId == vm.Group.Value)
                .ToListAsync();

            return View(model);
        }

        /// <summary>
        /// جزئیات محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ProductDetail(int id)
        {
            var model = await _productRepository.TableNoTracking
                .FirstOrDefaultAsync(a => a.Id == id);

            return View(model);
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

        /// <summary>
        /// افزودن تعداد بازدید کننده
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task AddVisit(int id)
        {
             await _productRepository.VisitPlus(id);
        }

        /// <summary>
        /// اضافه کردن لایک
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> AddLike(int id)
        {
            var model = await _productRepository.AddLike(id);

            return Json(model);
        }

        /// <summary>
        /// اضافه کردین دیس لایک
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<IActionResult> AddDisLike(int id)
        {
            var model = await _productRepository.AddDisLike(id);

            return Json(model);
        }
    }
}
