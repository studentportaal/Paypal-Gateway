using Google.Cloud.PubSub.V1;
using PaypalGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaypalGateway.Service
{
    public class PaymentService
{
        private static PublisherClient publisherclient;
        static public async Task publishMessage (string company)
        {
            List<string> test = new List<string>();
            test.Add(company);
            Console.WriteLine("dit is in de payment service publishmessage:" + test[0]);
            publisherclient = await PublisherClient.CreateAsync(new TopicName("pts6-bijbaan", "paypal-generic"));
            await Domain.Publisher.PublishMessagesAsync(publisherclient, test);
        }
    }
}
