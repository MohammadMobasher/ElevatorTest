using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.DTO.Products;
using DataLayer.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos;
using Service.Repos.Product;
using Service.Repos.User;
using WebFramework.Authenticate;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Product.Controllers
{

    [Area("Product")]
    [ControllerRole("مدیریت کالا‌ها")]
    public class ManageProductController : BaseAdminController
    {
        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly ProductUnitRepository _productUnitRepository;
        private readonly ProductGroupFeatureRepository _productGroupFeatureRepository;
        private readonly ProductFeatureRepository _productFeatureRepository;

        public ManageProductController(UsersAccessRepository usersAccessRepository,
            ProductRepostitory productRepostitory,
            ProductGroupRepository productGroupRepository,
            ProductUnitRepository productUnitRepository,
            ProductGroupFeatureRepository productGroupFeatureRepository,
            ProductFeatureRepository productFeatureRepository) : base(usersAccessRepository)
        {
            _productRepostitory = productRepostitory;
            _productGroupRepository = productGroupRepository;
            _productUnitRepository = productUnitRepository;
            _productGroupFeatureRepository = productGroupFeatureRepository;
            _productFeatureRepository = productFeatureRepository;
        }



        [ActionRole("صفحه لیست کالاها")]
        //[HasAccess]
        public IActionResult Index()
        {
            var model = _productRepostitory.TableNoTracking.ProjectTo<ProductFullDTO>().ToList();
            return View(model);
        }

        [ActionRole("ثبت کالای جدید")]
        public async Task<IActionResult> Create(int? id)
        {
            ViewBag.Units = await _productUnitRepository.TableNoTracking.ToListAsync();


            if (id != null) return View(id);
            ViewBag.Groups = await _productGroupRepository.TableNoTracking.ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductInsertViewModel vm, IFormFile file)
        {
            vm.IndexPic = MFile.Save(file, FilePath.Product.GetDescription());

            await _productRepostitory.MapAddAsync(vm);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }

        [ActionRole("ثبت ویژگی‌های کالا")]
        [HasAccess]
        public async Task<IActionResult> SubmitFeature(int id)
        {
            var groupId = await _productRepostitory.GetProductGroupIdbyProductId(id);

            if(groupId == null)
            {
                TempData.AddResult(SweetAlertExtenstion.Error("کالایی با این شناسه یافت نشد"));

                return RedirectToAction(nameof(Index));
            }

            var features =await _productGroupFeatureRepository.GetAllProductGroupFeature(groupId.Value);

            var productFeatures = await _productFeatureRepository.GetAllProductFeatureByProductId(id);

            ViewBag.ProductId = id;
            ViewBag.ProductFeatures = productFeatures;

            return View(features);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFeature(ProductFeatureInsertViewModel vm)
        {
            var model = await _productFeatureRepository.AddFeatureRange(vm);

            TempData.AddResult(model);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// ویژگی های محصولات بر اساس ویژگی های محصول
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAccess]
        public async Task<IActionResult> ProductFeatures(int id)
        {
            var model = await _productGroupFeatureRepository.TableNoTracking.Where(a => a.ProductGroupId == id).ToListAsync();

            return PartialView(model);
        }

    }
}