using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace FlinksAppServices.Response
{
    [DataContract]
    public class AccountDetailsResponse: HttpResponseMessage
    {
        public AccountDetailsResponse()
        {
            Accounts= new List<Accounts>();
        }
        [DataMember(Name = "Accounts")]
        public List<Accounts> Accounts { get; set; }
        [DataMember(Name = "Login")]
        public Login Login { get; set; }
        [DataMember(Name = "Institution")]
        public string Institution { get; set; }
        [DataMember(Name = "RequestId")]
        public Guid RequestId { get; set; }
    }
    [DataContract]
    public class Accounts
    {
        public Accounts()
        {
            Transactions = new List<Transactions>();
        }
        [DataMember(Name = "Transactions")]
        public List<Transactions> Transactions { get; set; }
        [DataMember(Name = "TransitNumber")]
        public string TransitNumber { get; set; }
        [DataMember(Name = "InstitutionNumber")]
        public string InstitutionNumber { get; set; }
        [DataMember(Name = "OverdraftLimit")]
        public double OverdraftLimit { get; set; }
        [DataMember(Name = "Title")]
        public string Title { get; set; }
        [DataMember(Name = "AccountNumber")]
        public string AccountNumber { get; set; }
        [DataMember(Name = "Balance")]
        public Balance Balance { get; set; }
        [DataMember(Name = "Category")]
        public string Category { get; set; }
        [DataMember(Name = "Type")]
        public string Type { get; set; }
        [DataMember(Name = "Currency")]
        public string Currency { get; set; }
        [DataMember(Name = "Holder")]
        public Holder Holder { get; set; }
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }
    }

    [DataContract]
    public class Holder
    {
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Address")]
        public Address Address { get; set; }
        [DataMember(Name = "Email")]
        public string Email { get; set; }
        [DataMember(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
    }

    [DataContract]
    public class Address
    {
        [DataMember(Name = "CivicAddress")]
        public string CivicAddress { get; set; }
        [DataMember(Name = "City")]
        public string City { get; set; }
        [DataMember(Name = "Province")]
        public string Province { get; set; }
        [DataMember(Name = "PostalCode")]
        public string PostalCode { get; set; }
        [DataMember(Name = "POBox")]
        public string POBox { get; set; }
        [DataMember(Name = "Country")]
        public string Country { get; set; }
    }

    [DataContract]
    public class Balance
    {
        [DataMember(Name = "Available")]
        public double? Available { get; set; }
        [DataMember(Name = "Current")]
        public double? Current { get; set; }
        [DataMember(Name = "Limit")]
        public double? Limit { get; set; }
    }


    [DataContract]
    public class Transactions
    {
        [DataMember(Name = "Date")]
        public DateTime Date { get; set; }
        [DataMember(Name = "Code")]
        public int? Code { get; set; }
        [DataMember(Name = "Description")]
        public string Description { get; set; }
        [DataMember(Name = "Debit")]
        public double? Debit { get; set; }
        [DataMember(Name = "Credit")]
        public double? Credit { get; set; }
        [DataMember(Name = "Balance")]
        public double? Balance { get; set; }
        [DataMember(Name = "Id")]
        public Guid Id { get; set; }
    }

}
