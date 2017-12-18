using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PayPal.Api;
using LearningPlattform.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LearningPlattform.Controllers
{
    [Authorize]
    public class PaypalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public PaypalController()
        {
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Paypal
        [HttpGet]
        public ActionResult Create(int courseId)
        {
            PaymentDetails PD = new PaymentDetails();
            PD.CourseId = courseId;
            PD.IsInvalidData = false;

            return View(PD);
        }

        // PaymentWithCreditCard
        [HttpPost]
        public ActionResult Create(PaymentDetails paymentDetails)
        {
            
            var CurrentUser = db.Users.Find(User.Identity.GetUserId());
            var Course = db.Courses.Include(c => c.Users).FirstOrDefault(d => d.Id == paymentDetails.CourseId);

            Item item = new Item();
            item.name = Course.Name.ToString();
            item.currency = "USD";
            item.price = Course.Price.ToString();
            item.quantity = "1";
            item.sku = "sku";


            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;


            Address billingAddress = new Address();
            billingAddress.city = paymentDetails.City;
            billingAddress.country_code = paymentDetails.CountryCode;
            billingAddress.line1 = paymentDetails.Street;
            billingAddress.postal_code = paymentDetails.PostalCode;
            billingAddress.state = paymentDetails.State;



            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = paymentDetails.CVV;  // CVV here
            crdtCard.expire_month = paymentDetails.ExpiryDate.Month;
            crdtCard.expire_year = paymentDetails.ExpiryDate.Year;
            crdtCard.first_name = CurrentUser.FirstName;
            crdtCard.last_name = CurrentUser.Surname;
            crdtCard.number = paymentDetails.CardNumber.ToString();
            crdtCard.type = "visa";


            Details details = new Details();
            details.shipping = "1";
            details.tax = "1";
            details.subtotal = Course.Price.ToString();

            Amount amnt = new Amount();
            amnt.currency = "USD";

            amnt.total = Convert.ToString(Course.Price + Convert.ToDecimal(details.shipping) + Convert.ToDecimal(details.tax));
            amnt.details = details;


            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = "Payment for " + Course.Name.ToString();
            tran.item_list = itemList;
            tran.invoice_number = Guid.NewGuid().ToString();



            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);



            FundingInstrument fundInstrument = new FundingInstrument();
            fundInstrument.credit_card = crdtCard;



            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);


            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";


            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactions;

            try
            {


                APIContext apiContext = Configuration.GetAPIContext();


                Payment createdPayment = pymnt.Create(apiContext);


                if (createdPayment.state.ToLower() != "approved")
                {
                    paymentDetails.IsInvalidData = true;
                    return View(paymentDetails);
                }
            }
            catch (PayPal.PayPalException ex)
            {
                Logger.Log("Error: " + ex.Message);
                paymentDetails.IsInvalidData = true;
                return View(paymentDetails);
            }

            return RedirectToAction("Enroll", "Courses", new { Id = paymentDetails.CourseId });
        }
    }
}