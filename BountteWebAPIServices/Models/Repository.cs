using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web;

namespace BountteWebAPIServices.Models
{
    public class Repository : IRepository
    {
        IDataAccess idac = null;
        public Repository() : this(GenericFactory<DataAccess, IDataAccess>.createInstanceOf()) { }
        public Repository(IDataAccess idc)
        {
            idac = idc;
        }

        public bool addImage(ProductImage img)
        {
            int status;
            List<SqlParameter> plist = new List<SqlParameter>();
            string sql = "insert into productImage(productId, prodimage, imageCode, imageDesc)" +
                          "values(@prodId, @img, @imgCode, @imgDesc)";
            SqlParameter p1 = new SqlParameter("@prodId", SqlDbType.BigInt);
            p1.Value = img.productId;
            plist.Add(p1);

            SqlParameter p2 = new SqlParameter("@img", SqlDbType.Binary);
            p2.Value =img.image;
            plist.Add(p2);
          



            SqlParameter p3 = new SqlParameter("@imgCode", SqlDbType.Int);
            p3.Value = img.imageCode;
            plist.Add(p3);

            SqlParameter p4 = new SqlParameter("@imgDesc", SqlDbType.VarChar);
            p4.Value = img.imageDesc;
            plist.Add(p4);

            status = idac.insertUpdateDelete(sql, plist);
            if (status > 0)
                return true;

            return false;
        }

        public bool addProduct(Product pd)
        {
            int status;
            List<SqlParameter> plist = new List<SqlParameter>();
            string sql = "insert into products(prodShortDesc,prodLongDesc, prodCatId, " +
                "imageId, vendorId,specificationId,classId, price, inventory, subCatId)" +
                "values(@prodName, @prodDesc, @catId, @imgId, @venId, @specId, @clId, @price, @inv, @subId)";
            SqlParameter p1 = new SqlParameter("@prodName",SqlDbType.VarChar);
            p1.Value = pd.prodShortDesc;
            plist.Add(p1);

            SqlParameter p2 = new SqlParameter("@prodDesc", SqlDbType.VarChar);
            p2.Value = pd.prodLongDesc;
            plist.Add(p2);

            SqlParameter p3 = new SqlParameter("@catId", SqlDbType.Int);
            p3.Value = pd.prodCatId;
            plist.Add(p3);

            SqlParameter p4 = new SqlParameter("@imgId", SqlDbType.Int);
            p4.Value = pd.imageId;
            plist.Add(p4);

            SqlParameter p5 = new SqlParameter("@venId", SqlDbType.Int);
            p5.Value = pd.vendorId;
            plist.Add(p5);

            SqlParameter p6 = new SqlParameter("@specId", SqlDbType.Int);
            p6.Value = pd.specificationId;
            plist.Add(p6);

            SqlParameter p7 = new SqlParameter("@clId", SqlDbType.Int);
            p7.Value = pd.classId;
            plist.Add(p7);

            SqlParameter p8 = new SqlParameter("@price", SqlDbType.Decimal);
            p8.Value = pd.price;
            plist.Add(p8);

            SqlParameter p9 = new SqlParameter("@inv", SqlDbType.Int);
            p9.Value = pd.inventory;
            plist.Add(p9);

            SqlParameter p10 = new SqlParameter("@subId", SqlDbType.Int);
            p10.Value = pd.subCatId;
            plist.Add(p10);

            status = idac.insertUpdateDelete(sql, plist);
            if (status > 0)
                return true;

            return false;
        }

        public int addToCart(string cartString)
        {
            string[] cartSpecs = cartString.Split(',');
            string qty = cartSpecs[0];
            string productId = cartSpecs[1];
            string cartId = cartSpecs[2];

            List<SqlParameter> plist = new List<SqlParameter>();
            DataTable dt = new DataTable();
            string cartLive = string.Empty;

            string sql1 = "select * from offlineshoppingcart where cartId ='" + cartId + "'";
            dt = idac.getDataTable(sql1, null);
            if(dt.Rows.Count > 0)//check to see if an item with same cartId exist on table...All items with same cartId must have same cartLive
            {
                cartLive = dt.Rows[0]["SaveDate"].ToString();//asign existing cartLive to new cart item
            }
            else
                cartLive = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString();//assign to first item of the cart

            string sql = "insert into offlineshoppingcart(productId, qty, saveDate, cartId)  values(@prodId, @quantity, @Date, @cartId)";
            SqlParameter p1 = new SqlParameter("@prodId", SqlDbType.BigInt);
            SqlParameter p2 = new SqlParameter("@quantity", SqlDbType.Int);
            SqlParameter p3= new SqlParameter("@Date", SqlDbType.VarChar);
            SqlParameter p4 = new SqlParameter("@cartId", SqlDbType.VarChar);
            p1.Value = productId;
            p2.Value = qty;
            p3.Value = cartLive;
            p4.Value = cartId;
            plist.Add(p1);
            plist.Add(p2);
            plist.Add(p3);
            plist.Add(p4);


            return idac.insertUpdateDelete(sql, plist);
        }

