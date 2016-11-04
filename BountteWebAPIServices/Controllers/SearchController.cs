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
    public class SearchController : ApiController
    {
        IBusiness ibiz = GenericFactory<Business, IBusiness>.createInstanceOf();
        [HttpGet]
        [Route("api/search/allCat/{searchString}")]
        public IHttpActionResult GETsearchCat(string searchString)
        {
            List<SearchModel> result = new List<SearchModel>();
            result = ibiz.searchCatName(searchString);
            return Ok(result);
        }
    }
}
