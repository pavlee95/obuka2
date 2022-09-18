using DTO.In;
using Microsoft.AspNetCore.Mvc;
using obukaApi.Core;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace obukaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly JwtManager _manager;

        public TokenController(JwtManager manager)
        {
            _manager = manager;
        }

        // POST api/<TokenController>
        [HttpPost]
        public void Post([FromBody] LoginDto dto)
        {

        }
    }
}
