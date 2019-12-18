using Core.Utilities;
using DataLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos
{
    public class ProductGalleryRepository : GenericRepository<DataLayer.Entities.ProductGallery>
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductGalleryRepository(DatabaseContext dbContext,IHostingEnvironment hostingEnvironment) : base(dbContext)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// آپلود گالری
        /// </summary>
        /// <returns></returns>
        
        public async Task<SweetAlertExtenstion> UploadGalley(List<IFormFile> files, int productId)
        {
            var lst = new List<ProductGallery>();

            foreach (var item in files)
            {
                lst.Add(new ProductGallery()
                {
                    ProductId = productId,
                    Pic = UploadPic(item)
                });
            }

            await AddRangeAsync(lst);

            return SweetAlertExtenstion.Ok();
        }

        string UploadPic(IFormFile file)
            => MFile.Save(file, FilePath.ProductGallery.GetDescription());

        public void DeletePic(List<string> path)
        {
            var WebAddress = _hostingEnvironment.WebRootPath;

            foreach (var item in path)
            {
                System.IO.File.Delete(WebAddress +"\\"+ item);
            }
        }
        
    }
}
