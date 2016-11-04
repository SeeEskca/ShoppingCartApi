using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BountteWebAPIServices.Models
{
    public class UserSession
    {
        public static readonly string _productID = "PRODUCTID";
        public static readonly string _prodShortName = "PRODUCTSHORTNAME";
        public static readonly string _price = "PRICE";
        public static readonly string _smalImg = "SMALLIMAGE";
        public static readonly string _currentItemCount = "CurrentItemCount";
        public static readonly string _myCart= "MYCART";



        public static string PRODUCTID
        {
            get
            {
                string content = "";
                if (HttpContext.Current.Session[_productID] != null)
                    content = HttpContext.Current.Session[_productID].ToString();
                return content;
            }
            set
            {
                HttpContext.Current.Session[_productID] = value;
            }

        }

        public static string PRODUCTSHORTNAME
        {
            get
            {
                string content = "";
                if (HttpContext.Current.Session[_prodShortName] != null)
                    content = HttpContext.Current.Session[_prodShortName].ToString();
                return content;
            }
            set
            {
                HttpContext.Current.Session[_prodShortName] = value;
            }

        }

        public static string PRICE
        {
            get
            {
                string content = "";
                if (HttpContext.Current.Session[_price] != null)
                    content = HttpContext.Current.Session[_price].ToString();
                return content;
            }
            set
            {
                HttpContext.Current.Session[_price] = value;
            }

        }


        public static string SMALLIMAGE
        {
            get
            {
                string content = "";
                if (HttpContext.Current.Session[_smalImg] != null)
                    content = HttpContext.Current.Session[_smalImg].ToString();
                return content;
            }
            set
            {
                HttpContext.Current.Session[_smalImg] = value;
            }

        }

        public static string CurrentItemCount
        {
            get
            {
                string content = "";
                if (HttpContext.Current.Session[_currentItemCount] != null)
                    content = HttpContext.Current.Session[_currentItemCount].ToString();
                return content;
            }
            set
            {
                HttpContext.Current.Session[_currentItemCount] = value;
            }
        }


        public static string MYCART
        {
            get
            {
                string content = "";
                if (HttpContext.Current.Session[_myCart] != null)
                    content = HttpContext.Current.Session[_myCart].ToString();
                return content;
            }
            set
            {
                HttpContext.Current.Session[_myCart] = value;
            }
        }

    }
}
