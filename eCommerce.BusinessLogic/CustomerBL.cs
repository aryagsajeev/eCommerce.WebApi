using eCommerce.BusinessEntities;
using eCommerce.DataAccess;

namespace eCommerce.BusinessLogic
{
    public class CustomerBL : ICustomerBL
    {
        public async Task<ReturnResultModel> ReturnRecentOrder(CustomerArguments customerArguments, string connectionString)
        {
            CustomerRecentOrderDAC custDAC = new CustomerRecentOrderDAC();
            return await custDAC.GetCustomerRecentOrder(customerArguments.User, customerArguments.CustomerId, connectionString);
        }
    }
}

