using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.DataAccessLayer.Repositories.PersonDetailsRepository
{
    public class PersonDetailsRepository : IPersonDetailsRepository
    {
        private readonly APIDbContext _context;
        public PersonDetailsRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeletePersonDetailsAsync(int id)
        {
            var details = await _context.PersonDetails
                .FirstOrDefaultAsync(x => x.Id == id);

            if (details == null) 
            {
                return false;
            }

            _context.PersonDetails.Remove(details);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PersonDetail>> GetAllPersonDetailsAsync()
        {
            var detailsList = await _context.PersonDetails
                .ToListAsync();

            return detailsList;
        }

        public async Task<PersonDetail> GetPersonDetailsByIdAsync(int id)
        {
            var details = await _context.PersonDetails
                .FirstOrDefaultAsync(x => x.PersonId == id);

            return details;
        }

        public async Task<bool> PostPersonDetailsAsync(PersonDetailCreation personDetails)
        {
            var person = await _context.Persons
                .FirstOrDefaultAsync(x => x.Id == personDetails.PersonId);

            if (person == null)
            {
                return false;
            }

            var details = new PersonDetail
            {
                Id = personDetails.Id,
                BirthDay = personDetails.BirthDay,
                PersonCity = personDetails.PersonCity,
                PersonId = personDetails.PersonId,
                Person = person
            };

            await _context.PersonDetails.AddAsync(details);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutPersonDetailsAsync(PersonDetailCreation personDetails)
        {
            var details = await _context.PersonDetails
                .FirstOrDefaultAsync(x => x.Id == personDetails.Id);

            if (details == null)
            {
                return false;
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(x => x.Id == personDetails.PersonId);

            details.Person = person;
            details.BirthDay = personDetails.BirthDay;
            details.PersonCity = personDetails.PersonCity;
            details.PersonId = personDetails.PersonId;

            _context.PersonDetails.Update(details);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
