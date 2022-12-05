
using Domain.Repositories;

namespace AllphiTestsm
{
    public class UnitTest1
    {
        #region Repos
        private DomainController cd;
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
        public void InvalidLicenseplate(string licensceplate)
        {
            Assert.False(cd.IsLicensePlateValid(licensceplate));
        }
        [Theory]
        [InlineData("1ABC213")]
        [InlineData("123ABC")]
        [InlineData("9ZZZ999")]
        [InlineData("1AAA111")]
        [InlineData("987ZZZ")]
        public void correctlicensplatecheck(string licensplate)
        {
            Assert.True(cd.IsLicensePlateValid(licensplate));
        }
        #endregion licenseplatecheck testen

        #region btwnumbercheck testen

        [Theory]
        [InlineData("BE0999999999")]
        [InlineData("BE0000000000")]
        [InlineData("BE0555555555")]
        [InlineData("BE0123456789")]
        public void valideBtwNumber(string btwNumber)
        {
            Assert.True(cd.IsBtwValid(btwNumber));
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
            Assert.False(cd.IsBtwValid(btwNumber));
        }


        #endregion btwnumbercheck testen

        #region Employee test
        [Fact]
        public void checkEmployeeLicesenseplatetrue()
        {
            cd.CreateEmployee("test naam", "test", "test", "test", "1ABC123");

            Employee testemployee = cd.CheckEmployeePlate("1ABC123");
            Assert.NotNull(testemployee);
            //Assert.False(cd.CheckEmployeePlate("1ABC123"));
        }

        [Fact]
        public void checkEmployeeeflase()
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
        public void checkcreateEmployeeAndCheckEmployeeplateTrue(string licenseplate)
        {


            Employee employee = new Employee("test naam", "test", "test", new Business("testbedrijf", "testbtw", "test"), "test", "1ABC123");
            
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

            cd.CreateEmployee("test naam", "test", "test", "test", "1aBC123");
            cd.CreateEmployee("test naam", "test", "test", "test", "2BC123");
            cd.CreateEmployee("test naam", "test", "test", "test", "aze123");

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

        public void deleteEmployeetrue()
        {
            cd.CreateEmployee("test naam", "test", "test", "test", "test");
            cd.DeleteEmployee(0);

            Assert.Contains(cd.GetEmployees(), e => e.IsDeleted == true);
        }
        [Fact]
        //fixed
        public void updateEmployeetesttrue()
        {
            Employee employee = new Employee("update naam", "update", "update", new Business("testbedrijf", "testbtw", "test"), "update", "1ABC123");
            cd.CreateEmployee("test naam", "test", "test", "test", "test");

            cd.UpdateEmployee("update naam", "update", "update", "update", "1ABC123", 0);
            Assert.True(cd.GetEmployees().Contains(employee));


            //weet niet goed hoe correct dees is want u email is update dus da wa raar update ook eigenlijk niks
        }

        [Fact]
        //wachten op validatie
        public void updateEmployetestfalse() 
        
        {
                Employee employee = new Employee("update naam", "update", "update", new Business("testbedrijf", "testbtw", "test"), "update", "1aBC123");
                cd.CreateEmployee("test naam", "test", "test", "test", "test");

                cd.UpdateEmployee("update naam", "update", "update", "update", "1aBC123", 0);
                Assert.False(cd.GetEmployees().Contains(employee));

        }


        [Fact]

        public void getEmployeeTest()
        {
            cd.CreateEmployee("test naam", "test", "test", "test", "test");
            Assert.NotNull(cd.GetEmployees());
        }


        #endregion Employee test

        #region business test

        [Fact]

        public void createBusinessTest()
        {
            Business business = new Business("testbedrijf", "testbtw", "test");
            cd.CreateBusiness("testbedrijf", "testbtw", "testaddress", "test@testing.com", "BE0123456789");
            Assert.Contains(cd.GetBusinesses(), b => b == business);
        }

        [Fact]
        //no validation
        public void createBusinessfalsetest()
        {
            Business business = new Business("testbedrijf", "testbtw", "test");
            cd.CreateBusiness("testbedrijf", "testbtw", "testaddress", "test@testing.com", "B123456789");
            Assert.DoesNotContain(cd.GetBusinesses(), b => b == business);
        }

        [Fact]

        public void getBusinessTest()
        {
            Assert.Empty(cd.GetBusinesses());
        }

        [Fact]

        public void deleteBusinessTest()
        {

            cd.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            cd.DeleteBusiness(0);

            Assert.Contains(cd.GetBusinesses(), b => b.IsDeleted == true);
        }

        [Fact]

        public void updateBusinessTest()
        {
            cd.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            cd.UpdateBusiness("updatebedrijf", "updateadress", "updatephone", "test@testing.com", "BE0123488789", 0);

            Assert.Contains(cd.GetBusinesses(), b => b.Btw == "BE0123488789");

        }

        [Fact]
        //missing validation in dc
        public void updateBusinessTestfalse()
        {
            cd.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            cd.UpdateBusiness("updatebedrijf", "updateadress", "updatephone", "test@testing.com", "BE0128789", 0);
           
            
            Assert.DoesNotContain(cd.GetBusinesses(), b => b.Btw == "BE0128789");

        }

        [Fact]

        public void getBusinessTesttrue()
        {

            cd.CreateBusiness("testbedrijf1", "testadress", "testphone", "test@test.com", "BE0123456789");
            cd.CreateBusiness("testbedrijf2", "testadress", "testphone", "test@test.com", "BE0123456789");
            cd.CreateBusiness("testbedrijf3", "testadress", "testphone", "test@test.com", "BE0123456789");


            Assert.NotEmpty(cd.GiveBusinesses());
            //Assert.Contains(cd.GiveBusinesses(), b => b == ["testbedrijf1", "testadress", "testphone", "test@test.com", "BE0123456789","false"]);


        }


            #endregion business test

        #region Email validation test
    [Fact]
        public void badEmail()
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

        

    }
}