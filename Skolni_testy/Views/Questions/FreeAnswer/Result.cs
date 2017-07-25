using MaterialSkin.Controls;
using Newtonsoft.Json;
using Skolni_testy.App;
using Skolni_testy.Models;
using Skolni_testy.Models.QuestionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Skolni_testy.Views.Questions.FreeAnswer
{
    using t = Properties.Translations;
    class Result : BaseView
    {
        public Result(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;
            var answer_m = (AnswerModel)data["answer"];
            var answer = JsonConvert.DeserializeObject<FreeAnswerModel.Answer>(answer_m.Data);
            var q = JsonConvert.DeserializeObject<FreeAnswerModel>(answer_m.Question.QuestionData);


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

            var qAnswerLabel = new MaterialLabel();
            qAnswerLabel.Text = t.Answer;
            qAnswerLabel.Location = new System.Drawing.Point(400, 50);
            f.Controls.Add(qAnswerLabel);

            var answerTextBox = new Label();
            answerTextBox.AutoSize = true;
            answerTextBox.Size = new System.Drawing.Size(300, 60);
            answerTextBox.MaximumSize = new System.Drawing.Size(300, 0);
            answerTextBox.Location = new System.Drawing.Point(400, 80);
            f.Controls.Add(answerTextBox);
            answerTextBox.Text = answer.Text;



        }

    }
}
