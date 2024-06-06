using ApiApp.BusinessLogicLayer.PersonDetailsBLL;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.AspNetCore.Mvc;

namespace ApiApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonDetailsController : Controller
    {
        private readonly IPersonDetailsService _personDetailsService;

        public PersonDetailsController(IPersonDetailsService personDetailsService)
        {
            _personDetailsService = personDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var personDetailsList = await _personDetailsService.GetAllPersonDetailsAsync();

            if (personDetailsList == null)
            {
                return NotFound();
            }
            return Ok(personDetailsList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByPersonIdAsync(int id)
        {
            var foundDetails = await _personDetailsService.GetPersonDetailsByIdAsync(id);
            if (foundDetails == null)
            {
                return NotFound();
            }

            return Ok(foundDetails);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync(PersonDetailCreation person)
        {
            if (ModelState.IsValid)
            {
                if (await _personDetailsService.PostPersonDetailsAsync(person))
                {
                    return Created("", person);
                }

                return NotFound();
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _personDetailsService.DeletePersonDetailsAsync(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(PersonDetailCreation person)
        {
            if (await _personDetailsService.PutPersonDetailsAsync(person))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
