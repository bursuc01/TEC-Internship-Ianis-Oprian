using ApiApp.BusinessLogicLayer.PersonBLL;
using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        IPersonService _personService;

        public PersonsController(IPersonService personService) 
        {
            _personService = personService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var personList = await _personService.GetPersonListAsync();

            if (personList == null)
            {
                return NotFound();
            }
            return Ok(personList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAsync(int Id)
        {
            var foundPerson = await _personService.GetPersonByIdAsync(Id);
            if (foundPerson == null)
            {
                return NotFound();
            }

            return Ok(foundPerson);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(PersonCreation person)
        {
            if (ModelState.IsValid)
            {
                if (await _personService.PostPersonAsync(person))
                {
                    return Created("", person);
                }

                return NotFound("Salary or Position not set correctly!");
            }
            else
                return BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int Id)
        {
            if (await _personService.DeletePersonAsync(Id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PersonCreation person)
        {
            if(await _personService.PutPersonAsync(person))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
