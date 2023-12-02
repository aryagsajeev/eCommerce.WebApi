using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using eCommerce.BusinessEntities;
using eCommerce.BusinessLogic;


namespace eCommerce.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private ICustomerBL _customerBL;
        private IConfiguration _configuration;
        public CustomerController(ICustomerBL customer, IConfiguration iConfiguration)
        {
            _customerBL = customer;
            _configuration = iConfiguration;
        }

        [HttpPost]
        [Route("User")]
        public async Task<IActionResult> CustomerRecentOrder([FromBody] CustomerArguments customerArguments)
        {
            try
            {
                string connectionString = _configuration["ConnectionStrings:DefaultConnection"];
                ReturnResultModel result = await _customerBL.ReturnRecentOrder(customerArguments,connectionString);
                if ((bool)result.Success)
                {
                    return Ok(result);
                }
                else
                {
                    return this.BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                ReturnResultModel result = new ReturnResultModel();
                result.Success = false;
                result.ErrorsMessage = ex.Message;
                return this.BadRequest(result);
            }
        }
    }
}
        