using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTableMobile.Models;
using TimeTableMobile.Services.Settings;
using TimeTableMobile.Services.TimeTable;
using TimeTableMobile.ViewModels.Base;
using TimeTableMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTableMobile.ViewModels
{
    public class TimetableViewModel : BaseViewModel
    {
        private string _campus;
        private string _classroom;
        private List<string> _dayofweek;
        private string _selectedDayOfWeek;
        private ITimeTableService TimeTableService { get; set; }
        

        //private string Url = "https://rntwebservice20190404104209.azurewebsites.net/api/Timetable";

        private string Url = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/Timetable";

        public TimetableViewModel()
        {
            TimeTableService = DependencyInjector.Get<ITimeTableService>();
            SetDayOfWeek();
        }

        public void SetDayOfWeek()
        {
            var weekList = new List<string>();
            weekList.Add("Mon");
            weekList.Add("Tue");
            weekList.Add("Wed");
            weekList.Add("Thu");
            weekList.Add("Fri");
            weekList.Add("Sat");
            weekList.Add("Sun");

            Dayofweek = weekList;
        }

        private ObservableCollection<TimeTable> _timetables;
        public ObservableCollection<TimeTable> Timetables
        {
            get { return _timetables; }
            set { SetProperty(ref _timetables, value); }
        }

        public string Campus
        {
            get { return _campus; }
            set { SetProperty(ref _campus, value); }
        }

        public string Classroom
        {
            get { return _classroom; }
            set { SetProperty(ref _classroom, value); }
        }

        public List<string> Dayofweek
        {
            get { return _dayofweek; }
            set { SetProperty(ref _dayofweek, value); }
        }

        public string SelectedDayOfWeek
        {
            get { return _selectedDayOfWeek; }
            set { SetProperty(ref _selectedDayOfWeek, value); }
        }

        public ICommand GetTimetableCommand => new Command(async () => await GetTimetable());

        private async Task GetTimetable()
        {
            if(Campus == "" || Campus == null)
            {
                await DialogService.ShowAlertAsync("Please enter campus", "Not Valid", "Close");
                
                return;
            }

            if (Classroom == "" || Classroom == null)
            {
                await DialogService.ShowAlertAsync("Please enter classroom", "Not Valid", "Close");
                return;
            }

            if (SelectedDayOfWeek == "" || SelectedDayOfWeek == null)
            {
                await DialogService.ShowAlertAsync("Please choose a day of week", "Not Valid", "Close");
                return;
            }

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            string destUrl = Url + "/" + Campus + "/" + Classroom + "/" + SelectedDayOfWeek;

            var returnValue = await TimeTableService.GetAllTimeTableAsync(destUrl, SettingsService.AuthAccessToken );

            Timetables = new ObservableCollection<TimeTable>(returnValue);
        }


        public ICommand ShowLecturerCommand => new Command<TimeTable>(async (item) => await OnShowLecturerCommand(item));
        private async Task OnShowLecturerCommand(TimeTable timetable)
        {
            string msg = "Campus" + timetable.LecturerId;


            //var viewModel = DependencyInjector.Resolve<LecturerViewModel>();
            //viewModel.lecturerId = timetable.lecturerId;

            await Navigation.PushModalAsync(new LecturerView());
        }


    }
}
