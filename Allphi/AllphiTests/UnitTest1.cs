
using Domain.Repositories;

namespace AllphiTestsm
{
    public class UnitTest1
    {
        #region Repos
        private DomainController dc;
        private IBusinessRepository businessRepo = new MockBusinessRepo();
        private IContractRepository contractRepo = new MockContractRepo();
        private IEmployeeRepository employeeRepo = new MockEmployeeRepo();
        private IParkingSpotRepository parkingRepo = new MockParkingSpotRepo();
        private IVisitorRepository visitorRepo = new MockVisitorRepo();
        private IVisitRepository visitRepo = new MockVisitRepo();
        #endregion Repos

        #region DCD
        public UnitTest1()
        {
            dc = new DomainController(businessRepo, contractRepo, employeeRepo, parkingRepo, visitorRepo, visitRepo);
        }
        #endregion DCD
        
        #region licenseplatecheck testen
        [Theory]
        [InlineData("1aBC213")]
        [InlineData("9zzz999")]
        [InlineData("1aBC111")]
        [InlineData("aAAA777")]
        [InlineData("1aaa111")]
        [InlineData("aaa111")]
        [InlineData("111aaa")]
        [InlineData("1000zzz")]
        public void InvalidLicenseplate(string licensceplate)
        {
            Assert.False(dc.IsLicensePlateValid(licensceplate));
        }
        [Theory]
        [InlineData("1ABC213")]
        [InlineData("123ABC")]
        [InlineData("9ZZZ999")]
        [InlineData("1AAA111")]
        [InlineData("987ZZZ")]
        public void correctlicensplatecheck(string licensplate)
        {
            Assert.True(dc.IsLicensePlateValid(licensplate));
        }
        #endregion licenseplatecheck testen

        #region btwnumbercheck testen

        [Theory]
        [InlineData("BE0438764850")]
        [InlineData("BE0830158563")]
        [InlineData("BE0824970944")]
        [InlineData("BE0455498538")]
        public void valideBtwNumber(string btwNumber)
        {
            Assert.True(dc.IsBtwValid(btwNumber));
        }
        //uitlegvragen
        [Theory]
        [InlineData("BE99999999")]
        [InlineData("BE11111111")]
        [InlineData("BE055555555")]
        [InlineData("B0123456789")]
        [InlineData("BE06123456789")]
        [InlineData("BE01653456789")]
        [InlineData("0123456789")]
        public void invalideBtwNumber(string btwNumber)
        {
            Assert.False(dc.IsBtwValid(btwNumber));
        }


        #endregion btwnumbercheck testen

        #region Email validation test
        [Fact]
        public void badEmail()
        {
            Assert.False(dc.IsEmailValid("slechteemail"));
        }
        #endregion Email validation test

        #region Employee test
        [Fact]
        public void checkEmployeeLicesenseplatetrue()
        {
            dc.CreateEmployee("test naam", "test", "test", "test", "1ABC123");
            
            Assert.NotNull(dc.CheckEmployeePlate("1ABC123"));
            //Assert.False(dc.CheckEmployeePlate("1ABC123"));
        }

        [Fact]
        public void checkEmployeebyId()
        {
            //ID???????????
            dc.CreateEmployee("test naam", "test", "test", "test", "1ABC123");

            Assert.Equal(dc.GetEmployeeIdByName("naam"), 0);
            //Assert.False(dc.CheckEmployeePlate("1ABC123"));
        }

        [Fact]
        public void checkEmployeeeflase()
        {
            Employee testemployee = dc.CheckEmployeePlate("2ABC123");
            Assert.Null(testemployee);
            //Assert.False(dc.CheckEmployeePlate("1ABC123"));
        }

        [Theory]
        [InlineData("1ABC123")]
        [InlineData("2ABC123")]
        [InlineData("3ABC123")]
        //broken
        public void checkcreateEmployeeAndCheckEmployeeplateTrue(string licenseplate)
        {


            Employee employee = new Employee("test naam", "test", "test", new Business("testbedrijf", "testbtw", "test"), "test", "1ABC123");
            
            dc.CreateEmployee("test naam", "test", "test", "test", licenseplate);

            Assert.Equal(employee, dc.CheckEmployeePlate(licenseplate));
            //Assert.Throws<MissingMethodException>(() => dc.DeleteEmployee(testemployee.Id));
        }

