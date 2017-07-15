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

        public void Index(Dictionary<string, object> parameters)
        {
            var data = parameters ?? new Dictionary<string, object>();

            var classes = appContext.DB.Classes.OrderBy(c=>c.Nazev).ToList();

            var classes_students = (from cl in classes
                                    let students = cl.Students.Select(c => c.Name).ToList()
                                    select new { cl, students }).ToDictionary(t => t.cl.Nazev, t => t.students);


            data.Add("classes_students", classes_students);

            appContext.ViewManager.RenderView("MainScreen", "Index", data);
        }

        public void TeacherLogin(Dictionary<string, object> parameters)
        {
            string username = (string)parameters["username"],
                    password = (string)parameters["password"];
            var admin = appContext.DB.Admins.FirstOrDefault(u => u.Login == username);
            if (admin == null || !admin.CheckPassword(password))
                Index(new Dictionary<string, object> { { "errors", Properties.Translations.UsernameOrPasswordInvalid } });
            else
                appContext.Router.SwitchTo("TeacherTests", "Index", null);


        }


        override public void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Index": Index(parameters); break;
                case "TeacherLogin": TeacherLogin(parameters); break;
                default: throw new NoSuchActionInController(action, "MainScreenController");
            }
        }

    }
}
