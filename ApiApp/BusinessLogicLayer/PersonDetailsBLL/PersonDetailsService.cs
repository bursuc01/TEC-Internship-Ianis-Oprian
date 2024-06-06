using ApiApp.DataAccessLayer.ObjectModel;
using ApiApp.DataAccessLayer.Repositories.PersonDetailsRepository;
using AutoMapper;

namespace ApiApp.BusinessLogicLayer.PersonDetailsBLL
{
    public class PersonDetailsService : IPersonDetailsService
    {
        private readonly IPersonDetailsRepository _personDetailsRepository;
        private readonly IMapper _mapper;
        public PersonDetailsService(
            IPersonDetailsRepository personDetailsRepository,
            IMapper mapper)
        {
            _personDetailsRepository = personDetailsRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeletePersonDetailsAsync(int id)
        {
            return await _personDetailsRepository.DeletePersonDetailsAsync(id);
        }

        public async Task<IEnumerable<PersonDetailsInformation>> GetAllPersonDetailsAsync()
        {
            var detailsList = await _personDetailsRepository.GetAllPersonDetailsAsync();

            return _mapper.Map<IEnumerable<PersonDetailsInformation>>(detailsList);
        }

        public async Task<PersonDetailsInformation> GetPersonDetailsByIdAsync(int id)
        {
            var details = await _personDetailsRepository.GetPersonDetailsByIdAsync(id);

            return _mapper.Map<PersonDetailsInformation>(details);
        }

        public async Task<bool> PostPersonDetailsAsync(PersonDetailCreation personDetailsInformation)
        {
            return await _personDetailsRepository.PostPersonDetailsAsync(personDetailsInformation);
        }

        public async Task<bool> PutPersonDetailsAsync(PersonDetailCreation personDetailsInformation)
        {
            return await _personDetailsRepository.PutPersonDetailsAsync(personDetailsInformation);
        }
    }
}
