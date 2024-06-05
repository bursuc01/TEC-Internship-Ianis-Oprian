using ApiApp.DataAccessLayer.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.DataAccessLayer.Repositories.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly APIDbContext _context;
        public DepartmentRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            var departments = await _context.Departments
                .ToListAsync();

            return departments;
        }
    }
}
