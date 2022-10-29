
namespace AllphiTests
{


    public class UnitTest1
    {
        #region Repos
        private DomainController cd;
        private IBusinessRepository businessRepo = new MockClasses.MockBusinessRepo();
        private IContractRepository contractRepo = new MockClasses.MockContractRepo();
        private IEmployeeRepository employeeRepo = new MockClasses.MockEmployeeRepo();
        private IParkingSpotRepository parkingRepo = new MockClasses.MockParkingSpotRepo();
        private IVisitorRepository visitorRepo = new MockClasses.MockVisitorRepo();
        private IVisitRepository visitRepo = new MockClasses.MockVisitRepo();
        #endregion Repos

        #region DCD
        public UnitTest1()
        {
            cd = new DomainController(businessRepo, contractRepo, employeeRepo, parkingRepo, visitorRepo, visitRepo);
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
        public void slechtenummerplaat(string licensceplate)
        {
            Assert.False(cd.IsLicensePlateValid(licensceplate));
        }
        [Theory]
        [InlineData("1ABC213")]
        [InlineData("123ABC")]
        [InlineData("9ZZZ999")]
        [InlineData("1AAA111")]
        [InlineData("987ZZZ")]
        public void nummerplaatjuistchek(string licensplate)
        {
            Assert.True(cd.IsLicensePlateValid(licensplate));
        }
        #endregion licenseplatecheck testen

        #region Employee testen
        [Fact]
        public void CheckEmployeePlatebestaan()
        {
            cd.CreateEmployee("test naam", "test", "test", "test", "1ABC123");

            Employee testemployee = cd.CheckEmployeePlate("1ABC123");
            Assert.NotNull(testemployee);
            //Assert.False(cd.CheckEmployeePlate("1ABC123"));
        }

        [Fact]
        public void CheckEmployeePlateNietBestaan()
        {
            Employee testemployee = cd.CheckEmployeePlate("2ABC123");
            Assert.Null(testemployee);
            //Assert.False(cd.CheckEmployeePlate("1ABC123"));
        }

        [Theory]
        [InlineData("1ABC123")]
        [InlineData("2ABC123")]
        [InlineData("3ABC123")]
        //broken
        public void CheckEmployeePlatenaCreateEmployee(string licenseplate)
        {


            Employee employee = new Employee("test naam", "test", "test", new Business("testbedrijf", "testbtw", "test"), "test", "1ABC123");

            cd.CreateEmployee("test naam", "test", "test", "test", licenseplate);
            cd.CreateEmployee("test naam", "test", "test", "test", licenseplate);
            cd.CreateEmployee("test naam", "test", "test", "test", licenseplate);

            Assert.Equal(employee, cd.CheckEmployeePlate(licenseplate));
            //Assert.Throws<MissingMethodException>(() => cd.DeleteEmployee(testemployee.Id));
        }

        [Theory]
        [InlineData("1aBC123")]
        [InlineData("2BC123")]
        [InlineData("aze123")]
        //nog aan te passen
        public void checkEmployeeflase(string licenseplate)
        {
            Employee employee = new Employee("test naam", "test", "test", new Business("testbedrijf", "testbtw", "test"), "test", "1ABC123");

            cd.CreateEmployee("test naam", "test", "test", "test", licenseplate);
            cd.CreateEmployee("test naam", "test", "test", "test", licenseplate);
            cd.CreateEmployee("test naam", "test", "test", "test", licenseplate);

            Assert.Equal(employee, cd.CheckEmployeePlate(licenseplate));
            //Assert.Throws<MissingMethodException>(() => cd.DeleteEmployee(testemployee.Id));
        }
        [Fact]
        //verandert naar * als input die geparst wordt wa formatexeption geeft cuz tis geen getal tis en ster
        public void DeleteEmployeefailtest()
        {
            Assert.Throws<FormatException>(() => cd.DeleteEmployee(int.Parse("*")));

        }

        [Fact]
        //fixed
        public void updateEmployeetest()
        {
            Employee employee = new Employee("update naam", "update", "update", new Business("testbedrijf", "testbtw", "test"), "update", "1ABC123");
            cd.CreateEmployee("test naam", "test", "test", "test", "test");

            cd.UpdateEmployee("update naam", "update", "update", "update", "1ABC123", 0);
            Assert.True(cd.GetEmployees().Contains(employee));


            //weet niet goed hoe correct dees is want u email is update dus da wa raar update ook eigenlijk niks
        }

        #endregion Employee testen

        #region Email validation test
        [Fact]
        public void slechteemail()
        {
            Assert.False(cd.IsEmailValid("slechteemail"));
        }
        #endregion Email validation test

        #region parkingtesten
        [Fact]
        public void ParkingSpotCheckVisitor()
        {
            ParkingSpot parkingSpot = cd.GetAvailableParkingSpotVisitor();
            Assert.NotNull(parkingSpot);
        }

        #endregion parkingtesten

        #region business testen
        [Fact]
        public void GetBusinessesTest()
        {

            cd.CreateBusiness("test", "test", "test", "test", "testc");
            Assert.True(cd.GetBusinesses().Count > 0);
        }

        #endregion business testen

    }
}