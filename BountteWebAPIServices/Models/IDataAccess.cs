using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BountteWebAPIServices.Models
{
    public interface IDataAccess
    {
        DataTable getDataTable(string sql, List<SqlParameter> spt);
        int insertUpdateDelete(string sql, List<SqlParameter> spList);
        object getScalar(string sql);


        //for test
        DataTable testDataTable(string sql);
    }
}