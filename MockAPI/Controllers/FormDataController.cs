using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json.Linq;
using Repository;
using Service;

namespace MockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormDataController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<string>> Get(string id)
        {
            FormDataService service = new FormDataService(new FormDataRepository());
            FormData data = service.GetFormData(System.Convert.ToInt32(id));
            return StatusCode(200); ;
        }



        [HttpPost]
        public ActionResult Post([FromBody] FormData data)
        {
            FormDataService service = new FormDataService(new FormDataRepository());
            service.SaveFormData(data);

            return StatusCode(200);
        }
    }
}