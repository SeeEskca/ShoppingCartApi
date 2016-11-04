using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;

namespace BountteWebAPIServices.Models
{
   class GenericFactory <T,I> where T:I
    {
        GenericFactory() { }

        public static I createInstanceOf(params object[] args)
        {
            return (I)Activator.CreateInstance(typeof(T), args);
        }
    }
}