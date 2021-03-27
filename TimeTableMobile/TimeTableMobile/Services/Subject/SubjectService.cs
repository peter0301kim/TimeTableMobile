using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TimeTableMobile.Services.Subject
{
    public class SubjectService : ISubjectService
    {
        //private string Url = "https://rntwebservice20190404104209.azurewebsites.net/api/subject";
        //private string Url = "https://rntwebservicefortafesa20190611083115.azurewebsites.net/api/subject";
        string ApiSubjectUrl { get; set; }
        public SubjectService()
        {
            ApiSubjectUrl
                = GlobalSettings.Instance.TimeTableApiBaseUrl
                + GlobalSettings.Instance.TimeTableApiSubjectEndPoint;
        }

        public async Task<ObservableCollection<TimeTableMobile.Models.Subject>> GetAllSubjectsAsync(string token)
        {
            HttpClient client = new HttpClient();



            string destUrl = ApiSubjectUrl + "/null";

            var subjects = new ObservableCollection<TimeTableMobile.Models.Subject>();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(destUrl); // Call WebAPI

                string content = result.ToString();

                List<TimeTableMobile.Models.Subject> results = JsonConvert.DeserializeObject<List<Models.Subject>>(content);

                subjects = new ObservableCollection<TimeTableMobile.Models.Subject>(results);

                client.Dispose();
            }
            catch (Exception ex)
            {
                string strErr = ex.ToString();
                client.Dispose();

                throw new Exception(strErr);
            }

            return subjects;
        }

        public async Task<ObservableCollection<Models.Subject>> GetAllSubjectsAsync(string subjectId, string token)
        {
            HttpClient client = new HttpClient();

            string destUrl = ApiSubjectUrl + "/" + subjectId;

            var subjects = new ObservableCollection<Models.Subject>();

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetStringAsync(destUrl); // Call WebAPI

                string content = result.ToString();

                List<Models.Subject> results = JsonConvert.DeserializeObject<List<Models.Subject>>(content);

                subjects = new ObservableCollection<Models.Subject>(results);

                client.Dispose();
            }
            catch (Exception ex)
            {
                string strErr = ex.ToString();
                client.Dispose();

                throw new Exception(strErr);
            }

            return subjects;
        }


    }
}
