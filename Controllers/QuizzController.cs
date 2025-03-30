using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuizzController : ControllerBase
    {
        private readonly IQuizzService _quizzService;
        public QuizzController(IQuizzService quizzService)
        {
            _quizzService = quizzService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_quizzService.Get());
            }
            catch (EntryPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_quizzService.GetById(id));
            }
            catch (EntryPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("admin/{id:int}")]
        public IActionResult GetByAdminId(int id)
        {
            try
            {
                return Ok(_quizzService.GetByAdminId(id));
            } catch (EntryPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        

        [HttpPost]
        public IActionResult Add([FromBody] QuizzRequest quizz)
            {
            int id;
            try
            {
                id = _quizzService.Create(quizz);
                return Ok(id);
            }
            catch (EntryPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] QuizzRequest quizz)
        {
            try
            {
                _quizzService.Update(id, quizz);
            }
            catch (ArgumentException ex)
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
                _quizzService.Delete(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deleted");
        }

        [HttpPost("{id:int}/attempt")]
        public IActionResult QuizsAttemptedByUser(int id, [FromBody] UserQuizAttemptRequest userQuizAttempt)
        {
            try
            {
                _quizzService.QuizsAttemptedByUser(id, userQuizAttempt.userId);
            }
            catch (ArgumentException)
            {
                return BadRequest($"Could not find {userQuizAttempt.userId}");
            }
            return Created("user attempt", userQuizAttempt.userId);
        }
    }
}
