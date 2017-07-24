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
    class TestInstancesController : BaseController
    {
        public TestInstancesController(SkolniTestyAppContext appContext) : base(appContext)
        {
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "New": New(parameters); break;
                case "Create": Create(parameters); break;
                case "Stop": Stop(parameters); break;
                case "Results": Results(parameters); break;
                case "ResultsDetail": ResultsDetail(parameters); break;
                default: throw new NoSuchActionInController(action, "TestsInstances");
            }
        }

        private void ResultsDetail(Dictionary<string, object> parameters)
        {
            var student_test = (StudentTestInstanceModel)parameters["test"];
            int OK, Wrong, DontKnow;
            OK = Wrong = DontKnow = 0;

            foreach (var ans in student_test.Answers)
            {
                switch (ans.Correct)
                {
                    case AnswerModel.AnswerStatus.OK:
                        OK++;
                        break;
                    case AnswerModel.AnswerStatus.Wrong:
                        Wrong++;
                        break;
                    case AnswerModel.AnswerStatus.DontKnow:
                        DontKnow++;
                        break;
                    default:
                        break;
                }
            }

            appContext.ViewManager.RenderView("TestInstances", "ResultsDetail", new Dictionary<string, object> {
                                                                                                            { "answers", student_test.Answers.OrderBy(t=>t.Question.Order)},
                                                                                                            { "answerStats", (OK, Wrong, DontKnow) }
            });
        }

        private void Stop(Dictionary<string, object> parameters)
        {
            var instance = (ClassTestInstanceModel)parameters["testInstance"];
            using (var scope = new DataAccessScope())
            {
                var instance_f = appContext.DB.ClassTestInstances.GetByPrimaryKey(instance.Id);
                instance_f.Active = false;
                scope.CompleteAsync();
            }
            appContext.Router.SwitchTo("TeacherTests", "Show", new Dictionary<string, object> { { "test", instance.Test } });
        }

        private void Results(Dictionary<string, object> parameters)
        {
            var test = (ClassTestInstanceModel)parameters["test"];

            var st_tests = test.StudentTestInstances;

            appContext.ViewManager.RenderView("TestInstances", "Results", new Dictionary<string, object> { { "st_tests", st_tests } });
        }

        private void Create(Dictionary<string, object> parameters)
        {
            var test = (TestModel)parameters["test"];
            var tested_class = (ClassModel)parameters["class"];

            using (var scope = new DataAccessScope())
            {
                var testInstance = appContext.DB.ClassTestInstances.Create();

                testInstance.Active = true;
                testInstance.Class = tested_class;
                testInstance.LaunchedAt = DateTime.Now;
                testInstance.Test = test;
                scope.Complete();
            }

            appContext.Router.SwitchTo("TeacherTests", "Show", new Dictionary<string, object> { { "test", test } });

        }

        private void New(Dictionary<string, object> parameters)
        {
            var classes = appContext.DB.Classes.OrderBy(t=>t.Nazev).ToList();
            appContext.ViewManager.RenderView("TestInstances", "New", new Dictionary<string, object> { { "classes", classes }, { "test", parameters["test"] } });
        }
    }
}
