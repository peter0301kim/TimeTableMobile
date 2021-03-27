using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TimeTableMobile.Models;
using TimeTableMobile.Services.Settings;
using TimeTableMobile.Services.Subject;
using TimeTableMobile.ViewModels.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TimeTableMobile.ViewModels
{
    public class SubjectViewModel : BaseViewModel
    {
        ISubjectService SubjectService { get; set; }

        private string _subjectId;
        public string SubjectId
        {
            get { return _subjectId; }
            set { SetProperty(ref _subjectId, value); }
        }

        private ObservableCollection<Subject> _subjects;
        public ObservableCollection<Subject> Subjects
        {
            get { return _subjects; }
            set { SetProperty(ref _subjects, value); }
        }

        public SubjectViewModel()
        {
            SubjectService = DependencyInjector.Get<ISubjectService>();

        }

        public ICommand GetSubjectCommand => new Command(async () => await GetSubject());

        private async Task GetSubject()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                await DialogService.ShowAlertAsync("Not Connected to Internet", "Connection Error", "Close");

                return;
                // Connection to internet is available
            }

            string tmpSubjectId = "null";

            if(SubjectId == null || SubjectId == "")
            {
                tmpSubjectId = "null";
            }
            else
            {
                tmpSubjectId = SubjectId;
            }

            try
            {
                Subjects = await SubjectService.GetAllSubjectsAsync(tmpSubjectId, SettingsService.AuthAccessToken);
            }
            catch (Exception ex)
            {
                string strErr = "Error " + ex.ToString();


                if (strErr.IndexOf("Unauthorized") > 0)
                {
                    await DialogService.ShowAlertAsync("Please, Log in again.", "Unauthorized", "Close");
                    SettingsService.AuthAccessToken = "";
                }
                else
                {
                    await DialogService.ShowAlertAsync(strErr, "Error", "Close");
                }

                await Navigation.PopAsync();
            }

            

            /*
            HttpClient client = new HttpClient();

            try
            {
                string token = _settingsService.AuthAccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(destUrl);
                string content = result.ToString();
                List<Subject> results = JsonConvert.DeserializeObject<List<Subject>>(content);

                subjects = new ObservableCollection<Subject>(results);

                client.Dispose();
            }
            catch (Exception ex)
            {
                string checkResult = "Error " + ex.ToString();
                await DialogService.ShowAlertAsync(checkResult, "Error", "Close");

                if (checkResult.IndexOf("Unauthorized") > 0)
                {
                    _settingsService.AuthAccessToken = "";
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
