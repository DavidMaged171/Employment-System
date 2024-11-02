
using EmpSystem.Application.Enums;

namespace EmpSystem.Application.DTOs.Resopnse
{
    public class GenericResopne <T> where T : class
    {
        public ResponseStatus ResopnseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public T? result { get; set; }
    }
}
