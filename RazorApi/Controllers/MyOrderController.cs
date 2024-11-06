using Microsoft.AspNetCore.Mvc;
using RazorApi.Models;
using Razorpay.Api;

namespace RazorApi.Controllers
{
    public class MyOrderController : Controller
    {
        [BindProperty]
        public EntityOrder _OrderDetails { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult CreateOrder()
        {

            string key = "rzp_test_7Y0VFRE62M2jdJ";
            string secret = "8snZYexHVDauBe0urBVcU0fK";

            Random _random = new Random();
            string TransactionId = _random.Next(0,3000).ToString();

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDecimal(_OrderDetails.Amount)*100); // this amount should be same as transaction amount
            input.Add("currency", "INR");
            input.Add("receipt", TransactionId);

           

            RazorpayClient client = new RazorpayClient(key, secret);

            Razorpay.Api.Order order = client.Order.Create(input);
            ViewBag.orderId = order["id"].ToString();

            return View("Payment",_OrderDetails);
        }

        public ActionResult Payment(string razorpay_payment_id, string razorpay_order_id, string razorpay_signature)
        {
            //RazorpayClient client = new RazorpayClient("[rzp_test_7Y0VFRE62M2jdJ]", "[8snZYexHVDauBe0urBVcU0fK]");

            Dictionary<string, string> attributes = new Dictionary<string, string>();

            attributes.Add("razorpay_payment_id", razorpay_payment_id);
            attributes.Add("razorpay_order_id", razorpay_order_id);
            attributes.Add("razorpay_signature", razorpay_signature);
            Utils.verifyPaymentSignature(attributes);
            EntityOrder orderDtl = new EntityOrder();
            orderDtl.TransactionId = razorpay_payment_id;
            orderDtl.OrderId = razorpay_order_id;

           

            return View("PaymentSuccess",orderDtl);

        }


        

    }
}
