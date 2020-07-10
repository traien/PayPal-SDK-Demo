using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//1. Import the PayPal SDK client that was created in `Set up Server-Side SDK`.
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;

namespace PaypalTest2
{
    public class CreateOrderSample
    {

        //2. Set up your server to receive a call from the client
        /*
          Method to create order

          @param debug true = print response data
          @return HttpResponse<Order> response received from API
          @throws IOException Exceptions from API if any
        */
        public async static Task<HttpResponse> CreateOrder(bool debug = true)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(BuildRequestBody());
            //3. Call PayPal to set up a transaction
            var response = await PayPalClient.client().Execute(request);

            if (debug)
            {
                var result = response.Result<Order>();
                Console.WriteLine("Status: {0}", result.Status);
                Console.WriteLine("Order Id: {0}", result.Id);
                Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
                Console.WriteLine("Links:");
                foreach (LinkDescription link in result.Links)
                {
                    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                }
                AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
                Console.WriteLine("Total Amount: {0} {1}", amount.CurrencyCode, amount.Value);
            }

            return response;
        }

        /*
          Method to generate sample create order body with CAPTURE intent

          @return OrderRequest with created order request
         */
        private static OrderRequest BuildRequestBody()
        {
            OrderRequest orderRequest = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",

                ApplicationContext = new ApplicationContext
                {
                    BrandName = "Jaguar CRM",
                    LandingPage = "BILLING",
                    UserAction = "PAY_NOW",
                    ShippingPreference = "NO_SHIPPING"
                },
                PurchaseUnits = new List<PurchaseUnitRequest>
        {
          new PurchaseUnitRequest
          {
              AmountWithBreakdown = new AmountWithBreakdown
              {
                  CurrencyCode = "USD",
                  Value = "25",
                  AmountBreakdown = new AmountBreakdown
                  {
                      ItemTotal =  new Money
                      {
                          CurrencyCode = "USD",
                          Value = "25"
                      },
                  }
              },
              InvoiceId = "111111",
              CustomId = "22222",
              SoftDescriptor = "Jaguar CRM",
              ShippingDetail = new ShippingDetail
              {
                  Name = new Name
                  {
                      FullName ="free"
                  }
              },
              Items = new List<Item>
              {
                  new Item
                  {
                      Name = "Jaguar CRM small",
                      Category = "DIGITAL_GOODS",
                      Quantity = "1",
                      Sku = "sf",
                      UnitAmount = new Money
                      {
                          CurrencyCode = "USD",
                          Value = "25"
                      },
                      Description="ddddd"
                  }
              }

          }
        }
            };

            return orderRequest;
        }

        /*
           This is the driver function that invokes the createOrder function
           to create a sample order.
        */
        static void Main(string[] args)
        {
            CreateOrder(true).Wait();
        }
    }
}