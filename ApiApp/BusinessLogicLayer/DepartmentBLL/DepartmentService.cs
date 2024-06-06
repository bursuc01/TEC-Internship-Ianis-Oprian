using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using ApiApp.DataAccessLayer.Repositories.DepartmentRepository;

namespace ApiApp.BusinessLogicLayer.DepartmentBLL
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            return await _departmentRepository.DeleteDepartmentAsync(id);
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetDepartmentByIdAsync(id);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _departmentRepository.GetDepartmentsAsync();
        }

        public async Task<bool> PostDepartmentAsync(DepartmentCreation department)
        {
            return await _departmentRepository.PostDepartmentAsync(department);
        }

        public async Task<bool> PutDepartmentAsync(DepartmentCreation department)
        {
            return await _departmentRepository.PutDepartmentAsync(department);
        }
    }
}
