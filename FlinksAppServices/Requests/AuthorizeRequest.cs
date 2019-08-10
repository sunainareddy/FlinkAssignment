
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FlinksAppServices.Requests
{
    [DataContract]
    public class AuthorizeRequest
    {
        [DataMember(Name = "MostRecentCached")]
        public bool MostRecentCached { get; set; }

        [DataMember(Name = "LoginId")]
        public string LoginId { get; set; }
    }
}
