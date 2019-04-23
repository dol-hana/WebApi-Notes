//
// Angular PWA Progressive Web App
//

// https://github.com/designcourse/angular-6-pwa

1. ng n test1
2. ng add @angular/pwa :: add pwa functionality into a project


//Index.html

<!doctype html>
<html lang="en">

<head>
  <meta charset="utf-8">
  
<title>Jokes</title>
  <base href="/">

  
<meta name="viewport" content="width=device-width, initial-scale=1">
  
<link rel="icon" type="image/x-icon" href="favicon.ico">
  
<link rel="manifest" href="manifest.json">
  
<meta name="theme-color" content="#1976d2">
  
<link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet">

</head>

<body>
  
<app-root></app-root>

</body>

</html>



// manifest.json 
{
  "name": "jokes",
  "short_name": "jokes",
  "theme_color": "#1976d2",
  "background_color": "#fafafa",
  "display": "standalone",
  "scope": "/",
  "start_url": "/",
  "icons": [
    {
      "src": "assets/icons/icon-72x72.png",
      "sizes": "72x72",
      "type": "image/png"
    },
    {
      "src": "assets/icons/icon-96x96.png",
      "sizes": "96x96",
      "type": "image/png"
    },
    {
      "src": "assets/icons/icon-128x128.png",
      "sizes": "128x128",
      "type": "image/png"
    },
    {
      "src": "assets/icons/icon-144x144.png",
      "sizes": "144x144",
      "type": "image/png"
    },
    {
      "src": "assets/icons/icon-152x152.png",
      "sizes": "152x152",
      "type": "image/png"
    },
    {
      "src": "assets/icons/icon-192x192.png",
      "sizes": "192x192",
      "type": "image/png"
    },
    {
      "src": "assets/icons/icon-384x384.png",
      "sizes": "384x384",
      "type": "image/png"
    },
    {
      "src": "assets/icons/icon-512x512.png",
      "sizes": "512x512",
      "type": "image/png"
    }
  ]
}



// main.ts
import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { AppModule } from './app/app.module';
import { environment } from './environments/environment';

if (environment.production) {
  enableProdMode();
}

platformBrowserDynamic().bootstrapModule(AppModule)
  .catch(err => console.log(err));


//app/app.component.html
<!--The content below is only a placeholder and can be replaced.-->
<div style="text-align:center">
 
  <h1>Jokes</h1>
  
  <p *ngIf="joke">{{ joke.value }}</p>


//app/app.component.ts

import { Component } from '@angular/core';
import { SwUpdate } from '@angular/service-worker';  // for sw update
import { DataService } from './data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  update: boolean = false;
  joke: any;

  constructor(updates: SwUpdate, private data: DataService) {
    updates.available.subscribe(event => {
      updates.activateUpdate().then(() => document.location.reload());
    })
  }
  ngOnInit() {
    this.data.gimmeJokes().subscribe(res => {
      this.joke = res;
    })
  }
}


// app/app.module.ts

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';

import { HttpClientModule } from '@angular/common/http';
import { DataService } from './data.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: environment.production })
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }


// app/data.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient) { }

  gimmeJokes() {
    return this.http.get('https://api.chucknorris.io/jokes/random')
  }
}



// ======================================================================================================================
// ======================================================================================================================
// ======================================================================================================================


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


//$http 
Built into Angular
Facilitates communication with a back-end service
Asynchronous
$http.get ("/api/products/")
	.then(function(response) {
		vm.products = response.data;
	      });


//REST : Representational State Transfer
	resourses are identified with a URL
 	/api/products/

	requests utilize a standard set of HTTP verbs
	GET | POST | PUT | DELETE

$resourse
	Abstraction on top of $http for calling RESTful service
	
	function productResource($resource) {
	  return  $resource("/api/products/:id")
	}


	productResource.query(function (data) {
		vm.products = data;
	    });


// Product Controller
namespace API.Controllers
{
    public class ProductsController : ApiController
    {
        // GET: api/Products
        public IEnumerable<Product> Get()
        {
            var productRepository = new ProductRepository();
            return productRepository.Retrieve();
        }

        // GET: api/Products/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Products
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Products/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Products/5
        public void Delete(int id)
        {
        }
    }
}


// Angular 1.7 version Web api call

View   Angular controller 	|	Angular module           |
	(productListCtrl)	|	(common.services)  	 |	Web API service
				|				 |	
		MODEL		|	Custom Angular 		 |
				|	service		      <======>
				|	(productResource)	 |
				|	Build in $resource 	 |
				|	service			 |



// CORS  : Cross Origin Resource Sharing : same scheme, same machine, same port

Browser  								Server	
html		===========http://localhost:52387  =================>  
Angular/
javascript 	===========http://localhost:52377/api/products  ====>  WEB API service

JSONP :  for targetting old browser
CORS : download the cors package
	call EnableCors method
		config.EnableCors();
	Set EnableCorsAttribute
		[EnableCorsAttrubute("http://localhost:52345", "*", "*")]



// Serialization Formatter
	Camel case property names
	config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
		new CamelCasePropertyNamesContractResolver();




// Passing parameters
 1. Query string http://stackoverflow.com/questions?aid=2353232&txt=angular-time-validation
	productResource.query({ search: vm.searchCriteria }, function (data) {
            vm.products = data;
        });

 2. URL Path : http://stackoverflow.com/questions/2353232/angular-time-validation


