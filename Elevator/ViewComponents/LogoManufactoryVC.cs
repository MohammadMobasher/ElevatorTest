using DataLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Service.Repos;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class LogoManufactoryVC : ViewComponent
    {

        private readonly LogoManufactoryRepository _logoManufactoryRepository;

        public LogoManufactoryVC(LogoManufactoryRepository logoManufactoryRepository)
        {
            _logoManufactoryRepository = logoManufactoryRepository;
        }



        public IViewComponentResult Invoke(int id)
        {
            var model = _logoManufactoryRepository.TableNoTracking.ToList();
            return View(model);
        }

    }
}
