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
                default: throw new NoSuchActionInController(action, "TeacherTests");
            }
        }

        private void Stop(Dictionary<string, object> parameters)
        {
            var instance = (TestInstanceModel)parameters["testInstance"];
            using (var scope = new DataAccessScope())
            {
                var instance_f = appContext.DB.TestInstances.GetByPrimaryKey(instance.Id);
                instance_f.Active = false;
                scope.CompleteAsync();
            }
            appContext.Router.SwitchTo("TeacherTests", "Show", new Dictionary<string, object> { { "test", instance.Test } });
        }

        private void Results(Dictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        private void Create(Dictionary<string, object> parameters)
        {
            var test = (TestModel)parameters["test"];
            var tested_class = (ClassModel)parameters["class"];

            using (var scope = new DataAccessScope())
            {
                var testInstance = appContext.DB.TestInstances.Create();

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
