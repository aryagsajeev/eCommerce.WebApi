using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.BusinessEntities
{
    public class ReturnResultModel
    {
        /// <summary>
        /// Gets or sets Success
        /// </summary>
        public bool? Success { get; set; }

        /// <summary>
        /// Gets or sets Errors
        /// </summary>
        public string ErrorsMessage { get; set; }

        /// <summary>
        /// Gets or sets Recent customer Order
        /// </summary>
        public CustomerOrder CustomerRecentOrder { get; set; }
}
    
}
