using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountteWebAPIServices.Models
{
    public class ProdByCat: IEntity
    {

        public string productId { get; set; }
        public string prodShortDesc { get; set; }
        public string prodLongDesc { get; set; }
        public string prodCatId { get; set; }
        public string imageString { get; set; }

        public decimal price { get; set; }


        public int inventory { get; set; }

        public void setFields(DataRow dr)
        {
            this.productId = dr["productid"].ToString();
            this.prodShortDesc = (string)dr["ProdShortDesc"];
            this.prodLongDesc = (string)dr["ProdLongDesc"];
            this.prodCatId = dr["ProdCatId"].ToString();
            this.price = (decimal)dr["Price"];
            this.imageString = Convert.ToBase64String((byte[])dr["ProdImage"]);
            this.inventory = Int32.Parse(dr["Inventory"].ToString());
        }
    }
}
