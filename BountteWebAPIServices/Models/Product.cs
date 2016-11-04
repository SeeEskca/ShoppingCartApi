using System;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BountteWebAPIServices.Models
{
    public class Product : IEntity
    {
        public string productId { get; set; }
        public string prodShortDesc { get; set; }
        public string prodLongDesc { get; set; }
        public string prodCatId { get; set; }
        public string imageString { get; set; }
        public string specs { get; set; }

        public decimal price { get; set; }

        
        public int inventory { get; set; }
        public int vendorId { get; set; }
        public int imageId { get; set; }
        public int specificationId { get; set; }
        public int classId { get; set; }
        public int subCatId { get; set; }
        


        public void setFields(DataRow dr)
        {
            this.productId = dr["productid"].ToString();
            this.prodShortDesc = (string)dr["ProdShortDesc"];
            this.prodLongDesc = (string)dr["ProdLongDesc"];
            this.prodCatId = dr["ProdCatId"].ToString();
            this.price = (decimal)dr["Price"];
            this.imageString = Convert.ToBase64String((byte [])dr["ProdImage"]);
            this.inventory = Int32.Parse(dr["Inventory"].ToString());
            this.specs = dr["specs"].ToString();
        }
    }
}