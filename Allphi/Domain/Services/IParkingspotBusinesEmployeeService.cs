using Domain.Models.DTOs;

namespace Domain.Services
{
    public interface IParkingspotBusinesEmployeeService
    {
        bool EnterParking(EnterParkingDTO enterParking);
    }
}