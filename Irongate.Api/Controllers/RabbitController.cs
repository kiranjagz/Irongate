using Irongate.Producer.MadProducer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Irongate.Api.Controllers
{
    public class RabbitController : ApiController
    {
        private readonly IProducer _producer;

        public RabbitController(IProducer producer)
        {
            _producer = producer;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public IHttpActionResult Get(int id)
        {
            var fireMessages = _producer.FireMessages();
            return Ok();
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]string value)
        {
            var headers = Request.Headers;
            return Ok(value);
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
