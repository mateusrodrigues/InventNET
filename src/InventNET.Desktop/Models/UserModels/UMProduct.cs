using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventNET.Desktop.Models.UserModels
{
    public class UMProduct : BindableBase
    {
        private string _productId;
        public string ProductID
        {
            get { return _productId; }
            set { SetProperty(ref _productId, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _minimumQuantity;
        public int MinimumQuantity
        {
            get { return _minimumQuantity; }
            set { SetProperty(ref _minimumQuantity, value); }
        }

        private int _availableQuantity;
        public int AvailableQuantity
        {
            get { return _availableQuantity; }
            set { SetProperty(ref _availableQuantity, value); }
        }

        private decimal _buyingPrice;
        public decimal BuyingPrice
        {
            get { return _buyingPrice; }
            set { SetProperty(ref _buyingPrice, value); }
        }

        private decimal _sellingPrice;
        public decimal SellingPrice
        {
            get { return _sellingPrice; }
            set { SetProperty(ref _sellingPrice, value); }
        }
    }
}
