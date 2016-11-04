using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountteWebAPIServices.Models
{
    public class SearchModel : IEntity
    {
        public string catName { get; set; }
        public string catId { get; set; }

        public void setFields(DataRow dr)
        {
            this.catName = dr["category"] as string;
            this.catId = dr["catId"].ToString();
        }
    }
}
