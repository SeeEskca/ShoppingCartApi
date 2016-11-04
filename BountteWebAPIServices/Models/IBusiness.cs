using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data;

namespace BountteWebAPIServices.Models
{
   public interface IBusiness
    {
        List<Category> getAllCats();
        List<Product> getProduct(string prodId);
        List<ProdByCat> getProductsByCatId(string catId);
        bool addProduct(Product pd);
        bool addImage(ProductImage img);
        List<SearchModel> searchCatName(string searchString);
        DataTable getProductSpes(int prodid);

        int addToCart(string cartString);
        List<CartItem> getCartItemsForDisplay(string cartString);
        List<CartItem> deleteCartItem(string updateString);
        List<CartItem> updateCartItem(string updateString);

        List<CustomerDetail> getCustomerDetails(string userId);
        List<PaymentDetail> getPaymentDetails(string userId);

        List<CustomerDetail> getSelectedAddress(string addrid, string userId);
        List<PaymentDetail> getSelectedPayMethod(string pId, string userId);

        byte[] getProductImage(int prodId);//temp method for testing
    }
}