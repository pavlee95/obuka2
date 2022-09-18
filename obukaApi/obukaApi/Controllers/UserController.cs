using DTO.In;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace obukaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UsersServices _usersServices;
        private readonly BazaContext _bazaContext;


        public UserController(UsersServices usersServices, BazaContext bazaContext)
        {
            _usersServices = usersServices;
            _bazaContext = bazaContext;
    }


        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            var id = 2;
            var ispiti =
                from i in this._bazaContext.UserRoles
                where i.UserId == id
                select new Role
                {
                 Id = i.Role.Id,
                 Name = i.Role.Name
                };

            var ispitiii = ispiti.ToList();
           
            return Ok(_usersServices.GetAll());
        }



        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] UserDto dto)
        {
            _usersServices.CreateUserService(dto);
            return StatusCode(201);
        }

       

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _usersServices.DeleteUserService(id);
            return NoContent();
        }
    }
    public class RoleNeke 
    {
        public int Id { get; set; }
            public string Name { get; set; }
    }
}
