using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using MaterialSkin.Controls;
using Newtonsoft.Json;
using Skolni_testy.Models.QuestionTypes;

namespace Skolni_testy.Views.Questions.FreeAnswer
{
    using t = Properties.Translations;
    class Edit : BaseView
    {
        public Edit(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var qData = (string)data["questionData"] ?? "";
            var q = JsonConvert.DeserializeObject<FreeAnswerModel>(qData) ?? new FreeAnswerModel();
            var f = formToRender;
            appContext.ViewManager.RenderPartial("QuestionTypeSelector", new Dictionary<string, object> { { "selected", "FreeAnswer" } }, formToRender);

            var qTextLabel = new MaterialLabel();
            qTextLabel.Text = t.QuestionText;
            qTextLabel.Location = new System.Drawing.Point(30, 50);
            f.Controls.Add(qTextLabel);

            var qTextInput = new TextBox();
            qTextInput.Multiline = true;
            qTextInput.BorderStyle = BorderStyle.FixedSingle;
            qTextInput.Name = "QuestionText";
            qTextInput.Size = new System.Drawing.Size(300, 60);
            qTextInput.Location = new System.Drawing.Point(30, 80);
            qTextInput.Text = q.QuestionText;
            f.Controls.Add(qTextInput);
        }
    }
}
