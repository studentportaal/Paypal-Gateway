using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalGateway.Domain
{
    public class PaymentRequest
    {
        public string companyId { get; set; }
        public string jobOfferId { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethodNonce { get; set; }
        public DateTime PaymentDate { get; set; }

        public override string ToString()
        {
            return "PaymentMethodNonce: " + PaymentMethodNonce + " Date: "; /*PaymentDate*/
        }
    }

}
