using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ONLINEEXAMINATION.API.Models.DTO;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Services;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_userService.Get());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_userService.GetById(id));
            }
            catch(EntryPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        //[HttpPost("login")]
        //public IActionResult Get([FromBody] LoginDTO userLogin)
        //{
        //    try
        //    {
        //        return Ok(_userService.CheckUser(userLogin));
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}
        [HttpPost]
        public IActionResult Add([FromBody] UserRequest user)
        {
            int id;
            try
            {
                id = _userService.Create(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/user/" + id, user);
        }

        [HttpPost("{id:int}/UserOption")]
        public IActionResult UserQuestionOption(int id, [FromBody] UserOptionRequest userOption)
        {
            try
            {
                _userService.UserQuestionOption(id, userOption.QuestionId, userOption.OptionId, userOption.QuizId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/user/" + id, userOption);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] UserRequest user)
        {
            try
            {
                _userService.Update(id, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deleted");
        }

    }
}
