
namespace eCommerce.BusinessEntities
{
    public class CustomerOrder
    {
        public CustomerDetails Customer { get; set; }
        public OrderDetails Order { get; set; }
    }
}
