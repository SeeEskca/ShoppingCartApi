using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data;

namespace BountteWebAPIServices.Models
{
   public interface IEntity
    {
       void setFields(DataRow dr);
    }
}