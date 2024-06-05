using ApiApp.DataAccessLayer.Model;
using ApiApp.DataAccessLayer.ObjectModel;
using ApiApp.DataAccessLayer.Repositories.SalaryRepository;
using AutoMapper;

namespace ApiApp.BusinessLogicLayer.SalaryBLL
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IMapper _mapper;

        public SalaryService(
            ISalaryRepository salaryRepository,
            IMapper mapper)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteSalaryAsync(int id)
        {
            return await _salaryRepository.DeleteSalaryAsync(id);
        }

        public async Task<IEnumerable<SalaryInformation>> GetSalariesAsync()
        {
            var salaries = await _salaryRepository.GetSalariesAsync();
            return _mapper.Map<IEnumerable<SalaryInformation>>(salaries);
        }

        public async Task<bool> PostSalaryAsync(SalaryInformation salary)
        {
            var salaryMapped = _mapper.Map<Salary>(salary);
            return await _salaryRepository.PostSalaryAsync(salaryMapped);
        }
    }
}
