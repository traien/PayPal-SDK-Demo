using PayPalCheckoutSdk.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PaypalTest2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost()]
        public async System.Threading.Tasks.Task<JsonResult> createOrderAsync()
        {
            var resaul = await CreateOrderSample.CreateOrder();
            Console.WriteLine(resaul);
            return Json(resaul.Result<Order>());
        }

        [HttpPost()]
        public async System.Threading.Tasks.Task<JsonResult> getOrderAsync(String orderId)
        {
            var resaul = await GetOrderSample.GetOrder(orderId);
            var result = resaul.Result<Order>();
            Console.WriteLine(result);
            return Json(result);
        }
    }
}