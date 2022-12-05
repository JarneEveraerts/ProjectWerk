using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllPhiAPI.Controllers
{
    [Route("Businesses")]
    [ApiController]
    public class BusinessController
    {
        private readonly IBusinessService _businessService;

        public BusinessController(IBusinessService businessService)
        {
            _businessService = businessService;
        }
        #region GET

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]


        public async Task<List<Business>> GetBusinessesAsync()
        {
            return await _businessService.GetBusinesses();

        }
        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public Business GetBusinessByName([FromRoute] string name)
        {
            return _businessService.GetBusinessByName(name);
        }

        [HttpGet("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public Business GetBusinessById([FromRoute] int id)
        {
            return _businessService.GetBusinessById(id);
        }
        [HttpGet("btw/{btw}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public Business GetBusinessByBTW([FromRoute] string btw)
        {
            return _businessService.GetBusinessByBtw(btw);
        }
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public Business GetBusinessByEmail([FromRoute] string email)
        {
            return _businessService.GetBusinessByEmail(email);
        }
        #endregion GET

        #region CREATE
        [HttpPut]
        public void CreateBusiness(CreateAndUpdateBusinessDTO business)
        {
            _businessService.CreateBusiness(business);
        }
        #endregion CREATE

        #region UPDATE
        [HttpPatch("{id}")]
        public void UpdateBusiness(CreateAndUpdateBusinessDTO business, [FromRoute] int id)
        {
            _businessService.UpdateBusiness(business, id);
        }

        [HttpDelete("{id}")]
        public void DeleteBusiness([FromRoute] int id)
        {
            _businessService.DeleteBusiness(id);
        }
        #endregion UPDATE
    }
}
