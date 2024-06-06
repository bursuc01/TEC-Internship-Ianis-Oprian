using ApiApp.DataAccessLayer.ObjectModel;

namespace ApiApp.BusinessLogicLayer.PersonDetailsBLL
{
    public interface IPersonDetailsService
    {
        public Task<IEnumerable<PersonDetailsInformation>> GetAllPersonDetailsAsync();
        public Task<PersonDetailsInformation> GetPersonDetailsByIdAsync(int id);
        public Task<bool> DeletePersonDetailsAsync(int id);
        public Task<bool> PostPersonDetailsAsync(PersonDetailCreation personDetailsInformation);
        public Task<bool> PutPersonDetailsAsync(PersonDetailCreation personDetailsInformation);
    }
}
