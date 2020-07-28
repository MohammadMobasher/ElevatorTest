using Dapper;
using DNTScheduler.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Service.Scheduler
{
    public class OrdersScheduler : IScheduledTask
    {
        private readonly IDbConnection _connection;

        public OrdersScheduler(IDbConnection connection)
        {
            _connection = connection;
        }

        public bool IsShuttingDown { get ; set; }

        public Task RunAsync()
        {
            if (IsShuttingDown)
                return Task.CompletedTask;

            var sqlQuery = $@"
                            select Id into #tmp from ShopOrder
                            where GetDate() > DATEADD(mi,10,CreateDate) and IsSuccessed = 0


                            update ShopProduct
                            set ShopOrderId =NULL,IsFactorSubmited = 0
                            where ShopOrderId in (select * from #tmp)

                            update ShopOrder 
                            set IsDeleted = 1
                            where Id in (select * from #tmp)

                            If(OBJECT_ID('tempdb..#tmp') Is Not Null)
                            Begin
                                Drop Table #Temp
                            End";

            //_connection.Query(sqlQuery);

            return Task.CompletedTask;
        }
    }
}
