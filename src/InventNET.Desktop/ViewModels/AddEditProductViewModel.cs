using AutoMapper;
using InventNET.Desktop.Data;
using InventNET.Desktop.Models;
using InventNET.Desktop.Models.UserModels;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventNET.Desktop.ViewModels
{
    public class AddEditProductViewModel : BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly ApplicationDbContext _context;

        private UMProduct _product;
        public UMProduct Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        private bool _isEditing;
        public bool IsEditing
        {
            get { return _isEditing; }
            set { SetProperty(ref _isEditing, value); }
        }

        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }

        public AddEditProductViewModel(IRegionManager regionManager, ApplicationDbContext context)
        {
            _regionManager = regionManager;
            _context = context;

            CancelCommand = new DelegateCommand(Cancel);
            SaveCommand = new DelegateCommand(Save);
        }

        private void Cancel()
        {
            _regionManager.RequestNavigate("ContentRegion", "Products");
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            Product = null;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var product = navigationContext.Parameters.FirstOrDefault(p => p.Key.Equals("Product"));
            var castProduct = product.Value as Product ?? new Product();
            if (product.Value is Product)
                IsEditing = true;

            Product = Mapper.Map<UMProduct>(castProduct);
        }

        private void Save()
        {
            var product = Mapper.Map<Product>(Product);

            if (IsEditing)
            {
                _context.Entry<Product>(product).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }

            _context.Entry<Product>(product).State = EntityState.Detached;
            _regionManager.RequestNavigate("ContentRegion", "Products");
        }
    }
}
