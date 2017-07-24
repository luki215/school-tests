using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;
using Skolni_testy.Models;
using Shaolinq;

namespace Skolni_testy.Controllers
{
    class StudentsController : BaseController
    {
        public StudentsController(SkolniTestyAppContext appContext) : base(appContext)
        {
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Create": Create(parameters); break;
                case "Delete": Delete(parameters); break;
                default: throw new NoSuchActionInController(action, "Students");
            }
        }

        private void Delete(Dictionary<string, object> parameters)
        {
            using (var scope = new DataAccessScope())
            {
                var student = (StudentModel)parameters["student"];
                student.Delete();
                scope.Complete();
            }
            appContext.Router.SwitchTo("Classes", "Show", new Dictionary<string, object> { { "class", parameters["class"] } });
        }

        private void Create(Dictionary<string, object> parameters)
        {
            var st_class = (ClassModel)parameters["class"];
            var student_name = (string)parameters["student_name"];

            using (var scope = new DataAccessScope())
            {
                var new_st = appContext.DB.Students.Create();
                new_st.Name = student_name;
                new_st.Class = st_class;

                scope.Complete();
            }

            appContext.Router.SwitchTo("Classes", "Show", new Dictionary<string, object> { { "class", st_class } });

        }
    }
}
