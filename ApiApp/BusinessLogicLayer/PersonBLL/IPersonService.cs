using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.BusinessLogicLayer.PersonBLL
{
    public interface IPersonService
    {
        public Task<IEnumerable<PersonInformation>> GetPersonListAsync();
        public Task<PersonInformation> GetPersonByIdAsync(int id);
        public Task<bool> PostPersonAsync(PersonCreation person);
        public Task<bool> PutPersonAsync(PersonCreation person);
        public Task<bool> DeletePersonAsync(int id);

    }
}
