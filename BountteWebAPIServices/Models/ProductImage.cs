using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data;
using System.Web;

namespace BountteWebAPIServices.Models
{
   public class ProductImage
    {
      
        public string imageDesc { get; set; }
        public int imageId { get; set; }
        public int productId { get; set; }
        public int imageCode { get; set; }
      
        public byte [] image { get; set; }
    }
}