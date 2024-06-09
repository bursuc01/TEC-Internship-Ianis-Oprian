using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.DataAccessLayer.Repositories.PersonRepository
{
    public interface IPersonRepository
    {
        public Task<IEnumerable<PersonInformation>> GetPersonListAsync();
        public Task<PersonCreation> GetPersonByIdAsync(int id);
        public Task<bool> PostPersonAsync(Person person);
        public Task<bool> DeletePersonAsync(int id);
        public Task<bool> PutPersonAsync(Person person);
    }
}
