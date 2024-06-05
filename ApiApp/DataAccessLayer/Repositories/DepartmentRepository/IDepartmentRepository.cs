using ApiApp.DataAccessLayer.Model;

namespace ApiApp.DataAccessLayer.Repositories.DepartmentRepository
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetDepartmentsAsync();

    }
}
