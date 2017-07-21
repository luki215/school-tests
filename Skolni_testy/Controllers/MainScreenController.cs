using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;

using MaterialSkin.Controls;
using Shaolinq;
using Skolni_testy.Models;

namespace Skolni_testy.Controllers
{
    class MainScreenController : BaseController
    {
        public MainScreenController(SkolniTestyAppContext appContext) : base(appContext){}




        override public void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Index": Index(parameters); break;
                case "TeacherLogin": TeacherLogin(parameters); break;
                case "SetStudent": SetStudent(parameters); break;
                default: throw new NoSuchActionInController(action, "MainScreenController");
            }
        }
        private void Index(Dictionary<string, object> parameters)
        {
            var data = parameters ?? new Dictionary<string, object>();

            var classes = appContext.DB.Classes.OrderBy(c => c.Nazev).ToList();

            var classes_students = (from cl in classes
                                    let students = cl.Students.ToList()
                                    select new { cl, students }).ToDictionary(t => t.cl, t => t.students);


            data.Add("classes_students", classes_students);

            appContext.ViewManager.RenderView("MainScreen", "Index", data);
        }

        private void TeacherLogin(Dictionary<string, object> parameters)
        {
            string username = (string)parameters["username"],
                    password = (string)parameters["password"];
            var admin = appContext.DB.Admins.FirstOrDefault(u => u.Login == username);
            if (admin == null || !admin.CheckPassword(password))
                Index(new Dictionary<string, object> { { "errors", Properties.Translations.UsernameOrPasswordInvalid } });
            else
                appContext.Router.SwitchTo("TeacherTests", "Index", null);


        }

        private void SetStudent(Dictionary<string, object> parameters)
        {
            appContext.Session.Add("loggedStudent", (Student: (StudentModel)parameters["student"], Class: (ClassModel)parameters["class"]));
            appContext.Router.SwitchTo("StudentTests", "Index", null);

        }
    }
}
