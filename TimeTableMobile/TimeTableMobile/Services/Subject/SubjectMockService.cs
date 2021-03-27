using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTableMobile.Services.Subject
{
    public class SubjectMockService : ISubjectService
    {

        public SubjectMockService()
        {

        }

        private ObservableCollection<Models.Subject> MockSubjects = new ObservableCollection<Models.Subject>()
        {
            new Models.Subject { subjectId = "5C#W", subjectName = "C# for Web Development" },
            new Models.Subject { subjectId = "5DD", subjectName = "Database Design" },
            new Models.Subject { subjectId = "5EWD", subjectName = "E-Commerce Website Development" },
            new Models.Subject { subjectId = "5JAW", subjectName = "Java for Web - Basics" },
            new Models.Subject { subjectId = "5TSD", subjectName = "Team Software Development" }
        };

        public async Task<ObservableCollection<Models.Subject>> GetAllSubjectsAsync(string sId, string token)
        {
            await Task.Delay(10);

            if (!string.IsNullOrEmpty(token))
            {
                if (sId == "null")
                {
                    return MockSubjects;
                }
                else
                {
                    var tmpS = MockSubjects.Where(s => s.subjectId.Contains(sId));
                    ObservableCollection<Models.Subject> subjects = new ObservableCollection<Models.Subject>(tmpS);
                    return subjects;
                }
            }
            else
                return new ObservableCollection <Models.Subject>();
        }




    }
}
