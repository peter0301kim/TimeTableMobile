using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTableMobile.Services.Settings;
using TimeTableMobile.ViewModels.Base;
using TimeTableMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTableMobile.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel()
        {
        }

        public ICommand SettingsCommand => new Command(async () => await SettingsAsync());

        private async Task SettingsAsync()
        {

        }

        public ICommand LoginCommand => new Command(async () => await OnLoginCommand());
        private async Task OnLoginCommand()
        {
            await Navigation.PushAsync(new LoginView());

        }

        public ICommand LogoutCommand => new Command(async () => await OnLogoutCommand());
        private async Task OnLogoutCommand()
        {
            SettingsService.AuthAccessToken = "";

        }

        public ICommand SubjectCommand => new Command(async () => await OnSubjectCommand());
        private async Task OnSubjectCommand()
        {
            if(SettingsService.AuthAccessToken == "")
            {
                await OnLoginCommand();
            }
            else
            {
                await Navigation.PushAsync(new SubjectView());
            }

        }

        public ICommand TimetableCommand => new Command(async () => await OnTimetableCommand());
        private async Task OnTimetableCommand()
        {
            if (SettingsService.AuthAccessToken == "")
            {
                await OnLoginCommand();
            }
            else
            {
                await Navigation.PushAsync(new TimetableView());
            }
        }

        public ICommand LecturerTimetableCommand => new Command(async () => await OnLecturerTimetableCommand());
        private async Task OnLecturerTimetableCommand()
        {
            if (SettingsService.AuthAccessToken == "")
            {
                await OnLoginCommand();
            }
            else
            {
                await Navigation.PushAsync(new LecturerTimetableView());
            }
        }

        public ICommand CourseCommand => new Command(async () => await OnCourseCommand());
        private async Task OnCourseCommand()
        {
            string uri = "https://www.tafesa.edu.au/courses";
            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.AliceBlue,
                PreferredControlColor = Color.Violet
            });

        }

        public ICommand LocationCommand => new Command(async () => await OnLocationCommand());
        private async Task OnLocationCommand()
        {
            string uri = "https://www.tafesa.edu.au/locations";

            await Browser.OpenAsync(uri, new BrowserLaunchOptions
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
                PreferredToolbarColor = Color.AliceBlue,
                PreferredControlColor = Color.Violet
            });
        }

    }







}
