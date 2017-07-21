using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using Newtonsoft.Json;
using Skolni_testy.Models.QuestionTypes;
using MaterialSkin.Controls;
using Skolni_testy.Models.QuestionTypes;
using Skolni_testy.Models;

namespace Skolni_testy.Views.Questions.Choices
{
    using t = Properties.Translations;
    class Show : BaseView
    {
        public Show(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var qData = (string)data["questionData"] ?? "";
            var q = JsonConvert.DeserializeObject<ChoiceModel>(qData) ?? new ChoiceModel();
            var f = formToRender;
            var tabControl = (TabControl)f.Parent;

            var qTextLabel = new MaterialLabel();
            qTextLabel.Text = t.QuestionText;
            qTextLabel.Location = new System.Drawing.Point(30, 50);
            f.Controls.Add(qTextLabel);


            var qText = new Label();
            qText.AutoSize = true;
            qText.Size = new System.Drawing.Size(300, 60);
            qText.MaximumSize = new System.Drawing.Size(300, 0);
            qText.Location = new System.Drawing.Point(30, 80);
            f.Controls.Add(qText);
            qText.Text = q.QuestionText;


            var answersPanel = new Panel();
            answersPanel.Name = "AnswersPanel";
            answersPanel.Location = new System.Drawing.Point(500, 50);
            answersPanel.Size = new System.Drawing.Size(300, 200);
            answersPanel.AutoScroll = true;
            f.Controls.Add(answersPanel);


            int i = 0;
            foreach (var a in (q.answers ?? new List<ChoiceModel.Answer>()))
            {
                var y = i;
                var ans = new MaterialCheckBox();
                ans.Text = a.Text;
                ans.Location = new System.Drawing.Point(0, i * 30);
                ans.Size = new System.Drawing.Size(200, 30);
                ans.CheckedChanged += (s, e) => { if (f.Text[f.Text.Length - 1] != '√') f.Text += "√"; };
                answersPanel.Controls.Add(ans);
                ans.Tag = a;
                i++;
            }

        }
        
    }
}
