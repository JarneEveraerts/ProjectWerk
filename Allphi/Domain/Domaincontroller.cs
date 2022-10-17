using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain
{
    public class Domaincontroller
    {
        private IBusinessRepository _businessRepo;
        private IContractRepository _contractRepo;
        private IEmployeeRepository _employeeRepo;
        private IParkingRepository _parkingRepo;
        private IVisitorRepository _visitorRepo;
        private IVisitRepository _visitRepo;

        public Domaincontroller(IBusinessRepository businessRepo, IContractRepository contractRepo, IEmployeeRepository employeeRepo, IParkingRepository parkingRepo, IVisitorRepository visitorRepo, IVisitRepository visitRepo)
        {
            this._businessRepo = businessRepo;
            this._contractRepo = contractRepo;
            this._employeeRepo = employeeRepo;
            this._parkingRepo = parkingRepo;
            this._visitorRepo = visitorRepo;
            this._visitRepo = visitRepo;
        }

        public void AddParking(string NamePlate)
        {
            _parkingRepo.AddParking(new Models.Parking(NamePlate));
        }

        #region Validation

        public bool IsEmailValid(string emailAddress)
        {
            Regex regex = new Regex(@"^[\w-.]+@([\w-]+.)+[\w-]{2,4}$");
            Match match = regex.Match(emailAddress);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsBtwValid(string BtwNumber)
        {
            Regex regexBE = new Regex(@"(BE)?0[0-9]{9}");
            Match match = regexBE.Match(BtwNumber);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Validation
    }
}