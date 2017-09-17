using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventNET.Desktop.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public int MinimumQuantity { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }

        public virtual ICollection<ProductTransaction> ProductTransactions { get; set; }
    }
}
