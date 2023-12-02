using System.Data;
using eCommerce.BusinessEntities;
using Microsoft.Data.SqlClient;

namespace eCommerce.DataAccess
{
    public class CustomerRecentOrderDAC: ICustomerRecentOrderDAC
    {
        public async Task<ReturnResultModel> GetCustomerRecentOrder(string user, string customerId, string connectionString)
        {
            ReturnResultModel result = new ReturnResultModel();

            SqlParameter[] param = new SqlParameter[10];
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    param[0] = new SqlParameter("@User", SqlDbType.NVarChar);
                    param[0].Value = user;

                    param[1] = new SqlParameter("@CustomerId", SqlDbType.NVarChar);
                    param[1].Value = customerId;
                    SqlCommand cmd = new SqlCommand("Get_Recent_Order", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    connection.Open();
                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (await dataReader.ReadAsync().ConfigureAwait(false))
                            {
                                if (!(bool)dataReader.GetValue("IsValidCustomer"))
                                {
                                    result.ErrorsMessage = "Invalid customer Input";
                                    result.Success = false;
                                    return result;
                                }
                            }

                            dataReader.NextResult();

                            CustomerOrder customerOrder = new CustomerOrder();
                            if (dataReader.HasRows)
                            {
                                List<Items> itemList = new List<Items>();
                                bool isFirstiteration = true;
                                bool containsGift = false; ;
                                while (await dataReader.ReadAsync().ConfigureAwait(false))
                                {
                                    if (isFirstiteration)
                                    {
                                        customerOrder.Customer.FirstName = (string)dataReader.GetValue("FIRSTNAME");
                                        customerOrder.Customer.LastName = (string)dataReader.GetValue("LASTNAME");
                                        customerOrder.Order.OrderNumber = (int)dataReader.GetValue("ORDERID");
                                        customerOrder.Order.OrderDate = (string)dataReader.GetValue("ORDERDATE");
                                        customerOrder.Order.DeliveryAddress = (string)dataReader.GetValue("DELIVERYADDRESS");
                                        customerOrder.Order.DeliveryExpected = (string)dataReader.GetValue("DELIVERYEXPECTED");
                                        containsGift = (bool)dataReader.GetValue("CONTAINSGIFT");
                                        if (containsGift)
                                        {
                                            Items item = new Items();
                                            item.Product = "Gift";
                                            itemList.Add(item);
                                        }
                                    }
                                    if (!containsGift)
                                    {
                                        Items items = new Items();
                                        items.Product = (string)dataReader.GetValue("PRODUCTNAME");
                                        items.Quantity = (int)dataReader.GetValue("QUANTITY");
                                        items.PriceEach = (int)dataReader.GetValue("PRICE");
                                        itemList.Add(items);
                                    }
                                    isFirstiteration = false;
                                }
                                customerOrder.Order.OrderItems = itemList;

                            }
                            else
                            {
                                dataReader.NextResult();
                                while (await dataReader.ReadAsync().ConfigureAwait(false))
                                {

                                    customerOrder.Customer.FirstName = (string)dataReader.GetValue("FIRSTNAME");
                                    customerOrder.Customer.LastName = (string)dataReader.GetValue("LASTNAME");
                                    customerOrder.Order = null;
                                }
                            }
                            result.Success = true;
                            result.ErrorsMessage = null;
                            result.CustomerRecentOrder = customerOrder;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
