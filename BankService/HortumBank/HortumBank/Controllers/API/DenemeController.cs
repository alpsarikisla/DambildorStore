using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HortumBank.Controllers.API
{
    public class DenemeController : ApiController
    {
        // GET: api/Deneme
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Deneme/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Deneme
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Deneme/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Deneme/5
        public void Delete(int id)
        {
        }
    }
}
