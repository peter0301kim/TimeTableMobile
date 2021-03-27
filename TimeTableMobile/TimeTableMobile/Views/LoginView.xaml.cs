using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeTableMobile.Services.Settings;
using TimeTableMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		LoginViewModel viewModel;
		public LoginView()
		{
			InitializeComponent ();
			BindingContext = viewModel = new LoginViewModel();
			viewModel.Navigation = Navigation;
        }
    }
}