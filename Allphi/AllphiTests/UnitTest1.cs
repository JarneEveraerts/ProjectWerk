using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework;

namespace AllphiTestsm
{


    public class UnitTest1
    {
        #region Repos
       private ViewController vc;/* niet mee nogdig in vc plus kloppen niet meer door rollback
        private IBusinessRepository businessRepo = new MockBusinessRepo();
        private IContractRepository contractRepo = new MockContractRepo();
        private IEmployeeRepository employeeRepo = new MockEmployeeRepo();
        private IParkingSpotRepository parkingRepo = new MockParkingSpotRepo();
        private IVisitorRepository visitorRepo = new MockVisitorRepo();
        private IVisitRepository visitRepo = new MockVisitRepo();*/
        Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();

        // Set up the mock to return a mock HttpClient when CreateClient is called
        Mock<HttpClient> mockHttpClient = new Mock<HttpClient>();
        #endregion Repos

        #region DCD


        public UnitTest1()
        {

            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>()))
            .Returns(mockHttpClient.Object);

            vc = new ViewController(mockHttpClientFactory.Object);

        }


        #endregion DCD

        #region licenseplatecheck testen
        [Theory]
        [InlineData( "1abc875")]
        [InlineData( "9zzz999")]
        [InlineData( "1aBC111")]
        [InlineData( "aAAA777")]
        [InlineData( "1aaa111")]
        [InlineData( "aaa111")]
        [InlineData( "111aaa")]
        [InlineData("1000zzz")]
        public void InvalidLicenseplate(string licenseplate)
        {
            
            //Act
            var result = vc.IsLicensePlateValid(licenseplate);
            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("1ABC213")]
        [InlineData("123ABC")]
        [InlineData("9ZZZ999")]
        [InlineData("1AAA111")]
        [InlineData("987ZZZ")]
        public void correctlicensplatecheck(string licensplate)
        {
            Assert.True(vc.IsLicensePlateValid(licensplate));
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
            Assert.True(vc.IsBtwValid(btwNumber));
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
            Assert.False(vc.IsBtwValid(btwNumber));
        }


        #endregion btwnumbercheck testen

        #region Email validation test
        [Fact]
        public void badEmail()
        {
            
            Assert.False(vc.IsEmailValid("slechteemail"));
        }
        #endregion Email validation test

        #region Employee test
        [Fact]
        public void checkEmployeeLicesenseplatetrue()
        {
            
            vc.CreateEmployee("test naam", "test", "test", "test", "1ABC123");
            var result = vc.GetEmployeeByName("test naam");
            Assert.NotNull(result);
            //Assert.False(vc.CheckEmployeePlate("1ABC123"));
        }

        [Fact]
        public void checkEmployeebyId()
        {
            //ID???????????
            vc.CreateEmployee("test naam", "test", "test", "test", "1ABC123");

            Assert.Null(vc.GetEmployeeByName("naam"));
        }

        [Fact]
        public void checkEmployeeeflase()
        {
            Assert.Null(vc.GetEmployeeByName("naam"));
        }


        [Theory]
        [InlineData("frans")]
        [InlineData("kante")]
        [InlineData("erik")]
        public void checkEmployeeflase(string name)
        {
    
            vc.CreateEmployee("frans", "test", "test", "test", "1aBC123");
            vc.CreateEmployee("kante", "test", "test", "test", "2BC123");
            vc.CreateEmployee("erik", "test", "test", "test", "aze123");

            Assert.Null(vc.GetEmployeeByName(name));
        }
        [Fact]
        //verandert naar * als input die geparst wordt wa formatexeption geeft cuz tis geen getal tis en ster
        public void DeleteEmployeefailtest()
        {
            Assert.Throws<FormatException>(() => vc.DeleteEmployee(int.Parse("*")));

        }

        [Fact]

        public void deleteEmployeetrue()
        {
            vc.CreateEmployee("test naam", "test", "test", "test", "test");
            vc.DeleteEmployee(0);

            Assert.Contains(vc.GetEmployeeViews(), x => x.Name == "test naam" && x.IsDeleted == true);
        }
        [Fact]
        
        public void updateEmployeetesttrue()
        {
            vc.CreateEmployee("test naam", "test", "test", "test", "test");

            vc.UpdateEmployee("update naam", "update", "update", "update", "1ABC123", 0);

            Assert.Contains(vc.GetEmployeeViews(), x => x.Name == "update naam" && x.IsDeleted == false);
        }

        [Fact]
        //wachten op validatie
        public void updateEmployetestfalse()

        {
            vc.CreateEmployee("test naam", "test", "test", "test", "test");

            vc.UpdateEmployee("update naam", "update", "update", "update", "1aBC123", 0);

            Assert.DoesNotContain(vc.GetEmployeeViews(), x => x.Name == "update naam" && x.Plate == "1aBC123" && x.IsDeleted == false);

        }


        [Fact]

        public void getEmployeeTest()
        {
            vc.CreateEmployee("test naam", "test", "test", "test", "test");
            Assert.NotNull(vc.GetEmployeeViews());
        }

        [Fact]
        public void GetEmployeesByBusinessTest()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0438764850");
            vc.CreateEmployee("frank bouwer", "test@gmail.com", "test", "Allphi", "1ABC123");
            vc.CreateEmployee("irene bouwer", "test@gmail.com", "test", "Allphi", "1ABC124");
            vc.CreateEmployee("marko bouwer", "test@gmail.com", "test", "Allphi", "1ABC125");


            Assert.Equal(3, vc.GetEmployeesByBusiness("Allphi").Count);
        }


        #endregion Employee test

        #region business test

        [Fact]

        public void createBusinessTest()
        {
            vc.CreateBusiness("testbedrijf", "testbtw", "testaddress", "test@testing.com", "BE0123456789");
            Assert.Contains(vc.GetBusinessViews(), x => x.Name == "testbedrijf" && x.IsDeleted == false);
        }

        [Fact]
        //no validation
        public void createBusinessfalsetest()
        {
            vc.CreateBusiness("testbedrijf", "testbtw", "testaddress", "test@testing.com", "B123456789");
            Assert.DoesNotContain(vc.GetBusinessViews(), x => x.Name == "testbedrijf" && x.Btw == "B123456789" && x.IsDeleted == false);
        }

        [Fact]

        public void getBusinessTest()
        {
            Assert.Empty(vc.GetBusinessViews());
        }

        [Fact]

        public void deleteBusinessTest()
        {

            vc.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            vc.DeleteBusiness(0);
            Assert.True(vc.GetBusinessById(0).IsDeleted);
        }

        [Fact]

        public void updateBusinessTest()
        {
            vc.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            vc.UpdateBusiness("updatebedrijf", "updateadress", "updatephone", "test@testing.com", "BE0123488789", 0);

            Assert.Contains(vc.GetBusinessViews(), b => b.Btw == "BE0123488789");

        }

        [Fact]
        //missing validation in vc
        public void updateBusinessTestfalse()
        {
            vc.CreateBusiness("testbedrijf", "testadress", "testphone", "test@test.com", "BE0123456789");

            vc.UpdateBusiness("updatebedrijf", "updateadress", "updatephone", "test@testing.com", "BE0128789", 0);


            Assert.DoesNotContain(vc.GetBusinessViews(), b => b.Btw == "BE0128789");

        }

        [Fact]

        public void getBusinessTesttrue()
        {

            vc.CreateBusiness("testbedrijf1", "testadress", "testphone", "test@test.com", "BE0123456789");
            vc.CreateBusiness("testbedrijf2", "testadress", "testphone", "test@test.com", "BE0123456789");
            vc.CreateBusiness("testbedrijf3", "testadress", "testphone", "test@test.com", "BE0123456789");


            Assert.NotNull(vc.GetBusinessByName("testbedrijf1"));


        }

        [Fact]

        public void GetBuSinessIDByEmployeetest()
        {

            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0438764850");
            vc.CreateEmployee("frank bouwer", "test@gmail.com", "test", "Allphi", "1ABC123");

            vc.GetBusinessIdByEmployeeName("bouwer");
            Assert.Equal("Allphi",vc.GetBusinessIdByEmployeeName("bouwer").Name);
        }


        #endregion business test

        #region parkingtesten
        [Fact]
        public void ParkingSpotCheckVisitor()
        {
            Assert.Null(vc.GetParkingSpotViews());
        }

        [Fact]

        public void ParkingSpotCheckEmployee()
        {
            vc.CreateBusiness("iwein de moor", "testadress", "testphone", "test@testing.com0", "BE0123456789");
            vc.EnterParking("1ABC123", "iwein de moor");
            Assert.Contains(vc.GetParkingSpotViews(), b => b.Plate == "1ABC123");
        }


        [Fact]
        public void GetParkingSpots()
        {
            Assert.Equal(0, vc.GetParkingSpotViews().Count);

        }

        [Fact]
        public void ExitParkingTestTrue()
        {
            vc.EnterParking("1ABC123", "iwein de moor");
            vc.ExitParking("1ABC123");

            Assert.Contains(vc.GetParkingSpotViews(), b => b.Plate == "1ABC123" && b.IsDeleted == true);
        }

        [Fact]
        public void exitParkingTestFalsePlate()
        {
            Assert.False(vc.ExitParking("1ABC123"));
        }
        [Fact]
        public void ParkingSpotExistFalse()
        {
            Assert.False(vc.ParkingSpotExists("1ABC123"));
        }

        [Fact]
        public void ParkingSpotExistTrue()
        {
            vc.EnterParking("1ABC123", "Allphi");
            Assert.True(vc.ParkingSpotExists("1ABC123"));
        }

        [Fact]
        public void SubmitVisitorTestTrue()
        {
            vc.EnterParking("1ABC123","Allphi");
            Assert.True(vc.ParkingSpotExists("1ABC123"));
        }
        [Fact]
        public void SubmitVisitorTestFalse()
        {
            vc.EnterParking("1BC123", "Allphi");
            vc.EnterParking("1gBC123", "Nophi");
            Assert.Contains(vc.GetParkingSpotViews(), b => b.Plate == null);

        }

        [Fact]

        public void ParkingWithContractTest()
        {
            ///// !!!!! vc aangepast omdat > omgekeerd stond kan nogsteeds zo zijn door rollback
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0438764850");
            vc.CreateEmployee("frank bouwer", "test@gmail.com", "test", "Allphi", "1ABC123");
            vc.CreateContract(5, "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));

            vc.EnterParking("1ABC123", "Allphi");
            Assert.Contains(vc.GetParkingSpotViews(), b => b.Plate == "1ABC123" && b.Reserved.Name == "Allphi");
        }

        #endregion parkingtesten

        #region contact testen
        [Fact]

        public void CreateContractTRUE()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0123456789");
            vc.CreateContract(5, "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));

            Assert.NotNull(vc.GetContractByBusiness("Allphi"));
        }


        [Fact]

        public void contracttest()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0123456789");
            vc.CreateContract(5, "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));

            vc.DeleteContract(0);

            Assert.Contains(vc.GetContractViews(), b => b.IsDeleted == true && b.Business.Name == "Allphi");


        }

        [Fact]

        public void getcontractBusinessTrue()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0123456789");
            vc.CreateContract(5, "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));

            Assert.NotNull(vc.GetContractByBusiness("Allphi"));
        }


        [Fact]

        public void updateContract()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@gmail.com", "BE0123456789");
            vc.CreateContract(5, "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2023"));
            vc.UpdateContract("5", "Allphi", DateTime.Parse("Jan 1 2022"), DateTime.Parse("Jan 1 2028"), 0);
            Assert.Contains(vc.GetContractViews(), b => b.EndDate == DateTime.Parse("Jan 1 2028") && b.Business.Name == "Allphi");
        }

        #endregion contact testen

        #region Visitor testen
        [Fact]
        public void CreateVisitorTEST()
        {

            // !!!!namen met de zijn schuffed plus omgedraaid?
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            vc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            vc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            Assert.Contains(vc.GetVisitorViews(), b => b.Name == "iwein");
        }

        [Fact]

        public void UpdateVisitorTest()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            vc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            vc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            vc.UpdateVisitor("frank de meersman", "test@testmail.com", "1ABC123", "Allphi", 0);

            Assert.DoesNotContain(vc.GetVisitorViews(), b => b.Name == "iwein");

        }

        [Fact]

        public void CreateVisitorBalieTest()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            vc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            vc.CreateVisitorBalie("iwein", "test@testmail.com", "1ABC123", "Allphi");

            Assert.NotNull(vc.GetVisitorByName("iwein"));

        }

        [Fact]

        public void CreateVisitorPlateTest()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            vc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            vc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            vc.CreateVisitorWithPlate("iwein", "test@testmail.com", "1ABC123", "notAllphi", "mark", "Allphi");

            Assert.NotNull(vc.GetVisitorByEmail("test@testmail.com"));

        }

        [Fact]

        public void deleteVisitorTest()
        {
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            vc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            vc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            vc.DeleteVisitor(0);

            Assert.Contains(vc.GetVisitorViews(), b => b.Name == "iwein" && b.IsDeleted == true);

        }
        #endregion Visitor testen

        #region visit testen

        [Fact]
        public void CreateVisitTest()
        {

            // !!!!namen met de zijn schuffed plus omgedraaid?
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            vc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            vc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            Assert.NotNull(vc.GetVisitByName("iwein"));
        }

        [Fact]
        public void UpdateVisitTest()
        {

            // !!!!namen met de zijn schuffed plus omgedraaid?
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            vc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            vc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            vc.UpdateVisit("iwein", "Mark boer", "Allphi", DateTime.Parse("Jan 1 2022 16:00:00"), DateTime.Parse("Jan 1 2023 18:00:00"));

            Assert.Contains(vc.GetVisitorViews(), b => b.Name == "iwein");
        }

        [Fact]
        public void DeleteVisit()
        {

            // !!!!namen met de zijn schuffed plus omgedraaid?
            vc.CreateBusiness("Allphi", "testadress", "testphone", "test@testmail.com", "be0123456789");
            vc.CreateEmployee("Mark boer", "test@testmail.com", "onthaalmedewerker", "Allphi", null);
            vc.CreateVisitor("iwein", "test@testmail.com", "testorg", "mark", "Allphi");
            vc.DeleteVisit("iwein");
            Assert.Contains(vc.GetVisitorViews(), b => b.IsDeleted == true && b.Name == "iwein");


        }

        #endregion visit testen

    }
}