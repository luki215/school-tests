using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;
using System.Windows.Forms;

namespace Skolni_testy.Controllers
{
    class QuestionsController : BaseController
    {
        public QuestionsController(SkolniTestyAppContext appContext) : base(appContext)
        {
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Process": Process(parameters); break;
                default: throw new NoSuchActionInController(action, "QuestionsController");
            }
        }

        private void Process(Dictionary<string, object> parameters)
        {
            var tabs = (TabControl)parameters["questionsTabs"];


            var questions = from TabPage tab in tabs.TabPages select (Tab: tab, Combo: tab.Controls.OfType<ComboBox>().Where(t=> t.Name=="QuestionTypeSelector").FirstOrDefault());

            foreach(var q in questions)
            {
                var q_type = Models.QuestionModel.QuestionTypes.FirstOrDefault(t => t.Translation == (string)q.Combo.SelectedItem).Name;
                appContext.Router.SwitchTo($"QuestionTypes.{q_type}", "Process", new Dictionary<string, object> { { "questionTab", q.Tab }, { "test", parameters["test"] }  });

            }
            appContext.Router.SwitchTo($"TeacherTests", "Index", new Dictionary<string, object> { { "infos", Properties.Translations.TestSuccessfullySaved } });
        }
    }
}
