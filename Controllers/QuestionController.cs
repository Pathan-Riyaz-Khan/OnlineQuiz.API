using Microsoft.AspNetCore.Mvc;
using ONLINEEXAMINATION.API.Models.DBModel;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Controllers
{
    [Route("quizzes/{QuizId}/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public IActionResult Get(int QuizId)
        {
            try
            {
                return Ok(_questionService.Get(QuizId));
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
                return Ok(_questionService.GetById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add(int QuizId, [FromBody] QuestionRequest question)
        {
            int id;
            try
            {
                id = _questionService.Create(QuizId, question);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/question/" + id, question);
        }

        [HttpPatch("{id:int}")]
        public IActionResult Update(int QuizId, int id, [FromBody] QuestionRequest question)
        {
            try
            {
                _questionService.Update(QuizId, id, question);
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
                _questionService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deleted");
        }
    }
}
