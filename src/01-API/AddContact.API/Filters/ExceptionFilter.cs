using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PosTech.Fase3.AddContact.Domain.Exceptions;
using PosTech.Fase3.AddContact.Domain.Responses;

namespace PosTech.Fase3.AddContact.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = 500;
            if (context.Exception is DomainException
                    || context.Exception.InnerException is DomainException
                    )
            {
                status = 400;
            }
            
            var mensagem = status == 400 ? (
                            context.Exception.InnerException == null ? context.Exception.Message
                            : context.Exception.InnerException.Message)
                                    : "Ocorreu uma falha inesperada no servidor";
            var response = context.HttpContext.Response;
            

            response.StatusCode = status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(new DefaultOutput<Exception>(false, mensagem));
        }
    }
}