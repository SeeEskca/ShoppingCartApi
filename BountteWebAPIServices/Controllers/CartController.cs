using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

using BountteWebAPIServices.Models;

namespace BountteWebAPIServices.Controllers
{
    [EnableCors(origins: "http://localhost:56666", headers: "*", methods: "*")]
    public class CartController : ApiController
    {
        IBusiness ibiz = GenericFactory<Business, IBusiness>.createInstanceOf();


        [HttpPost]
        [Route("api/all/updatecart/{cartString}")]
        public HttpResponseMessage PostToCart(string cartString)
        {
          
            try
            {
                int status = ibiz.addToCart(cartString);
            }
            catch(Exception ex)
            {
                throw ex;
            }
       
            return Request.CreateResponse(HttpStatusCode.OK);
        }




        [HttpGet]
        [Route("api/all/displaycart/{cartString}")]
        public IHttpActionResult GetShoppingCart(string cartString)
        {
            List<CartItem> cartModelview = new List<CartItem>();

            try
            {
                cartModelview = ibiz.getCartItemsForDisplay(cartString);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return Ok(cartModelview);//return model to list
        }

        [HttpGet]
        [Route("api/all/deleteProductFromCart/{updateString}")]
        public IHttpActionResult GetCartOnDelete(string updateString)
        {
            List<CartItem> cartModelview = new List<CartItem>();

            try
            {
                cartModelview = ibiz.deleteCartItem(updateString);
                if (cartModelview.Count == 0)//return even if list is null
                    return Ok(cartModelview);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(cartModelview);//return model to list
        }

        [HttpGet]
        [Route("api/all/updateCartQty/{updateString}")]
        public IHttpActionResult GetCartOnUpdate(string updateString)
        {
            List<CartItem> cartModelview = new List<CartItem>();

            try
            {
                cartModelview = ibiz.updateCartItem(updateString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(cartModelview);//return model to list
        }

    }
}
