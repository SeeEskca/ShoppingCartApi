using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountteWebAPIServices.Models
{
    public class CustomerDetail :IEntity
    {
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }

        public string userId { get; set; }
        public string customerName { get; set; }
        public string addressId { get; set; }

        public void setFields(DataRow dr)
        {
            this.userId = dr["UserId"].ToString();
            this.customerName = dr["Name"].ToString();
            this.street = dr["Street"].ToString();
            this.city = dr["City"].ToString();
            this.state = dr["State"].ToString();
            this.zip = dr["Zip"].ToString();
            this.country = dr["Country"].ToString();
            this.addressId = dr["addressId"].ToString();
        }
    }
}
