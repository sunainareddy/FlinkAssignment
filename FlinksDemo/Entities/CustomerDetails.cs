using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlinksDemo.Entities
{
    public class CustomerDetails
    {
        public CustomerDetails()
        {
            OperationAccounts = new List<OperationAccounts>();
            USDAccounts = new List<USDAccounts>();
        }
        public Guid LoginId { get; set; }
        public Guid RequestId { get; set; }
        public Holder Holder { get; set; }
        public List<OperationAccounts> OperationAccounts { get; set; }
        public List<USDAccounts> USDAccounts { get; set; }
        public Guid BiggestCreditTrxId { get; set; }
       

    }

    public class Holder
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }

    public class OperationAccounts
    {
        public int AccountNumber { get; set; }
    }

    public class USDAccounts
    {
        public int? Balance { get; set; }
    }



}
