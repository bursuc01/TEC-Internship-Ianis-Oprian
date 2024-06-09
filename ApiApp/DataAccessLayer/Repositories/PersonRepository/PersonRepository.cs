using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.DataAccessLayer.Repositories.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly APIDbContext _context;

        public PersonRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PersonInformation>> GetPersonListAsync()
        {
            var personList = await _context.Persons
                  .Include(x => x.Salary)
                  .Include(x => x.Position)
                  .Select(x => new PersonInformation()
                  {
                      Id = x.Id,
                      Name = x.Name,
                      PositionName = x.Position.Name,
                      DepartmentName = x.Position.Department.DepartmentName,
                      Salary = x.Salary.Amount,
                  }).ToListAsync();

            return personList;
        }

        public async Task<PersonCreation> GetPersonByIdAsync(int id)
        {
            var person = await _context
                .Persons
                .Include(x => x.Salary)
                .Include(x => x.Position)
                .Select(x => new PersonCreation()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Age = x.Age,
                    Email = x.Email,
                    Address = x.Address,
                    PositionId = x.PositionId,
                    SalaryId = x.SalaryId
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            return person;
        }

        public async Task<bool> PostPersonAsync(Person person)
        {
            var position = _context.Positions.FirstOrDefault(x => x.PositionId == person.PositionId);
            if (position == null)
            {
                return false;
            }

            var salary = _context.Salaries.FirstOrDefault(x => x.SalaryId == person.SalaryId);
            if (salary == null)
            {
                return false;
            }

            person.Salary = salary;
            person.Position = position;

            person.Salary.Persons.Add(person);
            person.Position.Persons.Add(person);

            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            var person = await _context.Persons
                .Include(x => x.Position)
                .Include(x => x.Salary)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (person == null)
            {
                return false;
            }

            if (person.Position != null)
            {
                person.Position.Persons.Remove(person);
            }

            if (person.Salary != null)
            {
                person.Salary.Persons.Remove(person);
            }

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutPersonAsync(Person person)
        {
            var updatedPerson = await _context.Persons
                .Include(x => x.Salary)
                .Include(x => x.Position)
                .FirstOrDefaultAsync(x => x.Id == person.Id);

            if (updatedPerson == null)
            {
                return false;
            }

            if (updatedPerson.SalaryId != person.SalaryId) 
            {
                var salary = await _context.Salaries
                .Include(x => x.Persons)
                .FirstOrDefaultAsync(x => x.SalaryId == person.SalaryId);

                if (salary == null)
                {
                    return false;
                }

                updatedPerson.Salary = salary;
            }

            if (updatedPerson.PositionId != person.PositionId)
            {
                var position = await _context.Positions
               .Include(x => x.Persons)
               .FirstOrDefaultAsync(x => x.PositionId == person.PositionId);

                if (position == null)
                {
                    return false;
                }

                updatedPerson.Position = position;
            }

            updatedPerson.Name = person.Name;
            updatedPerson.Surname = person.Surname;
            updatedPerson.Age = person.Age;
            updatedPerson.Email = person.Email;
            updatedPerson.Address = person.Address;

            _context.Persons.Update(updatedPerson);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
