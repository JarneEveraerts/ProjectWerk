using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AllPhiAPI.Controllers
{
    [Route("ParkingSpots")]
    [ApiController]
    public class ParkingSpotController : Controller
    {
        //webapi using iparkingspotrepository
        private readonly IParkingSpotRepository _parkingSpotRepository;

        public ParkingSpotController(IParkingSpotRepository parkingSpotRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
        }

        // all get routes
        [HttpGet]
        public IActionResult GetParkingSpots()
        {
            var parkingSpots = _parkingSpotRepository.GetParkingSpots();
            return Ok(parkingSpots);
        }

        //get parkingspots by reserved
        [HttpGet("{reservedspots}", Name = "GetParkingSpotsByReserved")]
        public IActionResult GetParkingSpotsByReserved(Business reserved)
        {
            var parkingSpots = _parkingSpotRepository.GetParkingSpotsByReserved(reserved);
            return Ok(parkingSpots);
        }

        //get parkingspot by reserved
        [HttpGet("{availablespotreserved}", Name = "GetAvailableParkingSpotReserved")]
        public IActionResult GetAvailableParkingSpotReserved(Business reserved)
        {
            var parkingSpot = _parkingSpotRepository.GetAvailableParkingSpotReserved(reserved);
            return Ok(parkingSpot);
        }

        //get parking spot by plate
        [HttpGet("{plate}", Name = "GetParkingSpotByPlate")]
        public IActionResult GetParkingSpotByPlate(string plate)
        {
            var parkingSpot = _parkingSpotRepository.GetParkingSpotByPlate(plate);
            return Ok(parkingSpot);
        }

        //get parking spot by employee
        [HttpGet("{employee}", Name = "GetParkingSpotByEmployee")]
        public IActionResult GetParkingSpotByEmployee(Employee employee)
        {
            var parkingSpot = _parkingSpotRepository.GetParkingSpotByEmployee(employee);
            return Ok(parkingSpot);
        }

        //get parking spot by visitor
        [HttpGet("{visitor}", Name = "GetParkingSpotByVisitor")]
        public IActionResult GetParkingSpotByVisitor(Visitor visitor)
        {
            var parkingSpot = _parkingSpotRepository.GetParkingSpotByVisitor(visitor);
            return Ok(parkingSpot);
        }

        //get available parking spot
        [HttpGet("{availablespotUnreserved}", Name = "GetAvailableParkingSpotUnreserved")]
        public IActionResult GetAvailableParkingSpotUnreserved()
        {
            var parkingSpot = _parkingSpotRepository.GetAvailableParkingSpotUnreserved();
            return Ok(parkingSpot);
        }

        //create
        [HttpPost]
        public IActionResult CreateParkingSpot(ParkingSpot parkingSpot)
        {
            _parkingSpotRepository.CreateParkingSpot(parkingSpot);
            return CreatedAtRoute("GetParkingSpotById", new { id = parkingSpot.Id }, parkingSpot);
        }

        //update
        [HttpPut("{id}")]
        public IActionResult UpdateParkingSpot(ParkingSpot parkingSpot)
        {
            _parkingSpotRepository.UpdateParkingSpot(parkingSpot);
            return NoContent();
        }

        //count by plate
        [HttpGet("{countplate}", Name = "CountParkingSpotByPlate")]
        public IActionResult GetParkingSpotCount(string plate)
        {
            var parkingSpotCount = _parkingSpotRepository.CountParkingSpotByPlate(plate);
            return Ok(parkingSpotCount);
        }

        //bool exist
        [HttpGet("{spotexist}", Name = "ParkingSpotExists")]
        public IActionResult ParkingSpotExists(string plate)
        {
            var parkingSpotExists = _parkingSpotRepository.ParkingSpotExist(plate);
            return Ok(parkingSpotExists);
        }
    }
}
