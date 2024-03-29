﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TimeTableMobile.Models;

namespace TimeTableMobile.Services.TimeTable
{
    public class TimeTableService : ITimeTableService
    {

        public async Task<List<Models.TimeTable>> GetAllTimeTableAsync(string timeTableApiUrl, string token)
        {
            List<Models.TimeTable> timeTables;
            try
            {
                var client = new RestClient(timeTableApiUrl);
                var request = new RestRequest(timeTableApiUrl, Method.Get);

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
            /*




                
            */
        }
    }
