using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.CustomAttributes;
using DataLayer.ViewModels.TreeInfo;
using Microsoft.AspNetCore.Mvc;
using Service.Repos.TreeInfo;
using Service.Repos.User;
using WebFramework.Base;

namespace ElevatorAdmin.Areas.Tree.Controllers
{
    [Area("Tree")]
    [ControllerRole("مدیریت درخت")]
    public class ManageTreeController : BaseAdminController
    {
        private readonly TreeRepository _treeRepository;

        public ManageTreeController(
            UsersAccessRepository usersAccessRepository,
            TreeRepository treeRepository) : base(usersAccessRepository)
        {
            _treeRepository = treeRepository;
        }

        [ActionRole("صفحه لیست اسلایدشو")]
        public async Task<IActionResult> Index(TreeSearchViewModel searchModel)
        {
            var model = await _treeRepository.LoadAsyncCount(
                this.CurrentPage,
                this.PageSize,
                searchModel);

            this.TotalNumber = model.Item1;

            ViewBag.SearchModel = searchModel;

            return View(model.Item2);
        }

        /// <summary>
        /// ویرایش رکورد
        /// در این تابع تنها شماره درخت و همچنین آدرس درخت قرار داده می شود
        /// </summary>
        /// <returns></returns>
        [ActionRole("ثبت درخت")]
        public async Task<IActionResult> Update(int id)
        {
            return View(id);
        }


        [HttpPost]
        public async Task<IActionResult> Update(TreeUpdateViewModel treeUpdateViewModel)
        {
            await _treeRepository.UpdateAsync(treeUpdateViewModel);
            return RedirectToAction(IndexUrlWithQueryString);
        }
    }
}