using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.SelfHost;
using WebApiLab.Models;

namespace WebApiLab
{
    class Program
    {
        static readonly Uri _baseAddress = new Uri("http://localhost:3002/");

        // Run Visual Studio as Administrator!!
        // test also with Chrome Postman - submit content of JSON file in project, with content-type application/json
        static void Main(string[] args)
        {
            HttpSelfHostServer server = null;
            try
            {
                // Set up server configuration
                HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{id}",
                    defaults: new { id = RouteParameter.Optional }
                );

                // Create server
                server = new HttpSelfHostServer(config);

                // Start listening
                server.OpenAsync().Wait();
                Console.WriteLine("Listening on " + _baseAddress);

                // Run HttpClient issuing requests
                RunClient();

                Console.WriteLine("Hit ENTER to exit...");
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine("Could not start server: {0}", e.GetBaseException().Message);
                Console.WriteLine("Hit ENTER to exit...");
                Console.ReadLine();
            }
            finally
            {
                if (server != null)
                {
                    // Stop listening
                    server.CloseAsync().Wait();
                }
            }
        }

        /// <summary>
        /// Runs an HttpClient issuing a POST request against the controller.
        /// </summary>
        static async void RunClient()
        {
            HttpClient client = new HttpClient();

            User user = new User
            {
                FirstName = "Fernando",
                LastName = "Alonso",
                DateOfBirth = DateTime.Parse("29/7/1981"),
                HomeAddress = new Address
                {
                    Street = "Gran Via",
                    City = "Oviedo",
                    PostCode = "OV12345",
                    Country = "Espana"
                },
                IsApproved = true,
                Roles = new List<Role>{
                    new Role{RoleName = "editor"},
                    new Role{RoleName = "contributor"}
                }
            };

            // Post contact
            Uri address = new Uri(_baseAddress, "/api/users");
            HttpResponseMessage response = await client.PostAsJsonAsync(address.ToString(), user);

            // Check that response was successful or throw exception
            response.EnsureSuccessStatusCode();

            // Read result as User
            User result = await response.Content.ReadAsAsync<User>();

            Console.WriteLine("Result: First Name: {0} Last Name: {1}", result.FirstName, result.LastName);
        }
    }
}
