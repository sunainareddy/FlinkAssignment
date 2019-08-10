using FlinksAppServices;
using FlinksAppServices.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace FlinksDemo.Tests
{
    [TestClass]
    [TestCategory("ApiTests")]
    public class ApiHandlerTests
    {
        private IApiServices _apiServices;
        public string LoginId = string.Empty;


        [TestInitialize]
        public void Initialize()
        {
            _apiServices = new ApiServices();
            var config = new ConfigurationBuilder()
            .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            LoginId = config["LoginId"];

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Returns_Exception_OnInEmptyLoginId()
        {
            string LoginId = string.Empty;
            var result = await _apiServices.Authenticate(new AuthorizeRequest() { LoginId = LoginId, MostRecentCached = true });
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Returns_Exception_OnInValidLoginId()
        {
            string LoginId = Guid.NewGuid().ToString();
            var result = await _apiServices.Authenticate(new AuthorizeRequest() { LoginId = LoginId, MostRecentCached = true });
        }

        [TestMethod]
        public async Task Returns_ValidStatusCode_OnValidLoginId()
        {
            string LoginId = "a61b4fb6-31ec-46bb-52d5-08d71cf6c152";
            var result = await _apiServices.Authenticate(new AuthorizeRequest() { LoginId = LoginId, MostRecentCached = true });
            Assert.IsTrue(result.HttpStatusCode == 200);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Returns_ValidRequestId_WhenLoginIdIsValid()
        {
            string LoginId = "a61b4fb6-31ec-46bb-52d5-08d71cf6c152";
            var requests = await _apiServices.Authenticate(new AuthorizeRequest() { LoginId = LoginId, MostRecentCached = true });
            var result = await _apiServices.GetAccountDetails(new GetAccountDetailsRequest() { RequestId = requests.RequestId });
            Assert.IsTrue(result.EnsureSuccessStatusCode().StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Returns_InValidRequestId_WhenLoginIdIsInValid()
        {
            string LoginId = Guid.NewGuid().ToString();
            var requests = await _apiServices.Authenticate(new AuthorizeRequest() { LoginId = LoginId, MostRecentCached = true });
            var result = await _apiServices.GetAccountDetails(new GetAccountDetailsRequest() { RequestId = requests.RequestId });
            Assert.IsTrue(result.EnsureSuccessStatusCode().StatusCode == System.Net.HttpStatusCode.BadRequest);
            Assert.IsTrue(!result.IsSuccessStatusCode);
        }

        [TestMethod]
        public async Task Returns_ValidAccountDetails_WhenLoginIdIsValid()
        {
            string LoginId = "a61b4fb6-31ec-46bb-52d5-08d71cf6c152";
            var requests = await _apiServices.Authenticate(new AuthorizeRequest() { LoginId = LoginId, MostRecentCached = true });
            var result = await _apiServices.GetAccountDetailsAsync(new GetAccountDetailsRequest() { RequestId = requests.RequestId });
            Assert.IsTrue(result.EnsureSuccessStatusCode().StatusCode == System.Net.HttpStatusCode.OK);
            Assert.IsTrue(result.IsSuccessStatusCode);
        }
    }
}
