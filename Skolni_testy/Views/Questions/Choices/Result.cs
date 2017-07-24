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
using Skolni_testy.Models;

namespace Skolni_testy.Views.Questions.Choices
{
    using t = Properties.Translations;
    class Result : BaseView
    {
        public Result(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var answer = (AnswerModel)data["answer"];
            var usr_answers = JsonConvert.DeserializeObject<List<ChoiceModel.Answer>>(answer.Data);
            var qData = answer.Question.QuestionData;
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
            var ans_corret_ans_list = (q.answers).Zip(usr_answers, (corr, ans) =>  ( corr, ans)  );
            foreach (var (correct_ans, usr_ans) in ans_corret_ans_list)
            {
                var status_label = new MaterialLabel();
                if(correct_ans.Checked == usr_ans.Checked ) status_label.Text = "√";
                else status_label.Text = "X";
                status_label.Location = new System.Drawing.Point(0, i * 30);
                status_label.Width = 20;
                answersPanel.Controls.Add(status_label);

                var ans = new MaterialCheckBox();
                ans.Text = correct_ans.Text;
                ans.Checked = correct_ans.Checked;
                ans.Location = new System.Drawing.Point(20, i * 30);
                ans.Size = new System.Drawing.Size(200, 30);
                ans.Enabled = false;
                answersPanel.Controls.Add(ans);
                ans.Tag = correct_ans;
                i++;
            }

        }
    }
}
