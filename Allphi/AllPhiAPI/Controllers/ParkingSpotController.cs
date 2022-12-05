using Domain;
using Domain.Models;
using Domain.Models.DTOs;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace AllPhiAPI.Controllers
{
    [Route("ParkingSpots")]
    [ApiController]
    public class ParkingSpotController
    {
        private readonly IParkingSpotService _ParkingSpotService;

        public ParkingSpotController(IParkingSpotService parkingSpotService)
        {
            _ParkingSpotService = parkingSpotService;
        }

        #region GET

        [HttpGet]
        public async Task<List<ParkingSpot>> GetParkingSpots()
        {
            return await _ParkingSpotService.GetParkingSpots();
        }

        #endregion

        [HttpPost("enter")]
        public bool EnterParking(EnterParkingDTO enterParkingDTO)
        {
            return _ParkingSpotService.EnterParking(enterParkingDTO);
        }

        [HttpPost("exit/{licenseplate}")]
        public bool ExitParking([FromRoute] string licenseplate)
        {
            return _ParkingSpotService.ExitParking(licenseplate);
        }


        [HttpGet("availablespot")]
        public ParkingSpot GetAvailableParkingSpotVisitor()
        {
            return _ParkingSpotService.GetAvailableParkingSpotVisitor();
        }

        [HttpPost("visitor/{licensePlate}")]
        public void SubmitVisitorParking([FromRoute] string licensePlate)
        {
            _ParkingSpotService.SubmitVisitorParking(licensePlate);
        }

        [HttpGet("{visitorPlate}/exists")]
        public bool ParkingSpotExists([FromRoute] string visitorPlate)
        {
            return _ParkingSpotService.ParkingSpotExists(visitorPlate);
        }
    }

}
