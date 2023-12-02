
namespace eCommerce.BusinessEntities
{
    public class OrderDetails
    {
        public int OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public List<Items> OrderItems { get; set; }
        public string DeliveryExpected { get; set; }

    }
}
