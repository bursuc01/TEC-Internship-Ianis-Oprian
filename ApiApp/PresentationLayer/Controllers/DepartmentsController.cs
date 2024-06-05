using ApiApp.BusinessLogicLayer.DepartmentBLL;
using ApiApp.DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("{Id}")]
        public IActionResult Get(int Id)
        {
            /*
            var db = new APIDbContext();
            Department department = db.Departments.Find(Id);
            if (department == null)
                return NotFound();
            else
                return Ok(department);
            */
            return NotFound();
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            /*
            if (ModelState.IsValid)
            {
                var db = new APIDbContext();
                db.Departments.Add(department);
                db.SaveChanges();
                return Created("", department);
            }
            else
                return BadRequest();
            */
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateDepartment(Department department)
        {

            //if (ModelState.IsValid)
            //{
            //    var db = new APIDbContext();
            //    Department updateDepartment = db.Departments.Find(department.DepartmentId);
            //    updateDepartment.DepartmentName = department.DepartmentName;
            //    db.SaveChanges();
            //    return NoContent();
            //}
            //else
            //    return BadRequest();
            return NoContent();
        }
    }
}
