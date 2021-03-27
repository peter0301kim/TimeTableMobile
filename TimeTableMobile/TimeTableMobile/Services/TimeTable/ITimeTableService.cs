using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TimeTableMobile.Models;

namespace TimeTableMobile.Services.TimeTable
{
    public interface ITimeTableService
    {
        Task<List<Models.TimeTable>> GetAllTimeTableAsync(string timeTableApiUrl, string token);
    }
}
