﻿using Core.Utilities;
using DataLayer.Entities;
using DataLayer.ViewModels.Feature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Repos
{
    public class FeatureItemRepository : GenericRepository<FeatureItem>
    {
        public FeatureItemRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<SweetAlertExtenstion> InsertFeatureItem(FeatureItemsViewModel vm)
        {
            var lst = new List<FeatureItem>();

            foreach (var item in vm.Items)
            {
                lst.Add(new FeatureItem()
                {
                    FeatureId = vm.FeatureId,
                    Value = item
                });
            }

            await AddRangeAsync(lst);

            return SweetAlertExtenstion.Ok();
        }


        public async Task<List<FeatureItem>> GetAllFeatureItemByFeatureId(int featureId)
            => await TableNoTracking.Where(a => a.FeatureId == featureId).ToListAsync();
        
    }
}
