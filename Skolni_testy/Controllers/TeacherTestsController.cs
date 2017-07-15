using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;

namespace Skolni_testy.Controllers
{
    class TeacherTestsController : BaseController
    {
        public TeacherTestsController(SkolniTestyAppContext appContext) : base(appContext){}

        public void Index(Dictionary<string, object> parameters)
        {
            var data = new Dictionary<string, object>();

           var lectures = appContext.DB.Lectures.OrderBy(c => c.Name).ToList();

           var lectures_tests = (from lec in lectures
                                    let tests = lec.Tests.ToList()
                                    select new { lec.Name, tests }).ToDictionary(t => t.Name, t => t.tests);

            data.Add("LecturesTests", lectures_tests);

            appContext.ViewManager.RenderView("TeacherTests", "Index", data);
        }
        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Index": Index(parameters); break;
                default: throw new NoSuchActionInController(action, "TeacherTests");
            }
        }
    }
}
