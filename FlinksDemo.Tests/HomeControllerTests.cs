using System;
using System.Linq;
using System.Threading.Tasks;
using FlinksAppServices;
using FlinksAppServices.Requests;
using FlinksAppServices.Response;
using FlinksDemo.Controllers;
using FlinksDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;

namespace FlinksDemo.Tests
{
    [TestClass]
    [TestCategory("ControllerTests")]

    public class HomeControllerTests
    {
        private Mock<IApiServices> _apiServices;
        public HomeController _homeController;

        [TestInitialize]
        public void Initialize()
        {
            _apiServices = new Mock<IApiServices>();
            _homeController = new HomeController(_apiServices.Object);
        }
        [TestMethod]
        public async Task Returns_ValidData_OnValidLoginId()
        {
            var loginId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();
            var guid4 = Guid.NewGuid();

            var accountDetails = new AccountDetailsResponse()
            {
                Login= new Login() {Id= loginId},
                RequestId=requestId,
                Institution="P-Test",
                Accounts = new System.Collections.Generic.List<Accounts>() { new Accounts() {
                    AccountNumber="1212",
                    Balance=new Balance()
                    {
                        Available=100.23,
                        Current=10000,
                        Limit=300.02
                    },
                    Category="chequing us",
                    Currency="Cad",
                    Holder=new FlinksAppServices.Response.Holder(){Name="Test",Email="test@gmail.com"},
                    Id=loginId,
                    InstitutionNumber="12132323",
                    OverdraftLimit=205.10,
                    Title="test",
                    Transactions=new System.Collections.Generic.List<Transactions>()
                    {
                        new Transactions()
                        {
                            Id=guid1,
                            Balance=100.232,
                            Credit=10121212,
                            Debit=null,

                        },
                         new Transactions()
                        {
                            Id=guid2,
                            Balance=100.232,
                            Credit=null,
                            Debit=1222,

                        },
                          new Transactions()
                        {
                            Id=guid3,
                            Balance=100.232,
                            Credit=23445,
                            Debit=null,

                        },
                            new Transactions()
                        {
                            Id=guid4,
                            Balance=100.232,
                            Credit=2323,
                            Debit=null,

                        }
                    },
                    TransitNumber="1222323",
                    Type="Usa"
                } },
            };
            _apiServices.Setup(x => x.Authenticate( It.Is<AuthorizeRequest>(y=>y.LoginId==loginId.ToString()))).Returns(Task.FromResult(new AuthorizeResponse() {RequestId=requestId }));
            _apiServices.Setup(x => x.GetAccountDetails(It.Is<GetAccountDetailsRequest>(gt=>gt.RequestId==requestId))).Returns(Task.FromResult(accountDetails));
            var result = _homeController.GetCustomerData(loginId.ToString()) as ContentResult;
            var results = JsonConvert.DeserializeObject<CustomerDetails>(result.Content);

            Assert.AreEqual(results.LoginId, loginId);
            Assert.AreEqual(results.RequestId, requestId);
            Assert.AreEqual(results.Holder.Email, "test@gmail.com");
            Assert.AreEqual(results.Holder.Name, "Test");
            Assert.AreEqual(results.OperationAccounts.FirstOrDefault().AccountNumber, 1212);
            Assert.AreEqual(results.USDAccounts.FirstOrDefault(), null);
            Assert.AreEqual(results.BiggestCreditTrxId, guid1);
        }

        [TestMethod]
        public async Task Returns_InValidData_OnValidLoginId()
        {
            var loginId = Guid.NewGuid();
            var requestId = Guid.NewGuid();
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var guid3 = Guid.NewGuid();
            var guid4 = Guid.NewGuid();

            var accountDetails = new AccountDetailsResponse()
            {
                Login = new Login() { Id = loginId },
                RequestId = requestId,
                Institution = "P-Test",
                Accounts = new System.Collections.Generic.List<Accounts>() { new Accounts() {
                    AccountNumber="1212",
                    Balance=new Balance()
                    {
                        Available=100.23,
                        Current=10000,
                        Limit=300.02
                    },
                    Category="chequing us",
                    Currency="Cad",
                    Holder=new FlinksAppServices.Response.Holder(){Name="Test",Email="test@gmail.com"},
                    Id=loginId,
                    InstitutionNumber="12132323",
                    OverdraftLimit=205.10,
                    Title="test",
                    Transactions=new System.Collections.Generic.List<Transactions>()
                    {
                        new Transactions()
                        {
                            Id=guid1,
                            Balance=100.232,
                            Credit=10121212,
                            Debit=null,

                        },
                         new Transactions()
                        {
                            Id=guid2,
                            Balance=100.232,
                            Credit=null,
                            Debit=1222,

                        },
                          new Transactions()
                        {
                            Id=guid3,
                            Balance=100.232,
                            Credit=23445,
                            Debit=null,

                        },
                            new Transactions()
                        {
                            Id=guid4,
                            Balance=100.232,
                            Credit=2323,
                            Debit=null,

                        }
                    },
                    TransitNumber="1222323",
                    Type="Usa"
                } },
            };
            _apiServices.Setup(x => x.Authenticate(It.Is<AuthorizeRequest>(y => y.LoginId == loginId.ToString()))).Returns(Task.FromResult(new AuthorizeResponse() { RequestId = requestId }));
            _apiServices.Setup(x => x.GetAccountDetails(It.Is<GetAccountDetailsRequest>(gt => gt.RequestId == requestId))).Returns(Task.FromResult(accountDetails));
            var result = _homeController.GetCustomerData(loginId.ToString()) as ContentResult;
            var results = JsonConvert.DeserializeObject<CustomerDetails>(result.Content);

            Assert.AreNotEqual(results.LoginId, Guid.NewGuid());
            Assert.AreNotEqual(results.RequestId, Guid.NewGuid());
            Assert.AreNotEqual(results.Holder.Email, "test123@gmail.com");
            Assert.AreNotEqual(results.Holder.Name, "Test122");
            Assert.AreNotEqual(results.OperationAccounts.FirstOrDefault().AccountNumber, 121232322);
            Assert.AreNotEqual(results.USDAccounts.FirstOrDefault(), new USDAccounts());
            Assert.AreNotEqual(results.BiggestCreditTrxId, Guid.NewGuid());
        }
    }
}
