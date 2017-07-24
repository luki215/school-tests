using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;
using Shaolinq;
using Skolni_testy.Models;

namespace Skolni_testy.Controllers
{
    class ClassesController : BaseController
    {
        public ClassesController(SkolniTestyAppContext appContext) : base(appContext)
        {
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Index": Index(); break;
                case "Create": Create(parameters); break;
                case "Show": Show(parameters); break;
                case "Delete": Delete(parameters); break;
                default: throw new NoSuchActionInController(action, "Classes");

            }
        }

        private void Show(Dictionary<string, object> parameters)
        {
            var class_ = (ClassModel) parameters["class"];
            var students = class_.Students;

            appContext.ViewManager.RenderView("Classes", "Show", new Dictionary<string, object> { {"class", class_ }, { "students", students } });
        }

        private void Create(Dictionary<string, object> parameters)
        {
            var class_name = (string)parameters["class_name"];

            using (var scope = new DataAccessScope())
            {
                var new_class = appContext.DB.Classes.Create();
                new_class.Nazev = class_name;

                scope.Complete();
            }

            appContext.Router.SwitchTo("Classes", "Index", null);

        }
        private void Delete(Dictionary<string, object> parameters)
        {

            using (var scope = new DataAccessScope())
            {
                var class_ = (ClassModel)parameters["class"];
                class_.Delete();
                scope.Complete();
            }

            appContext.Router.SwitchTo("Classes", "Index", null);

        }

        private void Index()
        {
            var classes = appContext.DB.Classes.OrderBy(t=>t.Nazev);

            appContext.ViewManager.RenderView("Classes", "Index", new Dictionary<string, object> { { "classes", classes } });
        }
    }
}
