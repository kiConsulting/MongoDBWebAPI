using Microsoft.AspNetCore.Mvc;
using MongoDBWebAPI.Models;
using MongoDBWebAPI.Services;

namespace MongoDBWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly MongoDBService _mongoDBService;

        public UserController(MongoDBService mongoDBService)
        {
            _mongoDBService = mongoDBService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _mongoDBService.GetUsers;

        [HttpGet("{id}")]
        public ActionResult<User> GetById(string id)
        {
            var user = _mongoDBService.GetUserById(id);
            if (user == null) 
                return NotFound();
            return user;
        }

        [HttpGet("age/{minAge}")]
        public ActionResult<List<User>> GetAboveAge(int minAge) =>
            _mongoDBService.GetUsersAboveAge(minAge);

        [HttpGet("{id}/minage/{age}")]
        public ActionResult<User> GetByIdAndMinAge(string id, int age)
        {
            var user = _mongoDBService.GetUserByIdAndMinAge(id, age);

            if (user == null)
            {
                return NotFound("No user found with the given ID and age criteria.");
            }

            return Ok(user);
        }

    }
}