using Google.Cloud.PubSub.V1;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaypalGateway.Domain
{
    public class Subscriber
    {
        public static object CreateSubscription(string projectId, string topicId, string subscriptionId)
        {
            SubscriberServiceApiClient subscriber = SubscriberServiceApiClient.Create();
            TopicName topicName = new TopicName(projectId, topicId);
            SubscriptionName subscriptionName = new SubscriptionName(projectId, subscriptionId);
            try
            {
                Subscription subscription = subscriber.CreateSubscription(
                    subscriptionName, topicName, pushConfig: null,
                    ackDeadlineSeconds: 60);
            }
            catch (RpcException e)
               when (e.Status.StatusCode == StatusCode.AlreadyExists)
            {

            }

            return 0;
        }

        public static async Task<object> PullMessageAsync (string projectId, string subscriptionId, bool acknowledge)
        {
            SubscriptionName subscriptionName = new SubscriptionName(projectId, subscriptionId);
            SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);

            await subscriber.StartAsync(
                async (PubsubMessage message, CancellationToken cancel) =>
                {
                    string text =
                        Encoding.UTF8.GetString(message.Data.ToArray());
                    await Console.Out.WriteLineAsync(
                        $"Message {message.MessageId}: {text}");
                    return acknowledge ? SubscriberClient.Reply.Ack
                        : SubscriberClient.Reply.Nack;
                });

            await Task.Delay(3000);
            await subscriber.StopAsync(CancellationToken.None);

            return 0;
        }
    }
}
