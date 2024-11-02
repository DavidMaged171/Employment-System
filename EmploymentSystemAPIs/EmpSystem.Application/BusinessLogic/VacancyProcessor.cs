using EmpSystem.Application.BusinessInterfaces;
using EmpSystem.Application.DTOs.Request;
using EmpSystem.Application.DTOs.Resopnse;
using EmpSystem.Application.Mappers;
using EmpSystem.Core.Entities;
using EmpSystem.Infrastructure.Interfaces;

namespace EmpSystem.Application.BusinessLogic
{
    public class VacancyProcessor : IVacancyProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        public VacancyProcessor(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }
        public GenericResopne<VacancyResponseDTO> CreateNewVacancy(VacancyCreateRequest vacancyCreateRequest)
        {
            var vacancy = new Vacancy();
            vacancy=GenericMapper<VacancyCreateRequest,Vacancy>.Map(vacancyCreateRequest,vacancy);
            vacancy.CreatedDate = DateTime.Now;
            vacancy= _unitOfWork.vacancyRepository.AddRecord(vacancy);
            return new GenericResopne<VacancyResponseDTO>()
            {
                result = GenericMapper<Vacancy, VacancyResponseDTO>.Map(vacancy, new VacancyResponseDTO()),
                ResopnseStatus=Enums.ResponseStatus.Success,
                ResponseMessage="Added Successfully"
            };
        }
    }
}
