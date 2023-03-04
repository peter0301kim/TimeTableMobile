using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TimeTableMobile.Services.Settings;

namespace TimeTableMobile.Services.LecturerTimeTable 
{
    public class LecturerTimeTableService : ILecturerTimeTableService
    {
        public async Task<List<Models.TimeTable>> GetAllLecturerTimeTableAsync(string destUrl, string token)
        {
            List<Models.TimeTable> timeTables;
            try
            {
                var client = new RestClient(destUrl);
                var request = new RestRequest(destUrl, Method.Get);

                request.AddHeader("authorization", "Bearer " + token);
                request.AddHeader("accept", "application/json");

                RestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(response.Content.ToString());
                }
                string responseContent = response.Content;
                timeTables = JsonConvert.DeserializeObject<List<Models.TimeTable>>(responseContent);

            }
            catch (Exception)
            {
                timeTables = null;
            }
            return timeTables;
           
        }
    }
}
