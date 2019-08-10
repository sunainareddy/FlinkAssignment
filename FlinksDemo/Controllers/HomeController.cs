using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FlinksAppServices;
using FlinksAppServices.Requests;
using FlinksAppServices.Response;
using FlinksDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using FlinksDemo.Models;
using Newtonsoft.Json;
using Holder = FlinksAppServices.Response.Holder;
using Login = FlinksDemo.Models.Login;

namespace FlinksDemo.Controllers
{

    public class HomeController : Controller
    {
        public readonly IApiServices _apiServices;
        public CustomerDetails customerDetails = null;
        public HomeController(IApiServices apiServices)
        {
            _apiServices = apiServices;
            customerDetails=new CustomerDetails();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCustomerData([FromQuery]string id)
        {
            
            if (id != null)
            {
                var result = _apiServices.Authenticate(new AuthorizeRequest() { LoginId = id, MostRecentCached = true }).GetAwaiter().GetResult();
                var accountDetails = _apiServices.GetAccountDetails(new GetAccountDetailsRequest()
                { RequestId = result.RequestId }).GetAwaiter().GetResult();
                 customerDetails = GetAccountData(accountDetails);
                 
            }
            var jsonContent = JsonConvert.SerializeObject(customerDetails);
            return Content(jsonContent);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private CustomerDetails GetAccountData(AccountDetailsResponse response)
        {
            CustomerDetails customerDetails = new CustomerDetails();
            if (response != null)
            {

                customerDetails.LoginId = response.Login?.Id ?? Guid.Empty;
                customerDetails.RequestId = response.RequestId;
                customerDetails.Holder = GetHolderInformarmation(response.Accounts);
                customerDetails.OperationAccounts = GetAccountNumbers(response.Accounts);
                customerDetails.USDAccounts = GetUsdAccountDetails(response.Accounts);
                customerDetails.BiggestCreditTrxId = GetLargestTranscationId(response.Accounts);
            }

            return customerDetails;
        }

        private FlinksDemo.Entities.Holder GetHolderInformarmation(List<Accounts> accounts)
        {
            FlinksDemo.Entities.Holder holderInfo = new FlinksDemo.Entities.Holder();
            var accountDetails = accounts.FirstOrDefault();
            if (accountDetails != null)
            {
                holderInfo.Email = accountDetails.Holder.Email;
                holderInfo.Name = accountDetails.Holder.Name;
            }

            return holderInfo;
        }

        private Guid GetLargestTranscationId(List<Accounts> accounts)
        {
            var transactionDetails = accounts.SelectMany(x => x.Transactions).ToList().OrderByDescending(x => x.Credit).FirstOrDefault();
            if (transactionDetails != null)
            {
                return transactionDetails.Id;
            }
            return Guid.Empty;
        }
        private List<OperationAccounts> GetAccountNumbers(List<Accounts> accounts)
        {
            List<OperationAccounts> accountNumbers = new List<OperationAccounts>();
            foreach (var account in accounts)
            {
                if (int.TryParse(account.AccountNumber, out int number))
                {
                    accountNumbers.Add(new OperationAccounts() { AccountNumber = number });
                }
            }

            return accountNumbers;
        }

        private List<USDAccounts> GetUsdAccountDetails(List<Accounts> accounts)
        {
            List<USDAccounts> usdAccounts = new List<USDAccounts>();
            var usdAccountDetails = accounts.Where(x => x.Title.ToLower() == "chequing us").ToList();
            if (usdAccountDetails != null && usdAccountDetails.Any())
            {
                foreach (var details in usdAccountDetails)
                {
                    usdAccounts.Add(new USDAccounts() { Balance = (int?)details.Balance?.Current });
                }
            }

            return usdAccounts;
        }
    }
}
