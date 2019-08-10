using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using FlinksAppServices.Requests;
using FlinksAppServices.Response;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace FlinksAppServices
{
    public class ApiServices : IApiServices
    {
        public const string AuthorizeUrl = "http://toolbox-api.private.fin.ag/v3/{0}/BankingServices/Authorize";
        public const string AccountDetailsUrl = " http://toolbox-api.private.fin.ag/v3/{0}/BankingServices/GetAccountsDetail";
        public const string AccountDetailsAsyncUrl = " http://toolbox-api.private.fin.ag/v3/{0}/BankingServices/GetAccountsDetailAsync/{1}";

        public const string CustomerId = "43387ca6-0391-4c82-857d-70d95f087ecb";
        static readonly HttpClient client = new HttpClient();

        public ApiServices()
        {
        }
        public async Task<AuthorizeResponse> Authenticate(AuthorizeRequest request)
        {

            if (string.IsNullOrEmpty(request.LoginId))
            {
                throw new Exception();
            }
            var myContent = JsonConvert.SerializeObject(request);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var builder = new UriBuilder(new Uri(string.Format(AuthorizeUrl, CustomerId)));
            HttpRequestMessage requests = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
            requests.Content = new StringContent(myContent, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.SendAsync(requests);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                throw new Exception();
            }
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsAsync<AuthorizeResponse>();
                return res;
            }

            throw new Exception();
        }

        public async Task<AccountDetailsResponse> GetAccountDetails(GetAccountDetailsRequest request)
        {
            try
            {
                var myContent = JsonConvert.SerializeObject(request);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var builder = new UriBuilder(new Uri(string.Format(AccountDetailsUrl, CustomerId)));
                HttpRequestMessage requests = new HttpRequestMessage(HttpMethod.Post, builder.Uri);
                requests.Content = new StringContent(myContent, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.SendAsync(requests);
                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    var result = await GetAccountDetailsAsync(request);
                    return await response.Content.ReadAsAsync<AccountDetailsResponse>();
                }
                return await response.Content.ReadAsAsync<AccountDetailsResponse>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<AccountDetailsResponse> GetAccountDetailsAsync(GetAccountDetailsRequest request)
        {
            try
            {
                int reTryAttempts = 0;
                bool isRequestSuccess = false;
                do
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var builder = new UriBuilder(new Uri(string.Format(AccountDetailsAsyncUrl, CustomerId, request.RequestId)));
                    HttpResponseMessage response = await client.GetAsync(builder.Uri.AbsoluteUri);
                    var res = await response.Content.ReadAsAsync<AccountDetailsResponse>();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return res;
                    }
                    reTryAttempts += 1;
                    Thread.Sleep(10000);
                }
                while (!isRequestSuccess && reTryAttempts < 30);
                return new AccountDetailsResponse();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
