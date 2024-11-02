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
    }
}
