using FluentValidation.Results;
using MECS.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace MECS.WebAPI.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(result);
            }
            var operationTitle = "Messages";

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {operationTitle,Erros.ToArray() },
            }));
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }
            return CustomResponse();
        }
        protected ActionResult CustomResponse(BaseEntity baseEntity)
        {

            var erros = baseEntity.ErrorMessages;

            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro);
            }
            var dict = baseEntity.ValidationResult.Errors.ToArray();
            return CustomResponse();
        }
        protected ActionResult CustomResponse(ValidationResult baseEntity)
        {

            var erros = baseEntity.Errors;

            foreach (var erro in erros)
            {
                AdicionarErroProcessamento(erro.ErrorMessage);
            }
            return CustomResponse();
        }
        protected ActionResult CustomResponse(ResponseResult response)
        {
            ResponsePossuiErros(response);
            return CustomResponse();
        }
        protected bool ResponsePossuiErros(ResponseResult response)
        {
            if (response == null || !response.Errors.Messages.Any())
                return false;

            foreach (var message in response.Errors.Messages)
                AdicionarErroProcessamento(message);

            return true;
        }
        protected bool OperacaoValida()
        {
            return !Erros.Any();
        }

        protected void AdicionarErroProcessamento(string erro)
        {
            Erros.Add(erro);
        }

        protected void LimparErrosProcessamento()
        {
            Erros.Clear();
        }
    }
}
