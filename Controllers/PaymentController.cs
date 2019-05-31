using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Braintree;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaypalGateway.Domain;
using PaypalGateway.Interfaces;
using PaypalGateway.Service;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaypaGateway.Controllers
{
    //[Authorize(Roles = "COMPANY")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        public PaymentService paymentService = new PaymentService();
        public IBraintreeConfiguration Config = new BrainTreeConfiguration();
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("token/new")]
        public object CreateToken ()
        {
            var gateway = Config.GetGateway();
            var clienttoken = gateway.ClientToken.Generate();
            return clienttoken;

        }
        
        [HttpPost]
        [Route("transaction")]
        public async Task<object> CheckOutAsync(PaymentRequest paymentRequest)
        {
            string paymentstatus = string.Empty;
            var gateway = Config.GetGateway();
            var request = new TransactionRequest
            {
                Amount = 10.00m,
                PaymentMethodNonce = paymentRequest.PaymentMethodNonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> transactionResult = gateway.Transaction.Sale(request);

            if (transactionResult.IsSuccess())
            {
                paymentstatus = "succeeded";
                paymentRequest.PaymentDate = DateTime.Today;
                paymentRequest.Price = request.Amount;
                string json = JsonConvert.SerializeObject(paymentRequest);
                PaymentService.publishMessage(json);
            }
            else
            {
                string errormessages = "";
                foreach (ValidationError error in transactionResult.Errors.DeepAll())
                {
                    errormessages += "Error: " + error.Message;
                }
                paymentstatus = errormessages;
            }
            return paymentRequest;
        }
    }

}