        public List<CartItem> deleteCartItem(string updateString)
        {
            List<SqlParameter> plist = new List<SqlParameter>();
            DataTable dt = new DataTable();
            string[] cartSpecs = updateString.Split(',');
            string productId = cartSpecs[0];
            string cartId = cartSpecs[1];
           // int deleteState = 0;
            string sql = "delete from offlineshoppingcart where productId=@prodId and cartId=@cartId";
            SqlParameter p1 = new SqlParameter("@prodId", SqlDbType.BigInt);
            SqlParameter p2 = new SqlParameter("@cartId", SqlDbType.VarChar);
            p1.Value = productId;
            p2.Value = cartId;
            plist.Add(p1);
            plist.Add(p2);

            string sql1 = "select ofl.productId, sum(ofl.qty) as quantity, prd.prodshortdesc, prd.price, img.ProdImage" +
                         " from offlineshoppingcart ofl" +
                        " join products prd" +
                        " on ofl.ProductId = prd.productid" +
                        " join productimage img" +
                        " on ofl.ProductId = img.ProductId" +
                        " where ofl.CartId = @cartID and img.ImageDesc = 'small'" +
                        " group by ofl.ProductId, prd.ProdShortDesc, prd.Price, img.ProdImage" +
                        " order by quantity";

            if (idac.insertUpdateDelete(sql,plist) > 0)
            {
                plist =new List<SqlParameter>();//reuse plist
                p2 = new SqlParameter("@cartID", SqlDbType.VarChar);
                p2.Value = cartId;
                plist.Add(p2);
                dt = idac.getDataTable(sql1, plist);
            }


            return RepositoryHelper.ConverDataTableToList<CartItem>(dt);
        }

        public List<Category> getAllCats()
        {

            string sql = "select prd.catid, prd.flagProdId, prd.catName, img.prodimage" +
                        " from ProductCategory prd" +
                        " join productimage img" +
                        " on prd.FlagProdId = img.ProductId" +
                        " where img.imageDesc= 'medium'";

            return RepositoryHelper.ConverDataTableToList<Category>(idac.getDataTable(sql, null));

        }

        public List<CartItem> getCartItemsForDisplay(string cartString)
        {
            List<SqlParameter> plist = new List<SqlParameter>();
           // DataTable dt = new DataTable();
            string sql = "select ofl.productId, sum(ofl.qty) as quantity, prd.prodshortdesc, prd.price, img.ProdImage" +
                         " from offlineshoppingcart ofl" +
                        " join products prd" +
                        " on ofl.ProductId = prd.productid" +
                        " join productimage img" +
                        " on ofl.ProductId = img.ProductId" +
                        " where ofl.CartId = @cartId and img.ImageDesc = 'small'" +
                        " group by ofl.ProductId, prd.ProdShortDesc, prd.Price, img.ProdImage" +
                        " order by quantity";

            SqlParameter p1 = new SqlParameter("@cartId", SqlDbType.VarChar);
            p1.Value = cartString;
            plist.Add(p1);


            List<CartItem> clist = new List<CartItem>();
            //DataTable dt = new DataTable();
            //dt = idac.getDataTable(sql, plist);
            //string s = sql;
            //return clist;
            return RepositoryHelper.ConverDataTableToList<CartItem>(idac.getDataTable(sql, plist));
        }

        public List<CustomerDetail> getCustomerDetails(string userId)
        {
            int uId = Int32.Parse(userId);
            List<SqlParameter> plist = new List<SqlParameter>();
           string sql = "select userid, firstname + ' ' + lastname as Name, street, city, state, zip, country, addressId" +
                        " from customerdetails" +
                        " where userid = @userId";
            SqlParameter p1 = new SqlParameter("@userId", SqlDbType.BigInt);
            p1.Value = uId;
            plist.Add(p1);

            return RepositoryHelper.ConverDataTableToList<CustomerDetail>(idac.getDataTable(sql, plist));
        }

        public List<PaymentDetail> getPaymentDetails(string userId)
        {
            int uId = Int32.Parse(userId);
            List<SqlParameter> plist = new List<SqlParameter>();
            string sql = "select UserId, CardType, CardNumber, CardExpiry, CardSecurity, CardOwnerName, PaymentId" +
                         " from paymentinfo" +
                         " where userid = @userId";
            SqlParameter p1 = new SqlParameter("@userId", SqlDbType.BigInt);
            p1.Value = uId;
            plist.Add(p1);

            return RepositoryHelper.ConverDataTableToList<PaymentDetail>(idac.getDataTable(sql, plist));
        }

