using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Core.CustomAttributes;
using Core.Utilities;
using DataLayer.DTO;
using DataLayer.DTO.ProductFeatures;
using DataLayer.DTO.Products;
using DataLayer.Entities;
using DataLayer.ViewModels;
using DataLayer.ViewModels.ProductPackage;
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
    [Area("ProductPackage")]
    [ControllerRole("مدیریت پکیج ها")]
    public class ManageProductPackageController : BaseAdminController
    {
        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductPackageRepostitory _productPackageRepostitory;
        private readonly ProductPackageDetailsRepostitory _productPackageDetailsRepostitory;
        private readonly PackageUserAnswerRepository _packageUserAnswerRepository;
        private readonly ProductGroupRepository _productGroupRepository;
        private readonly ProductPackageGroupRepository _productPackageGroupRepository;

        public ManageProductPackageController(UsersAccessRepository usersAccessRepository,
            ProductRepostitory productRepostitory,
            ProductPackageDetailsRepostitory productPackageDetailsRepostitory,
            ProductPackageRepostitory productPackageRepostitory,
            PackageUserAnswerRepository packageUserAnswerRepository,
            ProductGroupRepository productGroupRepository,
            ProductPackageGroupRepository productPackageGroupRepository) : base(usersAccessRepository)
        {
            _productRepostitory = productRepostitory;
            _productPackageDetailsRepostitory = productPackageDetailsRepostitory;
            _productPackageRepostitory = productPackageRepostitory;
            _packageUserAnswerRepository = packageUserAnswerRepository;
            _productGroupRepository = productGroupRepository;
            _productPackageGroupRepository = productPackageGroupRepository;
        }

        [ActionRole("صفحه لیست پکیج ها")]
        public async Task<IActionResult> Index(ProductPackageSearchViewModel searchModel = null)
        {
            var model = await _productPackageRepostitory.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }

        [ActionRole("ثبت پکیج جدید")]
        public async Task<IActionResult> Create()
        {
            var model = await _productGroupRepository.GetListAsync(a => a.Parent == null);

            ViewBag.Groups = model.ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductPackageInsertViewModel product
            , PackageFeatureInsertViewModel vm
            , List<int> groups
            , IFormFile file)
        {
            // ثبت پکیج
            var packageId = await _productPackageRepostitory.CreateAsync(product, file);

            await _packageUserAnswerRepository.AddAnswer(vm, UserId, packageId);

            await _productPackageGroupRepository.AddGroupRange(groups, packageId);

            // نمایش پیغام
            TempData.AddResult(SweetAlertExtenstion.Ok());

            // بازگشت به لیست محصولات
            return RedirectToAction(nameof(Index));
        }


        [ActionRole("ویرایش محصولات")]
        [HasAccess]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productPackageRepostitory
                .TableNoTracking.Where(a => a.Id == id)
                .ProjectTo<ProductPackageFullDTO>()
                .FirstOrDefaultAsync();

            var group = await _productGroupRepository.GetListAsync(a => a.Parent == null);
            var selectedGroup = await _productPackageGroupRepository.GetListAsync(a => a.PackageId == id);

            ViewBag.SelectedGroup = selectedGroup;
            ViewBag.Groups = group.ToList();
            ViewBag.PackageId = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductPackageUpdateViewModel package
            , PackageFeatureInsertViewModel vm
            , List<int> groups
            , IFormFile file)
        {

            if (_productPackageGroupRepository.IsGroupChanged(package.Id, groups))
            {
                await _productPackageDetailsRepostitory.RemoveProductsbyPackageId(package.Id);
            }

            // ویرایش پکیج
            var productId = await _productPackageRepostitory.UpdateAsync(package, file);

            await _productPackageGroupRepository.AddGroupRange(groups, package.Id);
            if (vm.Items != null || vm.Items.Count > 0)
            {
                await _packageUserAnswerRepository.UpdateAnswer(vm, UserId, package.Id);

            }

            TempData.AddResult(SweetAlertExtenstion.Ok());

            // بازگشت به لیست محصولات
            return RedirectToAction(nameof(Index));
        }


        #region فعال / غیرفعال

        [ActionRole("فعال / غیر فعال کردن پکیج")]
        public async Task<IActionResult> ChangeState(int id)
        {
            await _productPackageRepostitory.ChangeStateProduct(id);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }

        [ActionRole("تغییر پکیج عادی به محصول ویژه")]
        public async Task<IActionResult> ChangeSpecialSell(int id)
        {
            await _productPackageRepostitory.ChangeSpecial(id);

            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }

        #endregion


        #region محاسبه قیمت پکیج

        /// <summary>
        /// محاسبه قیمت پکیج
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAccess]
        public async Task<IActionResult> PackagePrice(int id)
        {
            var model = await _productPackageDetailsRepostitory.CalculatePrice(id);

            if (model == null) return Json(false);

            return Json(model);
        }

        #endregion

        /// <summary>
        /// ثبت محصول برای پکیج
        /// </summary>
        /// <param name="id">PackageId</param>
        /// <returns></returns>
        public async Task<IActionResult> SubmitProductForPackage(int id)
        {
            ViewBag.PackageId = id;
            return View();
        }


        [AllowAccess]
        public async Task<IActionResult> ProductList(ProductSearchViewModel search, PaginationViewModel page)
        {
            var model = await _productRepostitory.LoadAsyncCount(
                page.CurrentPage,
                page.PageSize,
                search);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = search;

            return PartialView(model.Item2);
        }



        [AllowAccess]
        public async Task<IActionResult> ProductPackageDetail(int id)
        {
            var model = await _productPackageDetailsRepostitory.TableNoTracking
                    .Include(a => a.Product)
                    .Where(a => a.PackageId == id)
                    .ToListAsync();

            ViewBag.PackageId = id;


            return PartialView(model);
        }



        public async Task<IActionResult> SubmitProduct(int productId, int packageId)
        {
            if (await _productPackageDetailsRepostitory.IsExist(packageId, productId))
            {
                return Json(false);
            }

            _productPackageDetailsRepostitory.Add(new ProductPackageDetails()
            {
                PackageId = packageId,
                ProductId = productId
            });

            var model = await _productPackageDetailsRepostitory.TableNoTracking
                .Include(a => a.Product)
                .Where(a => a.PackageId == packageId)
                .ToListAsync();

            ViewBag.PackageId = packageId;
            return PartialView("ProductPackageDetail", model);
        }

        public async Task<IActionResult> RemoveProduct(int productId, int packageId)
        {
            var model = await _productPackageDetailsRepostitory
                .GetByConditionAsync(a => a.PackageId == packageId && a.ProductId == productId);

            if (model == null) return Json(false);

            _productPackageDetailsRepostitory.Delete(model);

            var list = await _productPackageDetailsRepostitory.TableNoTracking
                .Include(a => a.Product)
                .Where(a => a.PackageId == packageId)
                .ToListAsync();

            ViewBag.PackageId = packageId;
            return PartialView("ProductPackageDetail", list);
        }

        #region ProductPackage

        public async Task<IActionResult> PackageProduct(int id)
        {
            ViewBag.Groups = await _productPackageGroupRepository.getItemsByPackageId(id);
            //ViewBag.Groups = 
            //    await _productGroupRepository
            //    .GetListAsync(a => a.ParentId == null);

            ViewBag.PackageId = id;

            return View();
        }



        public async Task<IActionResult> getProductForPackge(int groupId, int packageId
            , List<int> beforeGroups, string title = null)
        {
            var model = await _productRepostitory
                .GetProductForPackage(packageId, groupId, beforeGroups,title);
            ViewBag.SelectedProducts = await _productPackageDetailsRepostitory
               .GetAllByGroupIdAndPackageId(packageId, groupId);

            ViewBag.BeforeGroups = beforeGroups ?? new List<int>();
            ViewBag.GroupId = groupId;
            ViewBag.PackageId = packageId;
            ViewBag.Title = title;
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProductPackageAddItem(List<int> products
            , int packageId, int groupId)
        {
            var model = await _productPackageDetailsRepostitory
                .ProductPackageAddItem(products, packageId, groupId);

            return Json(true);
        }
        #endregion


        #region Delete

        [ActionRole("حذف پکیج")]
        [HasAccess]
        public async Task<IActionResult> Delete(int Id)
        {
            return View(new DeleteDTO { Id = Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteViewModel model)
        {
            var result = await _productPackageRepostitory.Delete(model.Id);
            TempData.AddResult(result);

            return Redirect(IndexUrlWithQueryString);
        }

        #endregion


    }
}