using MECS.Client.API.Application.Commands;
using MECS.Core.Data.Mediator;
using MECS.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MECS.Client.API.Controllers
{
    [Route("api/clients")]
    public class ClientsController : MainController
    {

        private readonly IMediatorHandler _mediatorHandler;

        public ClientsController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var result = await _mediatorHandler.SendCommand(
        //        new RegisterClientCommand(Guid.NewGuid(), "Renato", "renato@renato.com", "30314299076"));
        //    return CustomResponse(result);
        //}
    }
}