        [Theory]
        [InlineData("1aBC123")]
        [InlineData("2BC123")]
        [InlineData("aze123")]
        //nog aan te passen
        public void checkEmployeeflase(string licenseplate)
        {
            Employee employee = new Employee("test naam", "test", "test", new Business("testbedrijf", "testbtw", "test"), "test", "1ABC123");

            dc.CreateEmployee("test naam", "test", "test", "test", "1aBC123");
            dc.CreateEmployee("test naam", "test", "test", "test", "2BC123");
            dc.CreateEmployee("test naam", "test", "test", "test", "aze123");

            Assert.Equal(employee, dc.CheckEmployeePlate(licenseplate));
            //Assert.Throws<MissingMethodException>(() => dc.DeleteEmployee(testemployee.Id));
        }
        [Fact]
        //verandert naar * als input die geparst wordt wa formatexeption geeft cuz tis geen getal tis en ster
        public void DeleteEmployeefailtest()
        {
            Assert.Throws<FormatException>(() => dc.DeleteEmployee(int.Parse("*")));

        }

        [Fact]

        public void deleteEmployeetrue()
        {
            dc.CreateEmployee("test naam", "test", "test", "test", "test");
            dc.DeleteEmployee(0);

            Assert.Contains(dc.GetEmployees(), e => e.IsDeleted == true);
        }
        [Fact]
        //fixed
        public void updateEmployeetesttrue()
        {
            Employee employee = new Employee("update naam", "update", "update", new Business("testbedrijf", "testbtw", "test"), "update", "1ABC123");
            dc.CreateEmployee("test naam", "test", "test", "test", "test");

            dc.UpdateEmployee("update naam", "update", "update", "update", "1ABC123", 0);
            Assert.True(dc.GetEmployees().Contains(employee));


            //weet niet goed hoe correct dees is want u email is update dus da wa raar update ook eigenlijk niks
        }

        [Fact]
        //wachten op validatie
        public void updateEmployetestfalse() 
        
        {
                Employee employee = new Employee("update naam", "update", "update", new Business("testbedrijf", "testbtw", "test"), "update", "1aBC123");
                dc.CreateEmployee("test naam", "test", "test", "test", "test");

                dc.UpdateEmployee("update naam", "update", "update", "update", "1aBC123", 0);
                Assert.False(dc.GetEmployees().Contains(employee));

        }


        [Fact]

        public void getEmployeeTest()
        {
            dc.CreateEmployee("test naam", "test", "test", "test", "test");
            Assert.NotNull(dc.GetEmployees());
        }

        [Fact]
        public void GetEmployeesByBusinessTest()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0438764850");
            dc.CreateEmployee("frank bouwer", "test@gmail.com", "test", "Allphi", "1ABC123");
            dc.CreateEmployee("irene bouwer", "test@gmail.com", "test", "Allphi", "1ABC124");
            dc.CreateEmployee("marko bouwer", "test@gmail.com", "test", "Allphi", "1ABC125");

            
            Assert.Equal(3, dc.GetEmployeesByBusiness("Allphi").Count);
        }


        #endregion Employee test

        #region business test

        [Fact]

        public void createBusinessTest()
        {
            Business business = new Business("testbedrijf", "testbtw", "test");
            dc.CreateBusiness("testbedrijf", "testbtw", "testaddress", "test@testing.com", "BE0123456789");
            Assert.Contains(dc.GetBusinesses(), b => b == business);
        }

        [Fact]
        //no validation
        public void createBusinessfalsetest()
        {
            Business business = new Business("testbedrijf", "testbtw", "test");
            dc.CreateBusiness("testbedrijf", "testbtw", "testaddress", "test@testing.com", "B123456789");
            Assert.DoesNotContain(dc.GetBusinesses(), b => b == business);
        }

        [Fact]

        public void getBusinessTest()
        {
            Assert.Empty(dc.GetBusinesses());
        }

