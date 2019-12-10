using ClassExample1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace ClassExample1.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            IEnumerable<ProductModel> productModels = GetProducts();
            IEnumerable<ProductModel> sortedProducts = from pm in productModels
                                                       orderby pm.Name
                                                       select pm;
            return View(sortedProducts);
        }

        public ActionResult Details(int id)
        {
            ProductModel myProduct = GetProduct(id);

            if ( myProduct != null )
            {
                return View("Details", myProduct);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ProductModel GetProduct(int id)
        {
            return GetProductsFromSQL(id).FirstOrDefault();
        }

        public IEnumerable<ProductModel> GetProducts()
        {
            return GetProductsFromSQL();
        }

        private IEnumerable<ProductModel> GetProductsFromSQL(int id = -1)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString;
            string sqlStatement = "SELECT Id, Name, Image, Price FROM Products";

            if ( id != -1 )
            {
                sqlStatement += $" WHERE Id = {id}";
            }
            else
            {
                sqlStatement += " order by Name desc";
            }

            List<ProductModel> retVal = new List<ProductModel>();

            using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connStr))
            {
                connection.Open();
                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sqlStatement, connection))
                {
                    using (System.Data.SqlClient.SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while ( sdr.Read() )
                        {
                            ProductModel productModel = new ProductModel();
                            productModel.Id = sdr.GetInt32(0);
                            productModel.Name = sdr.GetString(1);
                            productModel.Image = sdr.GetString(2);
                            productModel.Price = sdr.GetDecimal(3);

                            retVal.Add(productModel);
                        }
                    }
                }
            }

            return retVal;
        }

#if NOT_USING_A_DB
        public ProductModel GetProduct(int id)
        {
            ProductModel[] productModels = GetProducts();
            return (from pm in productModels
                    where pm.Id == id
                    select pm).FirstOrDefault();
        }

        public ProductModel[] GetProducts()
        {
            return new ProductModel[]
            {
                new ProductModel()
                {
                    Name="Fall In Love Product",
                    Image = "https://images.yourstory.com/cs/wordpress/2016/08/125-fall-in-love.png?fm=png&auto=format",
                    Price = 200.0,
                    Id = 1
                },
                new ProductModel()
                {
                    Name="Sweet Foam",
                    Image = "https://api.time.com/wp-content/uploads/2018/11/sweetfoam-sustainable-product.jpg?w=800&quality=85",
                    Price = 20.0,
                    Id = 2
                },
                new ProductModel()
                {
                    Name="Egg Sandwhich",
                    Image = "https://api.time.com/wp-content/uploads/2018/11/just-egg-sandwich-sustainable-product.jpg?w=800&quality=85",
                    Price = 5.0, 
                    Id = 3
                },
                new ProductModel()
                {
                    Name="Wierd Bottle",
                    Image = "https://api.time.com/wp-content/uploads/2018/11/larq-sustainable-product.jpg?w=800&quality=85",
                    Price = 15.0,
                    Id = 4
                }
            };            
        }
#endif
        }
}