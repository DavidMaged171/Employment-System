
using System.ComponentModel.DataAnnotations;

namespace EmpSystem.Application.DTOs.Request
{
    public class VacancyCreateRequest
    {
        [Required]
        public string VacancyName { get; set; }
        [Required]
        public int MaxNumberOfApplicantions { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
