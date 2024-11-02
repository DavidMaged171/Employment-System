using EmpSystem.Application.BusinessInterfaces;
using EmpSystem.Application.DTOs.Request;
using EmpSystem.Application.DTOs.Resopnse;
using EmpSystem.Application.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystemAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacancyController : Controller
    {
        private readonly IVacancyProcessor _vacancyProcessor;
        public VacancyController(IVacancyProcessor vacancyProcessor)
        {
            _vacancyProcessor = vacancyProcessor;
        }
        [HttpPost]
        [Route("CreateNewVacancy")]
        public GenericResopne<VacancyResponseDTO> CreateNewVacancy(VacancyCreateRequest vacancyCreateRequest)
        {
            return GenericExceptionHandler.Handle(() =>
            {
                return _vacancyProcessor.CreateNewVacancy(vacancyCreateRequest);
            });
        }
        [HttpGet]
        [Route("GetAllVacancies")]
        public GenericResopne<List<VacancyResponseDTO>> GetAllVacancies()
        {
            return GenericExceptionHandler.Handle(() =>
            {
                return _vacancyProcessor.GetAllVacancies();
            });
        }
        [HttpPut]
        [Route("UpdateVacancy")]
        public GenericResopne<VacancyResponseDTO> UpdateVacancy(VacancyUpdateReuest vacancyUpdateReuest)
        {
            return GenericExceptionHandler.Handle(() => { 
                return _vacancyProcessor.UpdateVacancy(vacancyUpdateReuest);
            });
        }
        [HttpDelete]
        [Route("DeleteVacancy")]
        public GenericResopne<bool> DeleteVacancy(int vacancyId)
        {
            return GenericExceptionHandler.Handle(() => 
            {
                return _vacancyProcessor.DeleteVacancy(vacancyId);
            });
        }
        [HttpPost]
        [Route("ApplyForVacancy")]
        public GenericResopne<bool> ApplyForVacancy(VacancyApplicationRequest vacancyApplicationRequest)
        {
            return GenericExceptionHandler.Handle(() => 
            {
                return _vacancyProcessor.ApplyForVacancy(vacancyApplicationRequest);
            });
        }
    }
}
