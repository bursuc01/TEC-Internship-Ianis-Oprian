using ApiApp.BusinessLogicLayer.SalaryBLL;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalariesController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        public SalariesController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var salaries = await _salaryService.GetSalariesAsync();
            return Ok(salaries);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _salaryService.DeleteSalaryAsync(id))
            {
                return Ok();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(SalaryInformation salary)
        {
            if (await _salaryService.PostSalaryAsync(salary))
            {
                return Ok();
            }
            return NoContent();
        }
    }
}
