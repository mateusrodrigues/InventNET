using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventNET.Desktop.Models
{
    public class ProductTransaction
    {
        public Guid TransactionID { get; set; }
        public string ProductID { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual Product Product { get; set; }
    }
}
