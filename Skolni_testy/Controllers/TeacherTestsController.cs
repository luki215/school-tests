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
    class TeacherTestsController : BaseController
    {
        public TeacherTestsController(SkolniTestyAppContext appContext) : base(appContext){}

        private void Index(Dictionary<string, object> parameters)
        {
            var data = parameters ?? new Dictionary<string, object>();

           var lectures = appContext.DB.Lectures.OrderBy(c => c.Name).ToList();

           var lectures_tests = (from lec in lectures
                                    let tests = lec.Tests.ToList()
                                    select new { lec.Name, tests }).ToDictionary(t => t.Name, t => t.tests);

            data.Add("LecturesTests", lectures_tests);

            appContext.ViewManager.RenderView("TeacherTests", "Index", data);
        }


        private void Edit(Dictionary<string, object> parameters)
        {
            var test_id = (Guid)parameters["id"];
            var test = appContext.DB.Tests.FirstOrDefault(t => t.Id == test_id);


            var test_questions = from q in appContext.DB.Questions where q.Test == test orderby q.Order select q;

            var data = new Dictionary<string, object>() {   { "questions", test_questions.ToList() },
                                                            { "test", test } };


            appContext.ViewManager.RenderView("TeacherTests", "Edit", data);
        }


        private void Update(Dictionary<string, object> parameters)
        {
            string test_new_name = (string)parameters["testName"];
            
            var test = (TestModel)parameters["test"];

            using (var scope = new DataAccessScope())
            {
                var test_to_update = appContext.DB.Tests.GetReference(new { Id = (Guid)test.Id });
                test_to_update.Name = test_new_name;
                scope.Complete();
            }


            appContext.Router.SwitchTo("Questions", "Process", parameters);
        }

        private void New(Dictionary<string, object> parameters)
        {
            var lecture = appContext.DB.Lectures.GetByPrimaryKeyOrDefault((string)parameters["lecture"]);
            appContext.ViewManager.RenderView("TeacherTests", "New", new Dictionary<string, object>() {   { "lecture", lecture }});
        }
        private void Create(Dictionary<string, object> parameters)
        {
            string test_new_name = (string)parameters["testName"];

            var lecture = (LectureModel)parameters["lecture"];
            
            using (var scope = new DataAccessScope())
            {
                    var new_test = appContext.DB.Tests.Create();
                    new_test.Name = test_new_name;
                    new_test.Lecture = lecture;
                    scope.Complete();

                    parameters["test"] = new_test;
                    appContext.Router.SwitchTo("Questions", "Process", parameters);
            }
       
        }

        private void Show(Dictionary<string, object> parameters)
        {

            var launchedTests = (from instance in appContext.DB.ClassTestInstances
                                 where instance.Test == (TestModel)parameters["test"]
                                 orderby instance.LaunchedAt
                                 select instance).ToList();
            appContext.ViewManager.RenderView("TeacherTests", "Show", new Dictionary<string, object> { { "launchedTests", launchedTests }, {"test", parameters["test"] } });
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Index": Index(parameters); break;
                case "Update": Update(parameters); break;
                case "Edit": Edit(parameters); break;
                case "New": New(parameters); break;
                case "Create": Create(parameters); break;
                case "Show": Show(parameters); break;
                default: throw new NoSuchActionInController(action, "TeacherTests");
            }
        }

    }
}
