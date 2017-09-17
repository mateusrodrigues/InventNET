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
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly ApplicationDbContext _context;

        private string _version;
        public string Version
        {
            get { return _version; }
            set { SetProperty(ref _version, value); }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; set; }

        public MainWindowViewModel(
            IRegionManager regionManager,
            ApplicationDbContext context)
        {
            _regionManager = regionManager;
            _context = context;

            Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            NavigateCommand = new DelegateCommand<string>(Navigate);

            CheckMigrations();
            ConfigureMapper();
        }

        private void CheckMigrations()
        {
            IsLoading = true;
            Task.Factory.StartNew(() =>
            {
                var pendingMigrations = _context.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                {
                    _context.Database.Migrate();
                }
            }).ContinueWith((t) => IsLoading = false);
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Product, UMProduct>();
                config.CreateMap<UMProduct, Product>();
            });
        }

        private void Navigate(string uri)
        {
            _regionManager.RequestNavigate("ContentRegion", uri);
        }
    }
}
