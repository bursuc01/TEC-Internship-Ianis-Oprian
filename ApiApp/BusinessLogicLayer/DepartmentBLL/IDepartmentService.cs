using ApiApp.DataAccessLayer.Model;

namespace ApiApp.BusinessLogicLayer.DepartmentBLL
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<Department>> GetDepartmentsAsync();
    }
}
