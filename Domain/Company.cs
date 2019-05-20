using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalGateway.Domain
{
    public class Company
{
        public string uuid { get; set; }
        public string jobOfferId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Price { get; set; }
        public string PaymentMethodNonce { get; set; }
    }
}
