using TimeTableMobile.Services.Dialog;
using TimeTableMobile.Services.Lecturer;
using TimeTableMobile.Services.LecturerTimeTable;
using TimeTableMobile.Services.LogIn;
using TimeTableMobile.Services.Settings;
using TimeTableMobile.Services.Subject;
using TimeTableMobile.Services.TimeTable;
using Xamarin.Forms;


namespace TimeTableMobile.ViewModels.Base
{
    public static class DependencyInjector
    {
        static DependencyInjector()
        {
        }

        public static void RegisterDependencies()
        {
            DependencyService.Register<IDialogService, DialogService>();
            DependencyService.Register<ISettingsService, SettingsService>();
        }


        public static void UpdateDependencies(bool useMockServices)
        {
            
            if (useMockServices)
            {
                DependencyService.Register<ILogInService, LogInMockService>();
                DependencyService.Register<ISubjectService, SubjectMockService>();
                DependencyService.Register<ITimeTableService, TimeTableMockService>();
                DependencyService.Register<ILecturerService, LecturerMockService>();
                DependencyService.Register<ILecturerTimeTableService, LecturerTimeTableMockService>();

            }
            else
            {
                DependencyService.Register<ILogInService, LogInService>();
                DependencyService.Register<ISubjectService, SubjectService>();
                DependencyService.Register<ITimeTableService, TimeTableService>();
                DependencyService.Register<ILecturerService, LecturerService>();
                DependencyService.Register<ILecturerTimeTableService, LecturerTimeTableMockService>();
            }
            
        }

        public static T Get<T>() where T : class
        {
            
            return DependencyService.Get<T>();

        }
    }
}
