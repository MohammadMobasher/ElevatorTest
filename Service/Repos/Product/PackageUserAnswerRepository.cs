using Core.Utilities;
using DataLayer.Entities;
using DataLayer.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Product
{
    public class PackageUserAnswerRepository : GenericRepository<PackageUserAnswers>
    {
        public PackageUserAnswerRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }


        public async Task<SweetAlertExtenstion> AddAnswer(PackageFeatureInsertViewModel vm
            , int userId,int packageId)
        {
            var list = new List<PackageUserAnswers>();

            foreach (var item in vm.Items)
            {
                list.Add(new PackageUserAnswers()
                {
                    Answer = item.FeatureValue.ToString(),
                    QuestionId = item.QuestionId,
                    IsManager=true,
                    PackageId = packageId,
                    UserId = userId,
                    FeatureId = item.FeatureId
                });
            }

            await AddRangeAsync(list,false);

            return await SaveAsync();
        }
    }
}
