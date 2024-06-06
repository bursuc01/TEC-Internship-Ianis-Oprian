using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.DataAccessLayer.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetDepartmentsAsync();
        public Task<Department> GetDepartmentByIdAsync(int id);
        public Task<bool> PostDepartmentAsync(DepartmentCreation department);
        public Task<bool> PutDepartmentAsync(DepartmentCreation department);

        public Task<bool> DeleteDepartmentAsync(int id);
    }
}
