using Microsoft.AspNetCore.Mvc;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Controllers
{
    [Route("questions/{questionId}/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public IActionResult GetAnswerByQuestion(int questionId)
        {
            try
            {
                return Ok(_answerService.Get(questionId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            AnswerResponse question;
            try
            {
                return Ok(_answerService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult addUser(int questionId, [FromBody] AnswerRequest answer)
        {
            int id;
            try
            {
                id = _answerService.Create(questionId, answer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/answer/" + id, answer);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int questionId, int id, [FromBody] AnswerRequest answer)
        {
            try
            {
                _answerService.Update(questionId, id, answer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Updated");
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int questionId, int id)
        {
            try
            {
                _answerService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deleted");
        }
    }
}
