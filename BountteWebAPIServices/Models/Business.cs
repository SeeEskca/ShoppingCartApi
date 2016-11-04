using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BountteWebAPIServices.Models
{

    public class Business : IBusiness
    {
        IRepository irep;
       public Business(): this(GenericFactory<Repository, IRepository>.createInstanceOf()) { }
        public Business(IRepository rep)
        {
            irep = rep;
        }

        public bool addImage(ProductImage img)
        {
            return irep.addImage(img);
        }

        public bool addProduct(Product pd)
        {
            return irep.addProduct(pd);
        }

        public int addToCart(string cartString)
        {
            return irep.addToCart(cartString);
        }

        public List<CartItem> deleteCartItem(string updateString)
        {
            return irep.deleteCartItem(updateString);
        }

        public List<Category> getAllCats()
        {
            return irep.getAllCats();
        }

        public List<CartItem> getCartItemsForDisplay(string cartString)
        {
            return irep.getCartItemsForDisplay(cartString);
        }

        public List<CustomerDetail> getCustomerDetails(string userId)
        {
            return irep.getCustomerDetails(userId);
        }

        public List<PaymentDetail> getPaymentDetails(string userId)
        {
            return irep.getPaymentDetails(userId);
        }

        public List<Product> getProduct(string prodId)
        {
            return irep.getProduct(prodId);
        }

        public byte[] getProductImage(int prodId)
        {
            return irep.getProductImage(prodId);
        }

        public List<ProdByCat> getProductsByCatId(string catId)
        {
            return irep.getProductsByCatId(catId);
        }

        public DataTable getProductSpes(int prodid)
        {
            return irep.getProductSpes(prodid);
        }

        public List<CustomerDetail> getSelectedAddress(string addrid, string userId)
        {
            return irep.getSelectedAddress(addrid, userId);
        }

        public List<PaymentDetail> getSelectedPayMethod(string pId, string userId)
        {
            return irep.getSelectedPayMethod(pId, userId);
        }

        public List<SearchModel> searchCatName(string searchString)
        {
            return irep.searchCatName(searchString);
        }

        public List<CartItem> updateCartItem(string updateString)
        {
            return irep.updateCartItem(updateString);
        }
    }
}