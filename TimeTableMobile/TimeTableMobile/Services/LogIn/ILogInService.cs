using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableMobile.Services.LogIn
{
    public interface ILogInService
    {
        Task<string> CheckCredentials(string userId, string password);
    }
}
