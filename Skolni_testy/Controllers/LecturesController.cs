using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;
using Shaolinq;

namespace Skolni_testy.Controllers
{
    class LecturesController : BaseController
    {
        public LecturesController(SkolniTestyAppContext appContext) : base(appContext)
        {
        }

        public void  New()
        {
            appContext.ViewManager.RenderView("Lectures", "New", null);
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "New": New(); break;
                case "Create": Create(parameters); break;
                default: throw new NoSuchActionInController(action, "Lectures");
            }
        }

        public void Create(Dictionary<string, object> parameters)
        {
            var name = (string)parameters["name"];
            var data = new Dictionary<string, object>();
            try
            {
                using (var scope = new DataAccessScope())
                {
                    var new_lect = appContext.DB.Lectures.Create();
                    new_lect.Name = name;

                    scope.Complete();
                }
            } catch{
                data.Add("errors", Properties.Translations.LectureAlreadyExists);
                
            }


            appContext.Router.SwitchTo("TeacherTests", "Index", data);
        }
    }
}
