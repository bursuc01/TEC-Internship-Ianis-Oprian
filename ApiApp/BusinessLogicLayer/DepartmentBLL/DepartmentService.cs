using ApiApp.DataAccessLayer.Model;
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

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _departmentRepository.GetDepartmentsAsync();
        }
    }
}
