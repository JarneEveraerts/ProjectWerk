using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;

namespace AllPhiAPI.Controllers
{
    [Route("ParkingSpots")]
    [ApiController]
    public class ParkingSpotController : Controller
    {
        //webapi using iparkingspotrepository
        private readonly IParkingSpotRepository _parkingSpotRepository;

        private readonly IBusinessRepository _businessRepository;

        public ParkingSpotController(IParkingSpotRepository parkingSpotRepository, IBusinessRepository businessRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
            _businessRepository = businessRepository;
        }

        // all get routes
        [HttpGet]
        public IActionResult GetParkingSpots()
        {
            var parkingSpots = _parkingSpotRepository.GetParkingSpots();
            if (parkingSpots.Count == 0) return NoContent();
            return Ok(parkingSpots.Select(p => new ParkingSpotDto(p)));
        }

        //get parkingspots by reserved
        [HttpGet("reserved/{reserved}", Name = "GetParkingSpotsByReserved")]
        public IActionResult GetParkingSpotsByReserved(string reserved)
        {
            var business = _businessRepository.GetBusinessByName(reserved);
            var parkingSpots = _parkingSpotRepository.GetParkingSpotsByReserved(business);
            if (parkingSpots.Count == 0) return NoContent();
            return Ok(parkingSpots.Select(p => new ParkingSpotDto(p)));
        }

        //get parking spot by plate
        [HttpGet("plate/{plate}", Name = "GetParkingSpotByPlate")]
        public IActionResult GetParkingSpotByPlate(string plate)
        {
            var parkingSpot = _parkingSpotRepository.GetParkingSpotByPlate(plate);
            if (parkingSpot == null) return NoContent();
            return Ok(new ParkingSpotDto(parkingSpot));
        }

        //get parking spot by employee
        [HttpGet("employee/{employee}", Name = "GetParkingSpotByEmployee")]
        public IActionResult GetParkingSpotByEmployee(Employee employee)
        {
            var parkingSpot = _parkingSpotRepository.GetParkingSpotByEmployee(employee);
            if (parkingSpot == null) return NoContent();
            return Ok(new ParkingSpotDto(parkingSpot));
        }

        //get parking spot by visitor
        [HttpGet("visitor/{visitor}", Name = "GetParkingSpotByVisitor")]
        public IActionResult GetParkingSpotByVisitor(Visitor visitor)
        {
            var parkingSpot = _parkingSpotRepository.GetParkingSpotByVisitor(visitor);
            if (parkingSpot == null) return NoContent();
            return Ok(new ParkingSpotDto(parkingSpot));
        }

        //get available parking spot
        [HttpGet("availableUnreserved/{availablespotUnreserved}", Name = "GetAvailableParkingSpotUnreserved")]
        public IActionResult GetAvailableParkingSpotUnreserved()
        {
            var parkingSpot = _parkingSpotRepository.GetAvailableParkingSpotUnreserved();
            if (parkingSpot == null) return NoContent();
            return Ok(new ParkingSpotDto(parkingSpot));
        }

        //get available parking spot reserved
        [HttpGet("availableReserved/{availablespotreserved}", Name = "GetAvailableParkingSpotReserved")]
        public IActionResult GetAvailableParkingSpotReserved(Business reserved)
        {
            var parkingSpot = _parkingSpotRepository.GetAvailableParkingSpotReserved(reserved);
            if (parkingSpot == null) return NoContent();
            return Ok(new ParkingSpotDto(parkingSpot));
        }

        //count by plate
        [HttpGet("countPlate/{countplate}", Name = "CountParkingSpotByPlate")]
        public IActionResult GetParkingSpotCount(string plate)
        {
            var parkingSpotCount = _parkingSpotRepository.CountParkingSpotByPlate(plate);
            if (parkingSpotCount == 0) return NoContent();
            return Ok(parkingSpotCount);
        }

        //bool exist
        [HttpGet("spotExist/{spotexist}", Name = "ParkingSpotExists")]
        public IActionResult ParkingSpotExists(string plate)
        {
            var parkingSpotExists = _parkingSpotRepository.ParkingSpotExist(plate);
            if (parkingSpotExists == false) return NoContent();
            return Ok(parkingSpotExists);
        }

        //create
        [HttpPost]
        public IActionResult CreateParkingSpot(ParkingSpot parkingSpot)
        {
            _parkingSpotRepository.CreateParkingSpot(parkingSpot);
            return Ok();
        }

        //update
        [HttpPut("{id}")]
        public IActionResult UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            _parkingSpotRepository.UpdateParkingSpot(parkingSpot);
            return NoContent();
        }
    }
}