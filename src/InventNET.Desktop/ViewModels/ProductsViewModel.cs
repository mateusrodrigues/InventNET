using InventNET.Desktop.Data;
using InventNET.Desktop.Models;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventNET.Desktop.ViewModels
{
    public class ProductsViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ApplicationDbContext _context;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { SetProperty(ref _products, value); }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { SetProperty(ref _selectedProduct, value); }
        }

        private int _totalAvailableQuantity;
        public int TotalAvailableQuantity
        {
            get { return _totalAvailableQuantity; }
            set { SetProperty(ref _totalAvailableQuantity, value); }
        }

        private decimal _totalBuyingPrice;
        public decimal TotalBuyingPrice
        {
            get { return _totalBuyingPrice; }
            set { SetProperty(ref _totalBuyingPrice, value); }
        }

        private decimal _totalSellingPrice;
        public decimal TotalSellingPrice
        {
            get { return _totalSellingPrice; }
            set { SetProperty(ref _totalSellingPrice, value); }
        }

        public DelegateCommand AddProductCommand { get; set; }
        public DelegateCommand DeleteProductCommand { get; set; }
        public DelegateCommand EditProductCommand { get; set; }

        public ProductsViewModel(IRegionManager regionManager, ApplicationDbContext context)
        {
            _regionManager = regionManager;
            _context = context;

            AddProductCommand = new DelegateCommand(AddProduct);
            EditProductCommand = new DelegateCommand(EditProduct, CanEditDeleteProduct).ObservesProperty(() => SelectedProduct);
            DeleteProductCommand = new DelegateCommand(DeleteProduct, CanEditDeleteProduct).ObservesProperty(() => SelectedProduct);
        }

        private void EditProduct()
        {
            var parameters = new NavigationParameters
            {
                { "Product", SelectedProduct }
            };

            _regionManager.RequestNavigate("ContentRegion", "ManipulateProduct", parameters);
        }

        private void AddProduct()
        {
            _regionManager.RequestNavigate("ContentRegion", "ManipulateProduct");
        }

        private void DeleteProduct()
        {
            _context.Products.Remove(SelectedProduct);
            _context.SaveChanges();
            Products.Remove(SelectedProduct);

            LoadStats();
        }

        private bool CanEditDeleteProduct()
        {
            return SelectedProduct != null;
        }

        private void LoadProducts()
        {
            Products = new ObservableCollection<Product>(
                _context.Products.AsNoTracking()
                    .OrderBy(p => p.Name)
                    .ToList());
        }

        private void LoadStats()
        {
            Task.Factory.StartNew(() =>
            {
                TotalAvailableQuantity = 0;
                TotalBuyingPrice = 0.00M;
                TotalSellingPrice = 0.00M;
                foreach (var product in Products)
                {
                    TotalAvailableQuantity += product.AvailableQuantity;
                    TotalBuyingPrice += product.AvailableQuantity * product.BuyingPrice;
                    TotalSellingPrice += product.AvailableQuantity * product.SellingPrice;
                }
            });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadProducts();
            LoadStats();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Products = null;
        }
    }
}
