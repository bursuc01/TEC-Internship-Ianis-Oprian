using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using ApiApp.DataAccessLayer.Repositories.PersonRepository;
using AutoMapper;

namespace ApiApp.BusinessLogicLayer.PersonBLL
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(
            IPersonRepository personRepository,
            IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public async Task<PersonInformation> GetPersonByIdAsync(int id)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            return person;
        }

        public async Task<IEnumerable<PersonInformation>> GetPersonListAsync()
        {
            var personList = await _personRepository.GetPersonListAsync();
            return personList;
        }

        public async Task<bool> PostPersonAsync(PersonCreation person)
        {
            var mappedPerson = _mapper.Map<Person>(person);
            return await _personRepository.PostPersonAsync(mappedPerson);
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            return await _personRepository.DeletePersonAsync(id);
        }

        public async Task<bool> PutPersonAsync(PersonCreation person)
        {
            var mappedPerson = _mapper.Map<Person>(person);
            return await _personRepository.PutPersonAsync(mappedPerson);
        }
    }
}
