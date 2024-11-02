
namespace EmpSystem.Application.DTOs.Resopnse
{
    public class VacancyResponseDTO
    {
        public int VacancyId { get; set; }
        public string VacancyName { get; set; }
        public int MaxNumberOfApplicantions { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
