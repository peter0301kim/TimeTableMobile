﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTableMobile.Models;
using TimeTableMobile.Services.LecturerTimeTable;
using TimeTableMobile.Services.Settings;
using TimeTableMobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTableMobile.ViewModels
{
    public class LecturerTimetableViewModel : BaseViewModel
    {
        private string _lecturerName;

        private bool _isMon;
        private bool _isTue;
        private bool _isWed;
        private bool _isThu;
        private bool _isFri;

        

        private string Url = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/LecturerTimetable";
        ILecturerTimeTableService LecturerTimeTableService;

        public LecturerTimetableViewModel()
        {
            LecturerTimeTableService = DependencyInjector.Get<ILecturerTimeTableService>();
            IsMon = true;
        }

        private ObservableCollection<TimeTable> _timetables;
        public ObservableCollection<TimeTable> Timetables
        {
            get { return _timetables; }
            set { SetProperty(ref _timetables, value); }
        }

        public string LecturerName
        {
            get { return _lecturerName; }
            set { SetProperty(ref _lecturerName, value); }
        }

        public bool IsMon
        {
            get { return _isMon; }
            set
            {
                if(!value && !IsTue && !IsWed && !IsThu && !IsFri)
                {
                    _isMon = true;
                    SetProperty(ref _isMon, value);
                    return;
                }
                _isMon = value;
                SetProperty(ref _isMon, value);
            }
        }

        public bool IsTue
        {
            get { return _isTue; }
            set
            {
                if (!value && !IsMon && !IsWed && !IsThu && !IsFri)
                {
                    _isTue = true;
                    SetProperty(ref _isTue, value);
                    return;
                }
                _isTue = value;
                SetProperty(ref _isTue, value);
            }
        }

        public bool IsWed
        {
            get { return _isWed; }
            set
            {
                if (!value && !IsMon && !IsTue && !IsThu && !IsFri)
                {
                    _isWed = true;
                    SetProperty(ref _isWed, value);
                    return;
                }
                _isWed = value;
                SetProperty(ref _isWed, value);
            }
        }

        public bool IsThu
        {
            get { return _isThu; }
            set
            {
                if (!value && !IsMon && !IsTue && !IsWed && !IsFri)
                {
                    _isThu = true;
                    SetProperty(ref _isThu, value);
                    return;
                }
                _isThu = value;
                SetProperty(ref _isThu, value);
            }
        }

        public bool IsFri
        {
            get { return _isFri; }
            set
            {
                if (!value && !IsMon && !IsTue && !IsWed && !IsThu)
                {
                    _isFri = true;
                    SetProperty(ref _isFri, value);
                    return;
                }
                _isFri = value;
                SetProperty(ref _isFri, value);
            }
        }

        public ICommand GetLecturerTimetableCommand => new Command(async () => await GetLecturerTimetable());

        private async Task GetLecturerTimetable()
        {
            if(LecturerName == "" || LecturerName == null)
            {
                await DialogService.ShowAlertAsync("Please enter lecturer's name", "Info", "Close");
                return;
            }

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            string dayOfWeeks = "";


            if (IsMon) { dayOfWeeks += "1,"; }
            if (IsTue) { dayOfWeeks += "2,"; }
            if (IsWed) { dayOfWeeks += "3,"; }
            if (IsThu) { dayOfWeeks += "4,"; }
            if (IsFri) { dayOfWeeks += "5,"; }

            dayOfWeeks = dayOfWeeks.Remove(dayOfWeeks.Length - 1);


            string destUrl = Url + "/" + LecturerName + "/" + dayOfWeeks;

            var returnValue = await LecturerTimeTableService.GetAllLecturerTimeTableAsync(destUrl, SettingsService.AuthAccessToken);
            
            Timetables = new ObservableCollection<TimeTable>(returnValue);


            /*
            HttpClient client = new HttpClient();
            try
            {

                string token = SettingsService.AuthAccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(destUrl);
                string content = result.ToString();
                List<TimeTable> timeTables = JsonConvert.DeserializeObject<List<TimeTable>>(content);

                Timetables = new ObservableCollection<TimeTable>(timeTables);

                client.Dispose();

            }
            catch (Exception ex)
            {
                string checkResult = "Error " + ex.ToString();
                await DialogService.ShowAlertAsync(checkResult, "Error", "Close");

                if (checkResult.IndexOf("Unauthorized") > 0)
                {
                    SettingsService.AuthAccessToken = "";
                    client.Dispose();

                    await Navigation.PopAsync();
                    return;

                }
                client.Dispose();
            }
            */
        }

    }
}
