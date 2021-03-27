using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableMobile.Services.LecturerTimeTable
{
    public interface ILecturerTimeTableService
    {
        Task<List<Models.TimeTable>> GetAllLecturerTimeTableAsync(string timeTableApiUrl, string token);
    }
}
