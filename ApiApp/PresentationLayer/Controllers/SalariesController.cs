using ApiApp.BusinessLogicLayer.SalaryBLL;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetByIdAsync(int Id)
        {
            var salary = await _salaryService.GetSalaryByIdAsync(Id);
            return Ok(salary);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            if (await _salaryService.DeleteSalaryAsync(Id))
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

        [HttpPut]
        public async Task<IActionResult> PutAsync(SalaryInformation salary)
        {
            if (await _salaryService.PutSalaryAsync(salary))
            {
                return Ok();
            }

            return NoContent();
        }
    }
}
