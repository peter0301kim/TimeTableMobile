using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTableMobile.Models;
using TimeTableMobile.Services.Lecturer;
using TimeTableMobile.Services.Settings;
using TimeTableMobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTableMobile.ViewModels
{
    public class LecturerViewModel : BaseViewModel
    {
        // private string Url = "https://rntwebservice20190404104209.azurewebsites.net/api/lecturer";

        private string Url = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/lecturer";

        Lecturer _lecturer = new Lecturer();
        ILecturerService LecturerService;

        private ObservableCollection<Lecturer> _lecturers;
        public ObservableCollection<Lecturer> Lecturers
        {
            get { return _lecturers; }
            set { SetProperty(ref _lecturers, value); }
        }


        public LecturerViewModel()
        {
            LecturerService = DependencyInjector.Get<ILecturerService>();

            _lecturer = new Lecturer();

            Task.Run(async () => { await GetLecturer(); });
        }

        private string _lecturerId;
        public string LecturerId
        {
            get { return _lecturerId; }
            set { SetProperty(ref _lecturerId, value); }
        }

        private string _givenName;
        public string GivenName
        {
            get { return _givenName; }
            set { SetProperty(ref _givenName, value); }

        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }

        }

        private string _emailAddress;
        public string EmailAddress
        {
            get { return EmailAddress; }
            set { SetProperty(ref _emailAddress, value); }
        }

        private async Task GetLecturer()
        {
            //await DialogService.ShowAlertAsync(lecturerId, "Info", "Close");
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }


            string strUrl = Url + "/" + LecturerId;

            List<Models.Lecturer> returnValue = await LecturerService.GetAllLecturerAsync(Url, SettingsService.AuthAccessToken);

            Lecturers = new ObservableCollection<Lecturer>(returnValue);
        }

        public ICommand CloseCommand => new Command(async () => await OnClose());

        private async Task OnClose()
        {
            await Navigation.PopModalAsync();
        }
    }
}
