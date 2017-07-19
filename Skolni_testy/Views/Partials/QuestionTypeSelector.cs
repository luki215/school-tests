using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;

namespace Skolni_testy.Views.Partials
{
    using t = Properties.Translations;
    class QuestionTypeSelector : BaseView
    {
        public QuestionTypeSelector(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var qTypes = Models.QuestionModel.QuestionTypes;
            var qCombo = new ComboBox();
            qCombo.Name = "QuestionTypeSelector";
            qCombo.Items.Add(t.QuestionsChoices);
            qCombo.Items.Add(t.QuestionsFreeAnswer);
            qCombo.SelectedIndex = Array.IndexOf(qTypes.Select(t=>t.Item1).ToArray(), (string)data["selected"]);
            qCombo.FlatStyle = FlatStyle.Flat;
            qCombo.SelectedValueChanged += (s, e) =>
            {
                formToRender.Controls.Clear();
                appContext.ViewManager.RenderView($"Questions.{ qTypes[qCombo.SelectedIndex].Item1 }", "New", null, formToRender);
            };
            formToRender.Controls.Add(qCombo);
        }
    }
}
