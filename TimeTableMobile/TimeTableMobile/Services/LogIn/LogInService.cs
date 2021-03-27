using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableMobile.Services.LogIn
{
    public class LogInService : ILogInService
    {
        string ApiLogInUrl { get; set; }
        public LogInService()
        {
            ApiLogInUrl
                = GlobalSettings.Instance.TimeTableApiBaseUrl
                + GlobalSettings.Instance.TimeTableApiSubjectEndPoint;
        }
        public async Task<string> CheckCredentials(string userId, string password)
        {
            var client = new RestClient(ApiLogInUrl);
            var request = new RestRequest(Method.POST);

            string token = "";
            try
            {
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "Bearer " + accessToken);
                request.AddHeader("accept", "application/json");

                JObject inputData = new JObject();
                inputData.Add("UserName", userId);
                inputData.Add("Password", password);

                request.AddJsonBody(inputData);

                IRestResponse response = await client.ExecuteAsync(request);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(response.Content.ToString());
                }
                string responseContent = response.Content;
                
                var dic_token = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                token = dic_token["token"];


            }
            catch (Exception)
            {
                token = "";
            }

            return token;
        }
    }
}
