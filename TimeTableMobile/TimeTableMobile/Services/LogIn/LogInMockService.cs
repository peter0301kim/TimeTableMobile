﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableMobile.Services.LogIn
{
    public class LogInMockService : ILogInService
    {
        public async Task<string> CheckCredentials(string userId, string password)
        {
            await Task.Delay(10);

            string token = "MockToken-1234567890";
            
            return token;
        }
    }
}
