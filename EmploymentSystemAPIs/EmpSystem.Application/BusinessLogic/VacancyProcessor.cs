using EmpSystem.Application.BusinessInterfaces;
using EmpSystem.Application.DTOs.Request;
using EmpSystem.Application.DTOs.Resopnse;
using EmpSystem.Application.Mappers;
using EmpSystem.Core.Entities;
using EmpSystem.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace EmpSystem.Application.BusinessLogic
{
    public class VacancyProcessor : IVacancyProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        public VacancyProcessor(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }

        public GenericResopne<bool> ApplyForVacancy(VacancyApplicationRequest vacancyApplicationRequest)
        {
            var vacancy = _unitOfWork.vacancyRepository.FindWhere(x => x.VacancyId == vacancyApplicationRequest.vacancyId).FirstOrDefault();
            if (vacancy != null) 
            {
                int numOfApplicants = _unitOfWork.vacancyApplicationsRepository.GetNumOfApplicants(vacancy.VacancyId);
                if (vacancy.IsActive)
                {
                    if (vacancy.ExpiryDate > DateTime.Now)
                    {
                        if (numOfApplicants < vacancy.MaxNumberOfApplicantions)
                        {
                            _unitOfWork.vacancyApplicationsRepository.AddRecord(
                               GenericMapper<VacancyApplicationRequest, VacancyApplications>.Map(vacancyApplicationRequest));
                            return ResponseHandler<bool>.GenerateResponse(true, Enums.ResponseStatus.Success, "Applied Successfully");
                        }
                        else
                        {
                            return ResponseHandler<bool>.GenerateResponse(false, Enums.ResponseStatus.Failed, "Vacancy exeeded Max number of aplicants");
                        }
                    }
                    else
                    {
                        return ResponseHandler<bool>.GenerateResponse(false, Enums.ResponseStatus.Failed, "Vacancy Expired");
                    }
                }
                else
                {
                    return ResponseHandler<bool>.GenerateResponse(false, Enums.ResponseStatus.Failed, "Vacancy Deactivated");
                }
            }
            else
            {
                return ResponseHandler<bool>.GenerateResponse(false, Enums.ResponseStatus.Failed, "Vacancy doesn't exist");
            }
        }

        public GenericResopne<VacancyResponseDTO> CreateNewVacancy(VacancyCreateRequest vacancyCreateRequest)
        {
            var vacancy=GenericMapper<VacancyCreateRequest,Vacancy>.Map(vacancyCreateRequest);
            vacancy.CreatedDate = DateTime.Now;
            vacancy= _unitOfWork.vacancyRepository.AddRecord(vacancy);
            return new GenericResopne<VacancyResponseDTO>()
            {
                result = GenericMapper<Vacancy, VacancyResponseDTO>.Map(vacancy),
                ResopnseStatus=Enums.ResponseStatus.Success,
                ResponseMessage="Added Successfully"
            };
        }

        public GenericResopne<bool> DeleteVacancy(int vacancyId)
        {
            var res = _unitOfWork.vacancyRepository.DeleteRecord(entity => entity.VacancyId == vacancyId);
            if (res == 1)
            {
                return new GenericResopne<bool>()
                {
                    result = true,
                    ResopnseStatus = Enums.ResponseStatus.Success,
                    ResponseMessage = "Deleted Successfully"
                };
            }
            else
            {
                return new GenericResopne<bool>()
                {
                    result = false,
                    ResponseMessage= "Error occurred",
                    ResopnseStatus=Enums.ResponseStatus.Failed
                };
            }
        }

        public GenericResopne<List<VacancyResponseDTO>> GetAllVacancies()
        {
            var res = _unitOfWork.vacancyRepository.GetAll().ToList();
            return new GenericResopne<List<VacancyResponseDTO>>()
            {
                result = GenericMapper<Vacancy,VacancyResponseDTO>.Map(res,new List<VacancyResponseDTO> ()),
                ResopnseStatus=Enums.ResponseStatus.Success,
                ResponseMessage="Returned Successfully"
            };
        }

        public GenericResopne<List<VacancyResponseDTO>> GetAvailableVacancies()
        {
            throw new NotImplementedException();
        }

        public GenericResopne<VacancyResponseDTO> UpdateVacancy(VacancyUpdateReuest vacancyUpdateReuest)
        {
            var entity = _unitOfWork.vacancyRepository.FindWhere(entity => entity.VacancyId == vacancyUpdateReuest.VacancyId).FirstOrDefault();
            if (entity != null)
            {
                var createdDate = entity.CreatedDate;
                //entity = GenericMapper<VacancyUpdateReuest, Vacancy>.Map(vacancyUpdateReuest);
                entity.CreatedDate = createdDate;
                entity.ExpiryDate = vacancyUpdateReuest.ExpiryDate;
                entity.MaxNumberOfApplicantions = vacancyUpdateReuest.MaxNumberOfApplicantions;
                entity.VacancyName=vacancyUpdateReuest.VacancyName;
                entity.IsActive=vacancyUpdateReuest.IsActive;
                

                var x = _unitOfWork.vacancyRepository.UpdateRecord(entity);
                return new GenericResopne<VacancyResponseDTO>()
                {
                    result = GenericMapper<Vacancy, VacancyResponseDTO>.Map(x),
                    ResopnseStatus = Enums.ResponseStatus.Success,
                    ResponseMessage = "Updated Successfully"
                };
            }
            else
            {
                return new GenericResopne<VacancyResponseDTO>() 
                {
                    result=null,
                    ResopnseStatus=Enums.ResponseStatus.Failed,
                    ResponseMessage="Vacancy doesn't exist"
                };
            }
        }
    }
}
