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
