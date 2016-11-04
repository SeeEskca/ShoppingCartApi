using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BountteWebAPIServices.Models;

namespace BountteWebAPIServices.Controllers
{
    [EnableCors(origins: "http://localhost:56666", headers: "*", methods: "*")]
    public class CheckOutController : ApiController
    {
        IBusiness ibiz = GenericFactory<Business, IBusiness>.createInstanceOf();
       

        [HttpGet]
        [Route("api/all/getCustomerDetail/{username}")]
        public IHttpActionResult GetCustomerDetails(string username)
        {

            List<CustomerDetail> cdList = new List<CustomerDetail>();
            try
            {
                cdList = ibiz.getCustomerDetails(username);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return Ok(cdList);

        }


        [HttpGet]
        [Route("api/all/getpaymentDetail/{username}")]
        public IHttpActionResult GetPaymentDetails(string username)
        {

            List<PaymentDetail> cpList = new List<PaymentDetail>();
            try
            {
                cpList = ibiz.getPaymentDetails(username);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return Ok(cpList);

        }

        [HttpGet]
        [Route("api/all/getSelectedAddress/{details}")]
        public IHttpActionResult GetSelectedAddress(string details)
        {
            string[] dparams = details.Split('_');
            string addrid = dparams[0];
            string userId = dparams[1];
           
            List<CustomerDetail> cd = new List<CustomerDetail>();
            if (addrid == "undefined" || userId == "undefined")
                return Ok();
            cd = ibiz.getSelectedAddress(addrid, userId);
            var addr = (from p in cd where p.addressId == addrid select p).FirstOrDefault<CustomerDetail>();
           
           return Ok(addr);
        }

        [HttpGet]
        [Route("api/all/getSelectedPaymentDetail/{details}")]
        public IHttpActionResult GetSelectedPayMehtod(string details)
        {
            string[] dparams = details.Split('_');
            string pid = dparams[0];
            string userId = dparams[1];

            List<PaymentDetail> pd = new List<PaymentDetail>();
            if (pid == "undefined" || userId == "undefined")
                return Ok();
            pd = ibiz.getSelectedPayMethod(pid, userId);
            var payMet = (from p in pd where p.paymentId == pid select p).FirstOrDefault<PaymentDetail>();

            return Ok(payMet);
        }
    }
}
