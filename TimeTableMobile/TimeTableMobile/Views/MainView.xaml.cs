using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableMobile.ViewModels;
using Xamarin.Forms;

namespace TimeTableMobile.Views
{
    public partial class MainView : ContentPage
    {
        MainViewModel viewModel;
        public MainView()
        {
            InitializeComponent();
            BindingContext = viewModel = new MainViewModel();
            viewModel.Navigation = Navigation;
        }
    }
}
