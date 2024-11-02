
using EmpSystem.Application.Enums;

namespace EmpSystem.Application.DTOs.Resopnse
{
    public class GenericResopne <T>
    {
        public ResponseStatus ResopnseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public T? result { get; set; }
    }
}
