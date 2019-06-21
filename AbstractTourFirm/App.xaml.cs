using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AbstractTourFirm_ServiceDAL.Interfaces;
using AbstractTourFirm_ServiceDataBase;
using AbstractTourFirm_ServiceDataBase.Implements;
using Unity;

namespace AbstractTourFirm
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<DbContext, TourFirmDbContext>();
            container.RegisterType<IMainService, MainServiceDB>();
            container.RegisterType<ITourService, TourServiceDB>();
            container.RegisterType<ITravelService, TravelServiceDB>();
            container.RegisterType<ICustomerService, CustomerServiceDB>();
            var mainWindow = container.Resolve<SingUpWindow>();
            Application.Current.MainWindow = mainWindow;
            Application.Current.MainWindow.Show();
        }
    }
}
