
using EmpSystem.Application.Enums;

namespace EmpSystem.Application.DTOs.Resopnse
{
    public static class ResponseHandler <T>
    {
        public static GenericResopne<T>GenerateResponse(T? resut,ResponseStatus status,string resonseMessage)
        {
            return new GenericResopne<T>() 
            {
                result = resut,
                ResopnseStatus = status,
                ResponseMessage=resonseMessage
            };
        }
    }
}
