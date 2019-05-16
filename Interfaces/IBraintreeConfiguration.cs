using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalGateway.Interfaces
{
    public interface IBraintreeConfiguration
    {
        IBraintreeGateway GetGateway();
    }
}
