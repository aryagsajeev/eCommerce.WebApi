using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.BusinessEntities;

namespace eCommerce.DataAccess
{
    interface ICustomerRecentOrderDAC
    {
        Task<ReturnResultModel> GetCustomerRecentOrder(string user, string customerId, string connectionString);
    }
}
