using EmpSystem.Application.BusinessInterfaces;
using EmpSystem.Application.DTOs.Request;
using EmpSystem.Application.DTOs.Resopnse;
using EmpSystem.Application.Enums;
using EmpSystem.Application.Mappers;
using EmpSystem.Core.Entities;
using EmpSystem.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging;

namespace EmpSystem.Application.BusinessLogic
{
    public class VacancyProcessor : IVacancyProcessor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public VacancyProcessor(IUnitOfWork unitOfWork,ILogger<VacancyProcessor> logger) 
        { 
            _unitOfWork = unitOfWork;
            _logger=logger;
        }

        public GenericResopne<bool> ApplyForVacancy(VacancyApplicationRequest vacancyApplicationRequest)
        {
            try
            {
                _logger.Log(LogLevel.Information, "ApplyForVacancy Started");
                var vacancy = _unitOfWork.vacancyRepository.FindWhere(x => x.VacancyId == vacancyApplicationRequest.vacancyId).FirstOrDefault();
                if (vacancy != null) 
                {
                    if(IsUserAppliedForPositionToday(vacancyApplicationRequest.applicantId))
                    {
                        return ResponseHandler<bool>.GenerateResponse(false, Enums.ResponseStatus.Failed, "Can't apply for 2 positions at same day");
                    }
                    if (IsUserAppliedForSameVacancyBefore(vacancyApplicationRequest))
                    {
                        return ResponseHandler<bool>.GenerateResponse(false, Enums.ResponseStatus.Failed, "Appplied Before, Can't apply again");
                    }
                    int numOfApplicants = _unitOfWork.vacancyApplicationsRepository.GetNumOfApplicants(vacancy.VacancyId);
                    if (vacancy.IsActive)
                    {
                        if (vacancy.ExpiryDate > DateTime.Now)
                        {
                            if (numOfApplicants < vacancy.MaxNumberOfApplications)
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return ResponseHandler<bool>.GenerateResponse(false, ResponseStatus.Failed, "Error Occurred");
            }
            finally
            {
                _logger.LogInformation("ApplyForVacancy Ended");
            }
        }

        public GenericResopne<VacancyResponseDTO> CreateNewVacancy(VacancyCreateRequest vacancyCreateRequest)
        {
            try
            {
                _logger.Log(LogLevel.Information, "CreateNewVacancy Started");
                var vacancy=GenericMapper<VacancyCreateRequest,Vacancy>.Map(vacancyCreateRequest);
                vacancy.CreatedDate = DateTime.Now;
                vacancy= _unitOfWork.vacancyRepository.AddRecord(vacancy);
                return ResponseHandler<VacancyResponseDTO>.GenerateResponse(
                    resut: GenericMapper<Vacancy, VacancyResponseDTO>.Map(vacancy),
                    resonseMessage: "Added Successfully",
                    status: Enums.ResponseStatus.Success);
            }
            catch (Exception ex) 
            {
                _logger.Log(LogLevel.Error, ex.Message, ex.InnerException);
                return ResponseHandler<VacancyResponseDTO>.GenerateResponse(
                    resut: null,
                    resonseMessage: "Error Occurred",
                    status: Enums.ResponseStatus.Failed);
            }
            finally
            {
                _logger.LogInformation("CreateNewVacancy Ended");
            }
        }

        public GenericResopne<bool> DeleteVacancy(int vacancyId)
        {
            try
            {
                _logger.Log(LogLevel.Information, "DeleteVacancy Started");
                var res = _unitOfWork.vacancyRepository.DeleteRecord(entity => entity.VacancyId == vacancyId);
                if (res == 1)
                {
                    return ResponseHandler<bool>.GenerateResponse(true, ResponseStatus.Success, "Deleted Successfully");
                }
                else
                {
                    return ResponseHandler<bool>.GenerateResponse(false, ResponseStatus.Failed, "Error occurred");
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return ResponseHandler<bool>.GenerateResponse(false, ResponseStatus.Failed, "Error Occurred");
            }
            finally
            {
                _logger.LogInformation("DeleteVacancy Ended");
            }
        }

        public GenericResopne<List<VacancyResponseDTO>> GetAllVacancies()
        {
            try
            {
                _logger.Log(LogLevel.Information, "GetAllVacancies Started");
                var res = _unitOfWork.vacancyRepository.GetAll().ToList();

                return ResponseHandler<List<VacancyResponseDTO>>.GenerateResponse(
                    GenericMapper<Vacancy, VacancyResponseDTO>.Map(res), ResponseStatus.Success, "Returned Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return ResponseHandler<List<VacancyResponseDTO>>.GenerateResponse(null, ResponseStatus.Failed, "Error Occurred");
            }
            finally
            {
                _logger.LogInformation("GetAllVacancies Ended");
            }
        }

        public GenericResopne<List<VacancyResponseDTO>> GetAvailableVacancies()
        {
            try
            {
                _logger.Log(LogLevel.Information, "GetAvailableVacancies Started");
                var result=_unitOfWork.vacancyRepository.GetAvailableVacancies();
                return ResponseHandler<List<VacancyResponseDTO>>.GenerateResponse(
                    GenericMapper<Vacancy, VacancyResponseDTO>.Map(result), ResponseStatus.Success, "Success");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return ResponseHandler<List<VacancyResponseDTO>>.GenerateResponse(null, ResponseStatus.Failed, "Error Occurred");
            }
            finally
            {
                _logger.LogInformation("DeleteVacancy Ended");
            }
        }

        public GenericResopne<VacancyResponseDTO> UpdateVacancy(VacancyUpdateReuest vacancyUpdateReuest)
        {
            try
            {
                _logger.Log(LogLevel.Information, "UpdateVacancy Started");
                var entity = _unitOfWork.vacancyRepository.FindWhere(entity => entity.VacancyId == vacancyUpdateReuest.VacancyId).FirstOrDefault();
                if (entity != null)
                {
                    var createdDate = entity.CreatedDate;
                    //entity = GenericMapper<VacancyUpdateReuest, Vacancy>.Map(vacancyUpdateReuest);
                    entity.CreatedDate = createdDate;
                    entity.ExpiryDate = vacancyUpdateReuest.ExpiryDate;
                    entity.MaxNumberOfApplications = vacancyUpdateReuest.MaxNumberOfApplicantions;
                    entity.VacancyName=vacancyUpdateReuest.VacancyName;
                    entity.IsActive=vacancyUpdateReuest.IsActive;
                

                    var x = _unitOfWork.vacancyRepository.UpdateRecord(entity);
                    return ResponseHandler<VacancyResponseDTO>.GenerateResponse(
                        GenericMapper<Vacancy, VacancyResponseDTO>.Map(x),ResponseStatus.Success, "Updated Successfully");
                }
                else
                {
                    return ResponseHandler<VacancyResponseDTO>.GenerateResponse(null, ResponseStatus.Failed, "Vacancy doesn't exist");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return ResponseHandler<VacancyResponseDTO>.GenerateResponse(null, ResponseStatus.Failed, "Error Occurred");
            }
            finally
            {
                _logger.LogInformation("UpdateVacancy Ended");
            }
        }
        private bool IsUserAppliedForSameVacancyBefore(VacancyApplicationRequest vacancyApplicationRequest)
        {
            return _unitOfWork.vacancyApplicationsRepository.FindWhere(
                x=>x.VacancyId==vacancyApplicationRequest.vacancyId
                &&x.ApplicantId==vacancyApplicationRequest.applicantId).Any();
        }
        private bool IsUserAppliedForPositionToday(int applicantId)
        {
            var res= _unitOfWork.vacancyApplicationsRepository.FindWhere(x=>x.ApplicantId==applicantId 
                                                        &&x.ApplicationDate== DateTime.Today).FirstOrDefault();
            return res != null;
        }
    }
}
