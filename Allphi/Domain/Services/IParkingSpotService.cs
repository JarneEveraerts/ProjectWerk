using Domain.Models;
using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IParkingSpotService
    {
        bool EnterParking(EnterParkingDTO enterParkingDTO);
        bool ExitParking(string input);
        ParkingSpot GetAvailableParkingSpotVisitor();
        Task<List<ParkingSpot>> GetParkingSpots();
        bool ParkingSpotExists(string visitorPlate);
        void SubmitVisitorParking(string licensePlate);
    }
}