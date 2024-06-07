using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.DataAccessLayer.Repositories.SalaryRepository
{
    public interface ISalaryRepository
    {
        public Task<IEnumerable<Salary>> GetSalariesAsync();
        public Task<bool> PostSalaryAsync(Salary salary);
        public Task<bool> DeleteSalaryAsync(int id); 
        public Task<bool> PutSalaryAsync(Salary salary);
        public Task<Salary> GetSalaryByIdAsync(int id);
    }
}