// Odata : Open Data Protocol
	standard way to access data using HTTP
	largely follows REST conventions
	OData queries
		$filter : "contains (ProductCode, 'GDN')
				and Price lt 10"
		$orderby : "Price desc"

	Enable OData query in the Web API : 
		(1)[EnableQuery()]
		public IEnumerable<Product> Get()  ==> (2)public IQueryable<Product> Get()
		{
			var productRepository = new ProductRepository();
			return productRepository.Retrieve();  ==> (3)return productRepository.Retrieve().AsQueryable();
		}

	Query options
		$top:3
		$skip:1
		$orderby: "ProeductName desc"
		$filter:"Price gt 5"
		$select:"Proeductname, ProductCode"

	Query in Angulay
	productResource.query({
		$filter:"contains(productCode, 'GDN') and Price lt 10",
		$orderby:"Price desc", function(data) {
			vm.products = data;
		});

	$filter string functions
		startwith, endswith, contains, tolower, toupper, 
		atithmetioc operators (Addition, substraction,...),
		Date functions (year, month, day, now)
		Geo functions ( distance, length, ...)
		Literals(null, ...) 

	Security considerations
		Use JsonIgnore attribute as needed
		Consider liniting the page size 
			[EnableQuery(PageSize=50)]

		Consider limiting the available query options



// Token-based authentication

   username / password : Login ==========Post request=========> Authentication server
								(Token Service)
									|
				<==========Post response=========   	|
				{"access_token:aS987Hd...}		V
								Membership DB


  Product management screen	==========Get request=========> Product API
				api/products/5
				<==========Post request=========

// Register 
			Register ==========Post request=========> Account API
				api/account/Register		
									|
									V
								Membership DB

// Ahthentication options
	No Authentication
	Individual user accounts : for applications that store user profile ina SQL server database. 
				   Users can register, or sign in using their existing account for Facebook, Google, Microsoft, etc.
	Organizatiional accounts : Active directory, Azure
	Windows authentication : Intranet Only

// Building a login form
//	mainCtrl.js
	
(function () {
	"use strict";
	angular
	.module("productManagement")
	.controller("mainCtrl",
		["userAccount", MainCtrl]);

	function MainCtrl(userAccount) {
		var vm = this;
		vm.isLoggedIn = false;
		vm.message = '';
		vm.userData = {
			userName : '',
			email : '',
			password: '',
			confirmPassword:''
		}

		vm.registeruser = function () {  // see following section
		}

		vm.login = function () {
		  vm.userData.grant_type = "password";
                  vm.userData.userName = vm.userData.email;
		  userAccount.login.loginuser(vm.userData, 
			function (data) {
			  vm.isLoggedin = true;
			  vm.message = "";
			  vm.password = "";
			  vm.token = data.access_token;
			}
		}
	}

}) ();


// common folder
// userAccount.js
{function () {
	"use strict";
	angular
	.module("common.services")
	.factory("userAccount",
		["$resource", userAccount]);

	function userAccount($resource, appSettings) {  // ====> See below
		return $resource(appSettings.serverPath + "/api/Account/Register", null,
			{
				'registeruser': { method: 'POST' },
			});
	}
}) ();


//		vm.registeruser = function () {  // see following section
		}
		|
		|
		V

	vm.registeruser = function () { 
		vm.userData.confirmPassword = vm.userData.password;

		userAccount.registerUser(vm.userData,
			function(data) {
				vm.confirmPassword = "";
				vm.message = "... Registration successful";
				vm.login();
			});		
	}



// Login the user  in
POST request body
      userName=abc%40abc.com&password=Abc_123&grant_type=password
POST request Header
	Content-Type : application/x-www-form-urlencoded



// 	function userAccount($resource, appSettings) {  // ====> See below
		return $resource(appSettings.serverPath + "/api/Account/Register", null,
			{
				'registeruser': { method: 'POST' },
			});
	}
		|
		|
		V

 	function userAccount($resource, appSettings) {  
		return {
			  registration : $resource(appSettings.serverPath + "/api/Account/Register", null,
			  	{
				  'registerUser': { method: 'POST' },
				}
			  }),
			  login: $resource(appSettings.serverPath + "/Token", null,
			  {
				  'loginUser': { 
					method: 'POST',
				  	headers: { Content-Type': 'application/x-www-form-urlencoded'},
					transformRequest: function (data, headersGetter) {
					  var str= [];
					  for(var d in data) 
					     str.push(encodeURIComponent(data[d]));
				          return str.join("&");
					}
				}
			  }),
	        }
	}
	

// how to fix CORS issue for login : No 'Access-Control-Allow-Orgin' header is present on the requested resource.
// ApplicationOAuthProvider.cs
   public override async Task GrantResourceOwnerCredentials ( OAuthGrantResourceownerCredentials....)
   {

	context.OwinContext.Response.Headers.Add("Access-Control-Allow-Orgin",
		new[] { "http://localhost:52436" });

          .... ....
   }

// Protecting a resource with the Authorize attribute
	controller
	action method

// Authorize options
	Roles : [Authorize(Roles="Administrators, Support")]
	users : [Authorize(Users="abc@abc.com, admin@abc.com")]

// ProductController

[Authorize()]
public IHttpActionResult Get(int id)
{

}

//WebApiConfig.cs

// Uncommnet 2 lines
	config.SuppressDefaultHostAuthentication();
	config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

//
//Accessing a restricted resource
//

new service : currentUser
currentUser.js
{function () {
	"use strict";
	angular
	.module("common.services")
	.factory("currentUser",
		 currentUser);

	function currentUser() {  
	  var profile = {
	    isloggedIn : false,
  	    userName " '',
	    token: ''
	  };

	  var setProfile = function(username, token) {
	    profile.username = username;
	    profile.token = token;
	    profile.isloggedIn  = true;
	  };


	  var getProfile = function () {
	    return profile;
	  }
	  return {
	    setProfile: setProfile,
	    getProfile: getProfile
	  }
})();
