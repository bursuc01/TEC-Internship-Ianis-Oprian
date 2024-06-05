using ApiApp.DataAccessLayer.Model;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.DataAccessLayer.Repositories.SalaryRepository
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly APIDbContext _context;

        public SalaryRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSalaryAsync(int id)
        {
            var salary = await _context.Salaries
                .Include(x => x.Persons)
                .FirstOrDefaultAsync(x => x.SalaryId == id);

            if (salary == null)
            {
                return false;
            }

            var tempSalary = await _context.Salaries
                .Include(x => x.Persons)
                .FirstOrDefaultAsync(x => x.SalaryId != id);

            if (tempSalary != null)
            {
                foreach (var person in salary.Persons)
                {
                    person.Salary = tempSalary;
                    person.SalaryId = tempSalary.SalaryId;
                }
            }

            _context.Remove(salary);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Salary>> GetSalariesAsync()
        {
            var salaries = await _context.Salaries
                .ToListAsync();

            return salaries;
        }

        public async Task<bool> PostSalaryAsync(Salary salary)
        {
            await _context.Salaries.AddAsync(salary);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
