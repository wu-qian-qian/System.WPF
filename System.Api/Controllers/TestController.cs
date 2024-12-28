using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace System.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello World";
        }

        [HttpGet]
        [Route("/Get2")]
        public IActionResult Get2(string str)
        {
            using (var sw = new FileStream("E:\\Demo\\System.WPF\\System\\Assets\\Image\\SysIcon.jpg", FileMode.Open))
            {
                var bytes = new byte[sw.Length];
                sw.Read(bytes, 0, bytes.Length);
                sw.Close();
                return new FileContentResult(bytes, "image/jpeg");
            }
        }
    }
}
