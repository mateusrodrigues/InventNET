using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventNET.Desktop.Models
{
    public enum TransactionType
    {
        Inwards = 1,
        Outwards = 2
    }

    public class Transaction
    {
        public Guid TransactionID { get; set; }
        public TransactionType Type { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ProductTransaction> ProductTransactions { get; set; }
    }
}
