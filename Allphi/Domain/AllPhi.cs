using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AllPhi
    {
        private AllPhi _allPhi;
        #region Repos
        private IBussinesRepository _bussinesRepo;
        private IContractRepository _contractRepo;
        private IEmployeeRepository _employeeRepo;
        private IParkingRepository _parkingRepo;
        private IVisitorRepository _visitorRepo;
        private IVisitRepository _visitRepo;
        #endregion

        public AllPhi(IBussinesRepository bussinesRepo,IContractRepository contractRepo,IEmployeeRepository employeeRepo,IParkingRepository parkingRepo, IVisitorRepository visitorRepo,IVisitRepository visitRepo)
        {
            this._bussinesRepo = bussinesRepo;
            this._contractRepo = contractRepo;
            this._employeeRepo = employeeRepo;
            this._parkingRepo = parkingRepo;
            this._visitorRepo = visitorRepo;
            this._visitRepo = visitRepo;
        }

        public void Addparking(string NamePlate)
        {
            _parkingRepo.AddParking(new Models.Parking(NamePlate));
        }
    }
}
