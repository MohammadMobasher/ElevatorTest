using AutoMapper.QueryableExtensions;
using DataLayer.DTO.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Repos;
using Service.Repos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elevator.ViewComponents
{
    public class ProductCategoryVC : ViewComponent
    {

        private readonly ProductRepostitory _productRepostitory;
        private readonly ProductGroupRepository _productGroupRepository;

        public ProductCategoryVC(ProductRepostitory productRepostitory
            , ProductGroupRepository productGroupRepository)
        {
            _productRepostitory = productRepostitory;
            _productGroupRepository = productGroupRepository;
        }



        public IViewComponentResult Invoke()
        {
            var LastProduct =  _productRepostitory.TableNoTracking
                .Where(a => a.IsActive == true && !a.IsDeleted)
                .ProjectTo<ProductFullDTO>()
                .OrderByDescending(a => a.CreateDate)
                .ToList();

            ViewBag.Group =  _productGroupRepository.TableNoTracking.ToList();

            return View(LastProduct);
        }
    }
}
