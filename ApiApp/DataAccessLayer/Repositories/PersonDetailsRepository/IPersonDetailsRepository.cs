using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.DataAccessLayer.Repositories.PersonDetailsRepository
{
    public interface IPersonDetailsRepository
    {
        public Task<IEnumerable<PersonDetail>> GetAllPersonDetailsAsync();
        public Task<PersonDetail> GetPersonDetailsByIdAsync(int id);
        public Task<bool> DeletePersonDetailsAsync(int id);
        public Task<bool> PostPersonDetailsAsync(PersonDetailCreation personDetails);
        public Task<bool> PutPersonDetailsAsync(PersonDetailCreation personDetails);

    }
}
