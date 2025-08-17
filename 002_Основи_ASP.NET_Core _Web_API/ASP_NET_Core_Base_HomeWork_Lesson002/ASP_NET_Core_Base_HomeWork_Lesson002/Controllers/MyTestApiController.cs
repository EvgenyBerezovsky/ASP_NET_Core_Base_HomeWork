using Microsoft.AspNetCore.Mvc;

namespace ASP_NET_Core_Base_HomeWork_Lesson002.Controllers
{
    [Route("[controller]")]
    public class MyTestApiController : ControllerBase
    {

        [HttpGet("FirstEndpoint/{data1}")]
        public ActionResult GetData1(string data1)
        {
            if (string.IsNullOrEmpty(data1))
            {
                return BadRequest("Data cannot be null or empty.");
            }

            var result = $"Response from FirstEndpoint with {data1}";
            return Ok(result);
        }

        [HttpGet("SecondEndpoint/{data2}")]
        public ActionResult GetData2(string data2)
        {
            if (string.IsNullOrEmpty(data2))
            {
                return BadRequest("Data cannot be null or empty.");
            }

            var result = $"Response from SecondEndpoint with {data2}";
            return Ok(result);
        }
    }
}
