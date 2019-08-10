using System;
using System.Threading.Tasks;
using FlinksAppServices.Requests;
using FlinksAppServices.Response;

namespace FlinksAppServices
{
    public interface IApiServices
    {
        Task<AuthorizeResponse> Authenticate(AuthorizeRequest request);
        Task<AccountDetailsResponse> GetAccountDetails(GetAccountDetailsRequest request);

        Task<AccountDetailsResponse> GetAccountDetailsAsync(GetAccountDetailsRequest request);
    }
}
