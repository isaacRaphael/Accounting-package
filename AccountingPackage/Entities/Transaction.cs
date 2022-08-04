using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingPackage.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Guid TransactorId { get; set; }
        public Transactor Transactor { get; set; }
        public bool Debt { get; set; }
        public string Date { get; set; }

    }
}
