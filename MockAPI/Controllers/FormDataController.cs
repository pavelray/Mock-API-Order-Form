using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public ActionResult<string> Get()
        {
            FormDataService service = new FormDataService(new FormDataRepository());
            var data = service.GetFormData();

            if (data == null)
            {
                return NotFound();
            }
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(data));
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<string>> Get(string id)
        {
            FormDataService service = new FormDataService(new FormDataRepository());
            FormData data = service.GetFormData(System.Convert.ToInt32(id));

            if (data == null)
            {
                return NotFound(id);
            }
            return Ok(data);

        }



        [HttpPost]
        public ActionResult Post([FromBody] FormData data)
        {
            FormDataService service = new FormDataService(new FormDataRepository());
            bool isSaved = service.SaveFormData(data);

            if (!isSaved)
            {
                return StatusCode(400);
            }
            return Ok(isSaved);
        }
    }
}