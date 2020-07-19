using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Activities;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult<List<Activity>>> List(CancellationToken cancelletionToken)
        {
            return await Mediator.Send(new List.Query(), cancelletionToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> Details(Guid id)
        {
            return await Mediator.Send(new Detail.Query { Id = id });
        } 

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(Guid id, Edit.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            var command = new Delete.Command() { Id = id };

            return await Mediator.Send(command);
        }
    }
}
