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
        public ManageProductController(UsersAccessRepository usersAccessRepository,
            ProductRepostitory productRepostitory,
            ProductGroupRepository productGroupRepository,
            ProductUnitRepository productUnitRepository) : base(usersAccessRepository)
        {
            _productRepostitory = productRepostitory;
            _productGroupRepository = productGroupRepository;
            _productUnitRepository = productUnitRepository;
        }



        [ActionRole("صفحه کالاها")]
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
        public async Task<IActionResult> Create(ProductInsertViewModel vm,IFormFile file)
        {
            vm.IndexPic = MFile.Save(file, FilePath.Product.GetDescription());

            await _productRepostitory.MapAddAsync(vm);
                
            TempData.AddResult(SweetAlertExtenstion.Ok());

            return RedirectToAction(nameof(Index));
        }
    }
}