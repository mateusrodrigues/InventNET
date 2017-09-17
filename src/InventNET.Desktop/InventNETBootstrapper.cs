using InventNET.Desktop.Data;
using InventNET.Desktop.Views;
using Microsoft.Practices.Unity;
using Prism;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventNET.Desktop
{
    public class InventNETBootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Register Database context
            Container.RegisterType<object, ApplicationDbContext>();

            Container.RegisterTypeForNavigation<ProductsView>("Products");
            Container.RegisterTypeForNavigation<AddEditProductView>("ManipulateProduct");
        }
    }
}
