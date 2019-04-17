//
//WebAPIConfig.cs
//
public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.

            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();  // Attributes routes

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

// Routing 
routing : api/{controller}/{id}
	http://localhost:23421/api/values/5
	
namespace API.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values :: select all
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5 :: select one
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values  :: Insert
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5  :: update
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 :: delete
        public void Delete(int id)
        {
        }
    }
}

// =============================================================================================
//Sample : products
/* 1. Build model
   2. Build the repository
   3. Build the controller
*/

//1 .Build Model 
public class product
{
	public string Description {get;set;}
	public decimal Price {get;set;}
	public string ProductCode {get;set;}
	public int ProductId {get;set;}
	public string ProductName {get;set;}
	public DateTime ReleaseDate {get;set;}

}
 
//2. Build the repository
// Controller <--> Repository <--> data Source
                        ^
                        |
                      Model 

//
// @"~/App_Data/product.json" file 
//
[
  
  {
    
   "Description": "Leaf rake with 48-inch wooden handle.",
   
   "Price": 19.95,
    
   "ProductCode": "GDN-0011",
    
   "ProductId": 1,
    
   "ProductName": "Leaf Rake",
    
   "ReleaseDate": "2009-03-19T00:00:00-07:00"
  
},
  
{
    
   "Description": "15 gallon capacity rolling garden cart",
    
   "Price": 32.99,
    
   "ProductCode": "GDN-0023",
    
   "ProductId": 2,
    
   "ProductName": "Garden Cart",
    
   "ReleaseDate": "2010-03-18T00:00:00-07:00"
  }
]

// ProductRepository.cs
namespace APM.WebAPI.Models
{
    /// <summary>
    /// Stores the data in a json file so that no database is required for this
    /// sample application
    /// </summary>
    public class ProductRepository
    {
        /// <summary>
        /// Creates a new product with default values
        /// </summary>
        /// <returns></returns>
        internal Product Create()
        {
            Product product = new Product
            {
                ReleaseDate = DateTime.Now
            };
            return product;
        }

        /// <summary>
        /// Retrieves the list of products.
        /// </summary>
        /// <returns></returns>
        internal List<Product> Retrieve()
        {
            var filePath = HostingEnvironment.MapPath(@"~/App_Data/product.json");

            var json = System.IO.File.ReadAllText(filePath);

            var products = JsonConvert.DeserializeObject<List<Product>>(json);

            return products;
        }

        /// <summary>
        /// Saves a new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        internal Product Save(Product product)
        {
            // Read in the existing products
            var products = this.Retrieve();

            // Assign a new Id
            var maxId = products.Max(p => p.ProductId);
            product.ProductId = maxId + 1;
            products.Add(product);

            WriteData(products);
            return product;
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        internal Product Save(int id, Product product)
        {
            // Read in the existing products
            var products = this.Retrieve();

            // Locate and replace the item
            var itemIndex = products.FindIndex(p => p.ProductId == product.ProductId);
            if (itemIndex > 0)
            {
                products[itemIndex] = product;
            }
            else
            {
                return null;
            }

            WriteData(products);
            return product;
        }

        private bool WriteData(List<Product> products)
        {
            // Write out the Json
            var filePath = HostingEnvironment.MapPath(@"~/App_Data/product.json");

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);

            return true;
        }

    }
}

