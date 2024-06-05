using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.BusinessLogicLayer.SalaryBLL
{
    public interface ISalaryService
    {
        public Task<IEnumerable<SalaryInformation>> GetSalariesAsync();
        public Task<bool> PostSalaryAsync(SalaryInformation salary);
        public Task<bool> DeleteSalaryAsync(int id);

    }
}
