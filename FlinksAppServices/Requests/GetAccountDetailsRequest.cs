using System;
using System.Runtime.Serialization;

namespace FlinksAppServices.Requests
{
    [DataContract]
    public class GetAccountDetailsRequest
    {
        [DataMember(Name = "RequestId")]
        public Guid RequestId { get; set; }
    }
}