using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public class DomainController
    {
        #region Decleration

        private IBusinessRepository _businessRepo;
        private IContractRepository _contractRepo;
        private IEmployeeRepository _employeeRepo;
        private IParkingSpotRepository _parkingRepo;
        private IVisitorRepository _visitorRepo;
        private IVisitRepository _visitRepo;

        #endregion Decleration

        #region CTOR

        public DomainController(IBusinessRepository businessRepo, IContractRepository contractRepo, IEmployeeRepository employeeRepo, IParkingSpotRepository parkingRepo, IVisitorRepository visitorRepo, IVisitRepository visitRepo)
        {
            this._businessRepo = businessRepo;
            this._contractRepo = contractRepo;
            this._employeeRepo = employeeRepo;
            this._parkingRepo = parkingRepo;
            this._visitorRepo = visitorRepo;
            this._visitRepo = visitRepo;
        }

        #endregion CTOR

        #region Validation

        public bool IsEmailValid(string emailAddress)
        {
            Regex regex = new Regex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$");
            Match match = regex.Match(emailAddress);
            return match.Success;
        }

        public bool IsBtwValid(string BtwNumber)
        {
            Regex regexBE = new Regex(@"(BE)?0[0-9]{9}");
            Match match = regexBE.Match(BtwNumber);
            return match.Success;
        }

        #endregion Validation

        #region LicensePlate

        public Employee? CheckEmployeePlate(string plate)
        {
            return _employeeRepo.GetEmployeeByPlate(plate);
        }

        #endregion LicensePlate

        #region Parking

        //public bool CheckAvailableParkingSpots()
        //{
        //    return (_parkingRepo.GetParkingSpotsByPlate(null) != null);
        //}

        #endregion Parking
    }
}