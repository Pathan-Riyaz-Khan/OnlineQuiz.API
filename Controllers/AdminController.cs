using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ONLINEEXAMINATION.API.Models.RequestModel;
using ONLINEEXAMINATION.API.Models.ResponseModel;
using ONLINEEXAMINATION.API.Services.Interface;

namespace ONLINEEXAMINATION.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_adminService.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_adminService.GetById(id));
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

        [HttpPost]
        public IActionResult Add([FromBody] AdminRequest admin)
        {
            int id;
            try
            {
                id = _adminService.Create(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created("~/admin/"+id, admin);
        }

        [HttpPatch("{id:int}")]
        public IActionResult Update(int id, [FromBody] AdminRequest admin)
        {
            try
            {
                _adminService.Update(id, admin);
            }
            catch(Exception ex)
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
                _adminService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deleted");
        }
    }
}
