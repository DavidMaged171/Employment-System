
using System.ComponentModel.DataAnnotations;

namespace EmpSystem.Application.DTOs.Request
{
    public class VacancyUpdateReuest
    {
        [Required]
        public int VacancyId { get; set; }
        public string VacancyName { get; set; }
        public int MaxNumberOfApplicantions { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }

    }
}
