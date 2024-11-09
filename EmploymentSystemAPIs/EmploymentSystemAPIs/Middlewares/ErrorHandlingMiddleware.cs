using EmpSystem.Application.DTOs.Resopnse;
using EmpSystem.Application.Enums;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace EmploymentSystemAPIs.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string resultMessage;
                context.Response.ContentType = "application/json";
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                resultMessage= ex.Message;
            
                await context.Response.WriteAsync(JsonConvert.SerializeObject(
                        new GenericResopne<object>()
                        {
                            result = new object(),
                            ResopnseStatus = ResponseStatus.Failed,
                            ResponseMessage = resultMessage
                        }
                    ));
            }

        }
    }
}
