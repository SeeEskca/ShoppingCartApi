using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BountteWebAPIServices.Models
{
   public class Category : IEntity
    {
        public int catId { get; set; }
        public string CatName { get; set; }
        public string productId { get; set; }
        public string imageString { get; set; }


        public void setFields(DataRow dr)
        {
            this.productId = dr["flagProdId"].ToString();
            this.CatName = (string)dr["catName"];
            this.catId = Int32.Parse(dr["catid"].ToString());
            this.imageString = Convert.ToBase64String((byte[])dr["ProdImage"]);
        }

    }
}