        [Fact]

        public void deleteBusinessTest()
        {

            dc.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            dc.DeleteBusiness(0);
            Business business = dc.GetBusinessById(0);
            Assert.True(business.IsDeleted);
        }

        [Fact]

        public void updateBusinessTest()
        {
            dc.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            dc.UpdateBusiness("updatebedrijf", "updateadress", "updatephone", "test@testing.com", "BE0123488789", 0);

            Assert.Contains(dc.GetBusinesses(), b => b.Btw == "BE0123488789");

        }

        [Fact]
        //missing validation in dc
        public void updateBusinessTestfalse()
        {
            dc.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            dc.UpdateBusiness("updatebedrijf", "updateadress", "updatephone", "test@testing.com", "BE0128789", 0);
           
            
            Assert.DoesNotContain(dc.GetBusinesses(), b => b.Btw == "BE0128789");

        }

        [Fact]

        public void getBusinessTesttrue()
        {

            dc.CreateBusiness("testbedrijf1", "testadress", "testphone", "test@test.com", "BE0123456789");
            dc.CreateBusiness("testbedrijf2", "testadress", "testphone", "test@test.com", "BE0123456789");
            dc.CreateBusiness("testbedrijf3", "testadress", "testphone", "test@test.com", "BE0123456789");


            Assert.NotEmpty(dc.GiveBusinesses());
            //Assert.Contains(dc.GiveBusinesses(), b => b == ["testbedrijf1", "testadress", "testphone", "test@test.com", "BE0123456789","false"]);


        }

        /*[Fact]

        public void GetBuSinessIDByEmployeetest()
        {
            
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0438764850");
            dc.CreateEmployee("frank bouwer", "test@gmail.com", "test", "Allphi", "1ABC123");

            dc.GetBusinessIdByEmployeeName("bouwer");
            Assert.Equal("Allphi",dc.GetBusinessIdByEmployeeName("bouwer").Name);
        }*/


        #endregion business test

        #region parkingtesten
        [Fact]
        public void ParkingSpotCheckVisitor()
        {
            Assert.NotNull(dc.GetAvailableParkingSpotVisitor());
        }

        [Fact]

        public void ParkingSpotCheckEmployee()
        {
            dc.CreateBusiness("iwein de moor", "testadress", "testphone", "test@testing.com0", "BE0123456789");
            dc.EnterParking("1ABC123", "iwein de moor");
            Assert.Contains(dc.GetParkingSpots(), b => b.Plate == "1ABC123");
        }


        [Fact]
        public void GetParkingSpots()
        {
            Assert.Equal(0, dc.GetParkingSpots().Count);

        }

        [Fact]
        public void ExitParkingTestTrue()
        {
            dc.EnterParking("1ABC123", "iwein de moor");
            dc.ExitParking("1ABC123");

            Assert.Contains(dc.GetParkingSpots(), b => b.Plate == "1ABC123" && b.IsDeleted == true);
        }

        [Fact]
        public void exitParkingTestFalsePlate()
        {
            Assert.False(dc.ExitParking("1ABC123"));
        }
        [Fact]
        public void ParkingSpotExistFalse()
        {
            Assert.False(dc.ParkingSpotExists("1ABC123"));
        }

        [Fact]
        public void ParkingSpotExistTrue()
        {
            dc.EnterParking("1ABC123", "iwein de moor");
            Assert.True(dc.ParkingSpotExists("1ABC123"));
        }

        [Fact]
        public void SubmitVisitorTestTrue()
        {
            dc.SubmitVisitorParking("1ABC123");
            Assert.True(dc.ParkingSpotExists("1ABC123"));
        }
        [Fact]
        public void SubmitVisitorTestFalse()
        {
            dc.SubmitVisitorParking("1ABC123");
            dc.SubmitVisitorParking("1ABC123");
            Assert.Contains(dc.GetParkingSpots(), b => b.Plate == null);

        }

        [Fact]

