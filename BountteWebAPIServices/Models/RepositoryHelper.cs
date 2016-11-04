using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace BountteWebAPIServices.Models
{
    class RepositoryHelper
    {
        RepositoryHelper() { }
        public static List<T> ConverDataTableToList<T>(DataTable dt) where T : IEntity, new()
        {
            List<T> TList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T model = new T();
                model.setFields(dr);
                TList.Add(model);
            }

            return TList;
        }

    }
}