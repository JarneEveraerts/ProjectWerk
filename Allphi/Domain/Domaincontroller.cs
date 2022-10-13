using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Domaincontroller
    {
        private AllPhi _allPhi;

        public Domaincontroller(IBussinesRepository bussinesRepo, IContractRepository contractRepo, IEmployeeRepository employeeRepo, IParkingRepository parkingRepo, IVisitorRepository visitorRepo, IVisitRepository visitRepo)
        {
            _allPhi = new AllPhi(bussinesRepo, contractRepo, employeeRepo, parkingRepo, visitorRepo, visitRepo);
        }

        public void AddParking(string NamePlate)
        {
            _allPhi.Addparking(NamePlate);
        }
    }
}
