using System;
using TimeTableMobile.Services;
using TimeTableMobile.ViewModels.Base;
using TimeTableMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableMobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            DependencyInjector.RegisterDependencies();
            DependencyInjector.UpdateDependencies(true);//true : Mock, 

            MainPage = new NavigationPage(new MainView());

            //MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
