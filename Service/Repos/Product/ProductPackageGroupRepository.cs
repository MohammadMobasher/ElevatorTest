using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos.Product
{
    public class ProductPackageGroupRepository : GenericRepository<ProductPackageGroups>
    {
        public ProductPackageGroupRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }


        public async Task AddGroupRange(List<int> groups, int packageId)
        {
            DeleteRange(await GetListAsync(a => a.PackageId == packageId));

            var list = new List<ProductPackageGroups>();

            foreach (var item in groups)
            {
                list.Add(new ProductPackageGroups()
                {
                    GroupId = item,
                    PackageId = packageId
                });
            }

            await AddRangeAsync(list);
        }


    }
}