        public void ParkingWithContractTest() 
        {
            ///// !!!!! dc aangepast omdat > omgekeerd stond 
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0438764850");
            dc.CreateEmployee("frank bouwer", "test@gmail.com", "test", "Allphi", "1ABC123");
            dc.CreateContract("5", "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));

            dc.EnterParking("1ABC123", "Allphi");
            Assert.Contains(dc.GetParkingSpots(), b => b.Plate == "1ABC123" && b.Reserved == dc.GetBusinessByBtw("BE0438764850"));
        }

        #endregion parkingtesten

        #region contact testen
        [Fact]

        public void CreateContractTRUE()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0123456789");
            dc.CreateContract("5", "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));

            Assert.NotNull(dc.GetContractByBusiness("Allphi"));
        }


        [Fact]

        public void contracttest()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0123456789");
            dc.CreateContract("5", "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));

            dc.DeleteContract(0);

            Assert.Contains(dc.GetContracts(), b => b.IsDeleted == true);


        }

        [Fact]

        public void getcontractBusinessTrue()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0123456789");
            dc.CreateContract("5", "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));

            Assert.NotNull(dc.GetContractByBusiness("Allphi"));
        }


        [Fact]

        public void updateContract()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0123456789");
            dc.CreateContract("5", "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));
            dc.UpdateContract("5", "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2028"), 0);
            Assert.Contains(dc.GetContracts(), b => b.EndDate == DateTime.Parse("Jan 1 2028"));
        }

        #endregion contact testen

        #region Visitor testen
        [Fact]
        public void CreateVisitorTEST()
        {

            // !!!!namen met de zijn schuffed plus omgedraaid?
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            dc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            dc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            Assert.Contains(dc.GetVisitors(), b => b.Name == "iwein");
        }

        [Fact]

        public void UpdateVisitorTest()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            dc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            dc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            dc.UpdateVisitor("frank de meersman", "test@testmail.com", "1ABC123", "Allphi", 0);

            Assert.DoesNotContain(dc.GetVisitors(), b => b.Name == "iwein");

        }

        [Fact]

        public void CreateVisitorBalieTest()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            dc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            dc.CreateVisitorBalie("iwein", "test@testmail.com", "1ABC123", "Allphi");

            Assert.NotNull(dc.GetVisitorByName("iwein"));

        }

        [Fact]

        public void CreateVisitorPlateTest()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            dc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            dc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            dc.CreateVisitorWithPlate("iwein", "test@testmail.com", "1ABC123", "notAllphi", "mark", "Allphi");

            Assert.NotNull(dc.GetVisitorByEmail("test@testmail.com"));

        }

        [Fact]

        public void deleteVisitorTest()
        {
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            dc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            dc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            dc.DeleteVisitor(0);

            Assert.Contains(dc.GetVisitors(), b => b.Name == "iwein" && b.IsDeleted == true);

        }
        #endregion Visitor testen

        #region visit testen

        [Fact]
        public void CreateVisitTest()
        {

            // !!!!namen met de zijn schuffed plus omgedraaid?
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            dc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            dc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            Assert.NotNull(dc.GetVisitByName("iwein"));
        }

        [Fact]
        public void UpdateVisitTest()
        {

            // !!!!namen met de zijn schuffed plus omgedraaid?
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            dc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            dc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            dc.UpdateVisit("iwein", "Mark boer", "Allphi", DateTime.Parse("Jan 1 2022 16:00:00"), DateTime.Parse("Jan 1 2023 18:00:00"));

            Visit visit = new Visit(dc.GetVisitorByName("iwein"), dc.GetBusinessByBtw("be0123456789"), dc.GetEmployeeByName("Mark boer"), DateTime.Parse("Jan 1 2022 16:00:00"), DateTime.Parse("Jan 1 2023 18:00:00"));
            Assert.Contains(dc.GetVisits(), b => b == visit);
        }

        [Fact]
        public void DeleteVisit()
        {

            // !!!!namen met de zijn schuffed plus omgedraaid?
            dc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            dc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            dc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            dc.DeleteVisit("iwein");
            Assert.Contains(dc.GetVisits(), b => b.IsDeleted == true && b.Visitor.Name == "iwein");

            
        }

        #endregion visit testen

    }
}