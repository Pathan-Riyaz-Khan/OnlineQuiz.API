using Microsoft.AspNetCore.Mvc;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Controllers
{
    [Route("questions/{questionId}/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _optionService;
        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet]
        public IActionResult GetOptionsByQuestion(int questionId)
        {
            try
            {
                return Ok(_optionService.Get(questionId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_optionService.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult add(int questionId, [FromBody] OptionRequest option)
        {
            int id;
            try
            {
                id = _optionService.Create(questionId, option);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/answer/" + id, option);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int questionId, int id, [FromBody] OptionRequest option)
        {
            try
            {
                _optionService.Update(questionId, id, option);
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
                _optionService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deleted");
        }
    }
}
