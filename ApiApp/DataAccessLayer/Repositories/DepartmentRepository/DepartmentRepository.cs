using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ApiApp.DataAccessLayer.Repositories.DepartmentRepository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly APIDbContext _context;
        public DepartmentRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _context.Departments
                .Include(x => x.Positions)
                .FirstOrDefaultAsync(x => x.DepartmentId == id);
            
            if (department == null)
            {
                return false;
            }

            var placeHolder = await _context.Departments
                .Include(x => x.Positions)
                .FirstOrDefaultAsync(x => x.DepartmentId != id);

            if (placeHolder == null)
            {
                return false;
            }

            foreach(var value in department.Positions)
            {
                placeHolder.Positions.Add(value);
            }

            _context.Departments .Remove(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DepartmentCreation> GetDepartmentByIdAsync(int id)
        {
            var department = await _context.Departments
                .Include(x => x.Positions)
                .FirstOrDefaultAsync(x => x.DepartmentId == id);

            var departmentCreation = new DepartmentCreation();
            departmentCreation.DepartmentId = department.DepartmentId;
            departmentCreation.DepartmentName = department.DepartmentName;

            foreach(var value in department.Positions)
            {
                departmentCreation.PositionIds.Add(value.PositionId);
            }

            return departmentCreation;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            var departments = await _context.Departments
                .ToListAsync();

            return departments;
        }

        public async Task<bool> PostDepartmentAsync(DepartmentCreation department)
        {
            var departmentCreated = new Department
            {
                DepartmentName = department.DepartmentName
            };

            if(department.PositionIds != null)
            {
                foreach (var value in department.PositionIds)
                {
                    var position = _context.Positions
                        .FirstOrDefault(x => x.PositionId == value);

                    if (position == null)
                    {
                        return false;
                    }

                    departmentCreated.Positions.Add(position);
                }
            }

            await _context.Departments.AddAsync(departmentCreated);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutDepartmentAsync(DepartmentCreation department)
        {
            var departmentUpdated = await _context.Departments
                .Include(x => x.Positions)
                .FirstOrDefaultAsync(x => x.DepartmentId == department.DepartmentId);

            if (departmentUpdated == null)
            {
                return false;
            }

            departmentUpdated.DepartmentName = department.DepartmentName;

            if (department.PositionIds != null)
            {
                foreach (var value in department.PositionIds)
                {
                    var position = _context.Positions
                        .FirstOrDefault(x => x.PositionId == value);

                    if (position == null)
                    {
                        return false;
                    }

                    if (!departmentUpdated.Positions.Contains(position))
                    {
                        departmentUpdated.Positions.Add(position);
                    }
                }
            }

            _context.Departments.Update(departmentUpdated);
            await _context.SaveChangesAsync();
            return true;
        }
    
        
    }
}
