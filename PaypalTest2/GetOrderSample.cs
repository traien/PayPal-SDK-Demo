using System;
using System.Collections.Generic;
using System.Threading.Tasks;

//1. Import the PayPal SDK client that was created in `Set up Server-Side SDK`.
using PayPalHttp;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace PaypalTest2
{
    public class GetOrderSample
    {

        //2. Set up your server to receive a call from the client
        /*
          You can use this method to retrieve an order by passing the order ID.
         */
        public async static Task<HttpResponse> GetOrder(string orderId, bool debug = false)
        {
            OrdersGetRequest request = new OrdersGetRequest(orderId);
            //3. Call PayPal to get the transaction
            var response = await PayPalClient.client().Execute(request);
            //4. Save the transaction in your database. Implement logic to save transaction to your database for future reference.
            var result = response.Result<Order>();
            Console.WriteLine("Retrieved Order Status");
            Console.WriteLine("Status: {0}", result.Status);
            Console.WriteLine("Order Id: {0}", result.Id);
            //Console.WriteLine("Intent: {0}", result./*Intent*/);
            Console.WriteLine("Links:");
            foreach (LinkDescription link in result.Links)
            {
                Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
            }
            AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
            Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);

            return response;
        }

        /*
          This driver method invokes the getOrder function with
          order ID to retrieve order details.

          To get the correct order ID, this sample uses createOrder to create
          a new order and then uses the newly-created order ID with GetOrder.
         */
        static void Main(string[] args)
        {
            GetOrder("REPLACE-WITH-VALID-ORDER-ID").Wait();
        }
    }
}