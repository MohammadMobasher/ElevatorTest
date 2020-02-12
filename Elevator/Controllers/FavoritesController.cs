using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos.User;

namespace Elevator.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly FavoritesRepository _favoritesRepository;

        public FavoritesController(FavoritesRepository favoritesRepository)
        {
            _favoritesRepository = favoritesRepository;
        }

        /// <summary>
        /// لیست تمامی علافه مندی ها
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));

            var model = await _favoritesRepository.TableNoTracking.Where(a => a.UserId == userId).ToListAsync();

            return View(model);
        }

        /// <summary>
        /// ثبت علاقه مندی جدید
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Set(int id)
        {
            try
            {
                var userId = int.Parse(User.Identity.FindFirstValue(ClaimTypes.NameIdentifier));

                var model = await _favoritesRepository.TableNoTracking.AnyAsync(a => a.UserId == userId && a.ProductId == id);

                if (model) return Json("این محصول قبلا ثبت شده است");

                await _favoritesRepository.AddAsync(new DataLayer.Entities.Favorites()
                {
                    ProductId = id,
                    UserId = userId,
                    SubmitDate = DateTime.Now
                });

                return Json("محصول با موفقیت به عنوان محصول مورد علاقه ثبت شد");
            }
            catch (Exception)
            {
                return Json("خطایی در سایت رخ داده است لطفا با پشتیبانی تماس بگیرید");
            }
        }


        /// <summary>
        /// حذف علاقه مندی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Remove(int id)
        {
            var model = await _favoritesRepository.TableNoTracking.FirstOrDefaultAsync(a => a.Id == id);

            if (model == null) return Json("اطلاعاتی با این شناسه یافت نشد");

            await _favoritesRepository.DeleteAsync(model);

            return Json("عملیات با موفقیت انجام شد");
        }
    }
}