        public List<Product> getProduct(string prodId)
        {
            //DataTable test = new DataTable();
            //List<Product> ptest = new List<Product>();
            List<SqlParameter> plist = new List<SqlParameter>();
            long pId = Int64.Parse(prodId);
            string sql = "select prd.productid, prd.prodcatid, prd.prodShortDesc, prd.prodLongDesc, img.prodimage, prd.price, prd.inventory, mnf.multiplespec as specs" +
                        " from products prd" +
                        " join productimage img" +
                        " on prd.productId = img.productId" +
                        " join ManufacturerSpec mnf" +
                        " on prd.productid = mnf.ProdId" +
                        " where img.productId=@productID and img.ImageDesc = 'Large'";
                     //   " img.productId=@productID";
            SqlParameter p1 = new SqlParameter("@productID", SqlDbType.BigInt);
            p1.Value = pId;
            plist.Add(p1);
            //test = idac.getDataTable(sql, plist);
            //ptest = RepositoryHelper.ConverDataTableToList<Product>(test);
            //return ptest;
            return RepositoryHelper.ConverDataTableToList<Product>(idac.getDataTable(sql, plist));
        }

        public byte[] getProductImage(int prodId)
        {
            string sql="select prodimage from productImage where productId= " + prodId  + "and ImageDesc = 'medium' ";
            return (byte[])idac.getScalar(sql);
        }

        public List<ProdByCat> getProductsByCatId(string catId)
        {
            List<SqlParameter> plist = new List<SqlParameter>();
           int cID = Int32.Parse(catId);
            string sql = "select prd.productid, prd.prodcatid, prd.prodShortDesc, prd.prodLongDesc, img.prodimage, prd.price, prd.inventory" +
                        " from products prd" +
                        " join productimage img" +
                        " on prd.imageid = img.imageid" +
                        " where img.productId=prd.productid and prd.prodcatid= @prodcatID";
            SqlParameter p1 = new SqlParameter("@prodcatID", SqlDbType.Int);
            p1.Value = cID;
            plist.Add(p1);
            //DataTable dt = new DataTable();
            //List<ProdByCat> pl = new List<ProdByCat>();
            //dt = idac.getDataTable(sql, plist);
            //pl = RepositoryHelper.ConverDataTableToList<ProdByCat>(dt);
            ////sql = " ";
            //return pl;
          return RepositoryHelper.ConverDataTableToList<ProdByCat>(idac.getDataTable(sql, plist));
        }

        public DataTable getProductSpes(int prodid)
        {
            string pID = prodid.ToString();
            string sql= "select multiplespec as spec from manufacturerspec where prodid =@prodID";
            List<SqlParameter> plist = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@prodID", SqlDbType.VarChar);
            p1.Value = pID;
            plist.Add(p1);
            return idac.getDataTable(sql, plist);


        }

        public List<CustomerDetail> getSelectedAddress(string addrid, string userId)
        {
           
            List<SqlParameter> plist = new List<SqlParameter>();
            string sql="select userid, firstname + ' ' + lastname as Name, street, city, state, zip, country, addressId" +
                         " from customerdetails" +
                         " where userid = @usr and addressId=@addrid";

            SqlParameter p1 = new SqlParameter("@addrid", SqlDbType.Int);
            SqlParameter p2 = new SqlParameter("@usr", SqlDbType.BigInt);
            p1.Value = Int32.Parse(addrid);
            p2.Value = Int32.Parse(userId);
            plist.Add(p1);
            plist.Add(p2);


            return RepositoryHelper.ConverDataTableToList<CustomerDetail>(idac.getDataTable(sql, plist));
        }

        public List<PaymentDetail> getSelectedPayMethod(string pId, string userId)
        {
            List<SqlParameter> plist = new List<SqlParameter>();
            string sql = "select UserId, CardType, CardNumber, CardExpiry, CardSecurity, CardOwnerName, PaymentId" +
                        " from paymentinfo" +
                        " where userid = @usr and paymentId=@pid";
            SqlParameter p1 = new SqlParameter("@pid", SqlDbType.BigInt);
            SqlParameter p2 = new SqlParameter("@usr", SqlDbType.BigInt);
            p1.Value = Int32.Parse(pId);
            p2.Value = Int32.Parse(userId);
            plist.Add(p1);
            plist.Add(p2);


            return RepositoryHelper.ConverDataTableToList<PaymentDetail>(idac.getDataTable(sql, plist));
        }

        public List<SearchModel> searchCatName(string searchString)
        {
          
            string sql = "select catName as category, catId from productcategory where catName like '%' + @schString + '%'";
            List<SqlParameter> plist = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@schString", SqlDbType.VarChar);
            p1.Value = searchString;
            plist.Add(p1);
            
            return  RepositoryHelper.ConverDataTableToList<SearchModel>(idac.getDataTable(sql, plist));
        }

        public List<CartItem> updateCartItem(string updateString)
        {

            string[] cartSpecs = updateString.Split('_');
            string productId = cartSpecs[1];
            int orinalQty = Int32.Parse(cartSpecs[2]);
            int newQty = Int32.Parse(cartSpecs[3]);
            string cartId = cartSpecs[4];

            int insertVal = newQty - orinalQty;//insert only +ve or -ve difference after selection
            string cartString = insertVal + "," + productId + "," + cartId;
            
            return addToCart(cartString) > 0 ? getCartItemsForDisplay(cartId) : null;
        }
    }
}
 