using TimeTableMobile.Services.Settings;
using TimeTableMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTableMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubjectView : ContentPage
    {
        SubjectViewModel viewModel;
        public SubjectView()
        {
            InitializeComponent();

            BindingContext = viewModel = new SubjectViewModel();
            viewModel.Navigation = Navigation;
        }
    }
}