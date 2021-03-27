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
	public partial class LecturerTimetableView : ContentPage
	{
		LecturerTimetableViewModel viewModel;
		public LecturerTimetableView ()
		{
			InitializeComponent ();

			BindingContext = viewModel = new LecturerTimetableViewModel();
			viewModel.Navigation = Navigation;
        }
	}
}