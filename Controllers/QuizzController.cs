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
            catch(EntryPointNotFoundException ex)
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
            catch(EntryPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("admin/{id:int}")]
        public IActionResult GetByAdminId(int AdminId)
        {   
            try
            {
                return Ok(_quizzService.GetByAdminId(AdminId));
            } catch(EntryPointNotFoundException ex)
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
            }
            catch(EntryPointNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/quizz/" + id, quizz);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int AdminId, int id, [FromBody] QuizzRequest quizz)
        {
            try
            {
                _quizzService.Update(AdminId, id, quizz);
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
    }
}
