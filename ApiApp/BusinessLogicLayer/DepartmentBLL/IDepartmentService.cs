using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.BusinessLogicLayer.DepartmentBLL
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<Department>> GetDepartmentsAsync();
        public Task<DepartmentCreation> GetDepartmentByIdAsync(int id);
        public Task<bool> PostDepartmentAsync(DepartmentCreation department);
        public Task<bool> PutDepartmentAsync(DepartmentCreation department);
        public Task<bool> DeleteDepartmentAsync(int id);

    }
}
