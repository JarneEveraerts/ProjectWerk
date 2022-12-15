using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllPhiAPI.Controllers
{
    [Route("Visitors")]
    [ApiController]

    public class VisitorController
    {
        private readonly IVisitorService _visitorService;

        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Visitor>> GetVisitors()
        {
            {
                return await _visitorService.GetVisitors();
            }
        }
        [HttpGet("businessname/{businessname}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<List<Visitor>> GetVisitorsByBusiness([FromRoute] string businessname)
        {
            return await _visitorService.GetVisitorsByBusiness(businessname);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public Visitor GetVisitorByName([FromRoute] string name)
        {
            return _visitorService.GetVisitorByName(name);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public Visitor GetVisitorById([FromRoute] int id)
        {
            return _visitorService.GetVisitorById(id);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public Visitor GetVisitorByMail([FromRoute] string email)
        {
            return _visitorService.GetVisitorByMail(email);
        }
        #endregion GET

        #region CREATE
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Visitor CreateVisitor(CreateVisitorDTO visitor)
        {
            return _visitorService.CreateVisitor(visitor);
        }

        [HttpPut("balie")]
        public void CreateVisitorBalie(CreateVisitorBalieDTO visitor)
        {
            _visitorService.CreateVisitorBalie(visitor);
        }
        #endregion CREATE

        #region UPDATE
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public void UpdateVisitor(UpdateVisitorDTO visitor, [FromRoute] int id)
        {
            {
                _visitorService.UpdateVisitor(visitor, id);
            }
        }
        [HttpDelete("{id}")]
        public void DeleteVisitor([FromRoute] int id)
        {
            _visitorService.DeleteVisitor(id);
        }
        #endregion
    }
}
