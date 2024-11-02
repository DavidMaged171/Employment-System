using EmpSystem.Application.DTOs.Request;
using EmpSystem.Application.DTOs.Resopnse;

namespace EmpSystem.Application.BusinessInterfaces
{
    public interface IVacancyProcessor
    {
        public GenericResopne<VacancyResponseDTO> CreateNewVacancy(VacancyCreateRequest vacancyCreateRequest);
    }
}
