using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
//using Newtonsoft.Json;
using WebApiLab.Models;

namespace WebApiLab.Controllers
{
    public class UsersController : ApiController
    {     
        // POST api/Users
        public User Post(User user)
        {
            if (!ModelState.IsValid)
            {
                var errors = new Dictionary<string, IEnumerable<string>>();
                foreach (KeyValuePair<string, ModelState> keyValue in ModelState)
                {
                    errors[keyValue.Key] = keyValue.Value.Errors.Select(e => e.ErrorMessage);
                }

                var resp = Request.CreateResponse(HttpStatusCode.BadRequest, errors);
                throw new HttpResponseException(resp);
            }

            return user;
        }

        //public User Post(User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var errors = new List<string>();
        //        foreach (var state in ModelState)
        //        {
        //            foreach (var error in state.Value.Errors)
        //            {
        //                errors.Add(error.ErrorMessage);    // sending binding error message, typically from validation attribute
        //                //if (error.Exception != null)          // send exception message - probably wouldn't want to do this, exposes raw exception info
        //                //    errors.Add(error.Exception.Message);
        //            }
        //        }
        //        var errorsJson = JsonConvert.SerializeObject(errors);
        //        //var errorsJson = JsonConvert.SerializeObject(ModelState);
        //        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
        //        {
        //            Content = new StringContent(errorsJson),
        //            ReasonPhrase = "Invalid JSON object"
        //        };
        //        throw new HttpResponseException(resp);
        //    }

        //    return user;
        //}   
    }
}