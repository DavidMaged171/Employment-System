using EmpSystem.Application.DTOs.Request;
using EmpSystem.Application.DTOs.Resopnse;

namespace EmpSystem.Application.BusinessInterfaces
{
    public interface IVacancyProcessor
    {
        public GenericResopne<VacancyResponseDTO> CreateNewVacancy(VacancyCreateRequest vacancyCreateRequest);
        public GenericResopne<VacancyResponseDTO> UpdateVacancy(VacancyUpdateReuest vacancyUpdateReuest);
        public GenericResopne<List<VacancyResponseDTO>> GetAllVacancies();
        public GenericResopne<List<VacancyResponseDTO>> GetAvailableVacancies();
        public GenericResopne<bool> DeleteVacancy(VacancyDeleteRequest vacancyDeleteRequest);
        public GenericResopne<bool> ApplyForVacancy(VacancyApplicationRequest vacancyApplicationRequest);
    }
}
