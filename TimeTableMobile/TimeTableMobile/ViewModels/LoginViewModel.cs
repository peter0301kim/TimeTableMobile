using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTableMobile.Services.LogIn;
using TimeTableMobile.Services.Settings;
using TimeTableMobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTableMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private const string Url = "https://identityservice20190424115844.azurewebsites.net/api/RntAppUser/LogIn";

        
        private string _userId;
        private string _password;
        private ILogInService LogInService { get; set; }
        public LoginViewModel()
        {
            LogInService = DependencyInjector.Get<ILogInService>();
        }

        public string UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public override Task InitializeAsync(object navigationData)
        {
            /*
            if (navigationData is LogoutParameter)
            {
                var logoutParameter = (LogoutParameter)navigationData;

                if (logoutParameter.Logout)
                {
                    Logout();
                }
            }
            */
            return base.InitializeAsync(navigationData);
        }

        public ICommand LogInCommand => new Command(async () => await OnLogIn());

        private async Task OnLogIn()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            string token = await LogInService.CheckCredentials(UserId, Password);

            SettingsService.AuthAccessToken = token;

            if (string.IsNullOrEmpty(token))
            {
                await DialogService.ShowAlertAsync("CheckChredentials Error", "Error", "Close");
            }
            else
            {
                await Navigation.PopAsync();
            }
        }


        public ICommand CancelCommand => new Command(async () => await OnCancel());

        private async Task OnCancel()
        {
            await Navigation.PopAsync();
        }


        
        /*
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}
