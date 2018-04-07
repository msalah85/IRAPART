using IRACMS.DA.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IRACMS.DA.Controllers
{
    public class ClientsController : ApiController
    {
        string [] lists = new string[] { "value1", "value2", "value3", "value4", "value5", "value6", "value7", "value8", "value9", "value10" };

        // GET api/values
        public IEnumerable<string> Get()
        {
            return lists;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return lists[id - 1];
        }

        // POST api/values
        [HttpPost]
        public object Post([FromBody]SaveDataModel value)
        {
            return value;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}