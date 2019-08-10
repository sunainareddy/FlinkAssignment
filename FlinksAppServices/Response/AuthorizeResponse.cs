using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace FlinksAppServices.Response
{
    [DataContract]
    public class AuthorizeResponse: HttpResponseMessage
    {
        public AuthorizeResponse()
        {
            Links=new List<Links>();
        }
        [DataMember(Name = "Links")]
        public List<Links> Links { get; set; }
        [DataMember(Name = "HttpStatusCode")]
        public int HttpStatusCode { get; set; }
        [DataMember(Name = "Login")]
        public  Login Login { get; set; }
        [DataMember(Name = "Institution")]
        public string Institution { get; set; }
        [DataMember(Name = "RequestId")]
        public Guid RequestId { get; set; }
    }

    [DataContract]
    public class Links
    {
        [DataMember(Name = "rel")]
        public string Rel { get; set; }
        [DataMember(Name = "href")]
        public string Href { get; set; }
        [DataMember(Name = "example")]
        public string Example { get; set; }
    }
    [DataContract]
    public class Login
    {
        [DataMember(Name = "Username")]
        public string Username { get; set; }
        [DataMember(Name = "IsScheduledRefresh")]
        public bool IsScheduledRefresh { get; set; }
        [DataMember(Name = "LastRefresh")]
        public DateTime LastRefresh { get; set; }
        [DataMember(Name = "Type")]
        public string Type { get; set; }
        [DataMember(Name = "Id")]
        public  Guid Id { get; set; }
    }
}
