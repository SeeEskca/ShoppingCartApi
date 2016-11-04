using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountteWebAPIServices.Models
{
    public class CartItem : IEntity
    {

        //public static List<CartItem> myCartlist = new List<CartItem>();
        //public static List<CartItem> myCartlistCleared = new List<CartItem>();//for use with ClearCart.setCleareCartFields() method


        public string productId { get; set; }
        public string imageString{ get; set; }
        public decimal price { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
       




        public CartItem() { }//private constructor

        public void setFields(DataRow dr)
        {
            this.productId = dr["productid"].ToString();
            this.imageString = Convert.ToBase64String((byte[])dr["ProdImage"]);
            this.quantity = (int)dr["quantity"];
            this.price = (decimal)dr["price"];
            this.productName = dr["prodShortDesc"].ToString();
          

        }
    }
}
