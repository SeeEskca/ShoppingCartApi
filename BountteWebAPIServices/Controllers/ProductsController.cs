using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BountteWebAPIServices.Models;
using System.Web.Http.Cors;
using System.Web;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Data;

namespace BountteWebAPIServices.Controllers
{
    [EnableCors(origins: "http://localhost:56666", headers:"*", methods:"*")]
    //add route prefix
    public class ProductsController : ApiController
    {
        IBusiness ibiz = GenericFactory<Business, IBusiness>.createInstanceOf();

        [HttpGet]
        [Route("api/allCats")]
        public IHttpActionResult GetAllProducts()
        {
            List<Category> pList = new List<Category>();
            pList = ibiz.getAllCats();
            return Ok(pList);
        }

        [HttpGet]
        [Route("api/all/single/{prodid:int}")]
        public IHttpActionResult GetProduct(int prodid)
        {
            List<Product> plist = new List<Product>();
           

           
                plist = ibiz.getProduct(prodid.ToString());
                //var session = HttpContext.Current.Session;

                var prod = (from s in plist where s.productId == prodid.ToString() select s).FirstOrDefault();

                //UserSession.PRICE = prod.price.ToString();
                //UserSession.PRODUCTSHORTNAME = prod.prodShortDesc;
                //UserSession.SMALLIMAGE = prod.imageString;
                //UserSession.PRODUCTID = prod.productId;

                return Ok(prod);
           
           
        }


       
        [HttpGet]
        [Route("api/all/single/specs/{prodid:int}")]
        public IHttpActionResult GetProductSpecs(int prodid)
        {
            DataTable dt = new DataTable();
            List<string> specs = new List<string>();
            dt = ibiz.getProductSpes(prodid);
            string sp = dt.Rows[0]["spec"].ToString();
            string[] spc = sp.Split(',');
            foreach(var spec in spc)
            {
                specs.Add(spec);
            }
            
            return Ok(specs);

        }

        [HttpGet]
        [Route("api/all/singleimage/{prodid:int}")]
        public HttpResponseMessage GetProductImage(int prodId)
        {
            byte[] arr;
            try
            {
                arr = ibiz.getProductImage(prodId);
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            
            return Request.CreateResponse(HttpStatusCode.OK, Convert.ToBase64String(arr));
        }



        [HttpGet]
        [Route("api/all/bycat/{catid:int}")]
        public IHttpActionResult GetProductsByCategory(int catid)
        {
            List<ProdByCat> plist = new List<ProdByCat>();
            plist = ibiz.getProductsByCatId(catid.ToString());
            return Ok(plist);
        }

        [HttpPost]
        [Route("api/all/postprod")]
        public HttpResponseMessage postProduct([FromBody] Product pd)
        {
                       
            try
            {
                if (ModelState.IsValid)
                {
                    if (ibiz.addProduct(pd))
                        return Request.CreateResponse(HttpStatusCode.Created, "Product successfully created...");
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error creating product...");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error in model details...");
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
                       
        }

        [HttpPost]
        [Route("api/all/postimage")]
       // [ValidateMimeMultipartContentFilter]//create filter class
        public async Task<HttpResponseMessage> postImageInfo()
        {
            //   string Folder = "C:\\BountteTemp";
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());

            //assign formData to image object
            ProductImage img = new ProductImage();
            NameValueCollection formData = provider.FormData;
            img.imageDesc = formData["imageDesc"];
            img.imageCode = Int32.Parse(formData["imageCode"]);
            img.productId = Int32.Parse(formData["productId"]);

            //initialize file access
            IList<HttpContent> files = provider.Files;

            //read file strea
            HttpContent file1 = files[0];
            Stream fStream = await file1.ReadAsStreamAsync();

            //convert file stream to byte array
            byte[] barr = new byte[fStream.Length];
            fStream.Read(barr, 0, (int)fStream.Length);
            //assign byte array to image object
            img.image = barr;




            try
            {
                if (ModelState.IsValid)
                {
                   
                    if (ibiz.addImage(img))
                        return Request.CreateResponse(HttpStatusCode.Created,"Product image load was successful...");
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error updating image...");
                }
                else
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid image model information...");
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
