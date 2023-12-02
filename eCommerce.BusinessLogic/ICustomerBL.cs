using eCommerce.BusinessEntities;

namespace eCommerce.BusinessLogic
{
    public interface ICustomerBL
    {
        Task<ReturnResultModel> ReturnRecentOrder(CustomerArguments customerArguments, string connectionString);

    }
}