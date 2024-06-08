using ApiApp.BusinessLogicLayer.DepartmentBLL;
using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var departments = await _departmentService.GetDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(DepartmentCreation department)
        {
            
            if(await _departmentService.PostDepartmentAsync(department))
            {
                return Created("", department);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (await _departmentService.DeleteDepartmentAsync(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartment(DepartmentCreation department)
        {

            if (await _departmentService.PutDepartmentAsync(department))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
