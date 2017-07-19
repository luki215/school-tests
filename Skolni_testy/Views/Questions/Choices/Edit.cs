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

namespace Skolni_testy.Views.Questions.Choices
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
            var q = JsonConvert.DeserializeObject<ChoiceModel>(qData) ?? new ChoiceModel();
            answers = (q.answers == null)? new List<ChoiceModel.Answer>() : q.answers.ToList() ;
            var f = formToRender;
            var tabControl = (TabControl)f.Parent;

            appContext.ViewManager.RenderPartial("QuestionTypeSelector", new Dictionary<string, object> { { "selected", "Choices" } }, f);

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
            qTextInput.KeyPress += (s, e) =>
            {   if(qTextInput.TextLength == 1)
                    switchToEdited(f, tabControl);
            };
            f.Controls.Add(qTextInput);
            qTextInput.Text = q.QuestionText;


            var answersPanel = new Panel();
            answersPanel.Name = "AnswersPanel";
            answersPanel.Location = new System.Drawing.Point(500, 30);
            answersPanel.Size = new System.Drawing.Size(300, 160);
            answersPanel.AutoScroll = true;
            f.Controls.Add(answersPanel);
            renderAnswers(answersPanel);

            var newAnswerInput = new MaterialSingleLineTextField();
            newAnswerInput.Size = new System.Drawing.Size(200, 30);
            newAnswerInput.Location = new System.Drawing.Point(500, 210);
            newAnswerInput.Text = t.NewAnswer;
            newAnswerInput.GotFocus += (s, e) => newAnswerInput.SelectAll();
            newAnswerInput.KeyPress += (s, e) => {
               
                if (e.KeyChar == (char)Keys.Enter)
                {
                    addAnswer(new ChoiceModel.Answer { Text = newAnswerInput.Text, Checked = false }, answersPanel);
                    switchToEdited(f, tabControl);
                    newAnswerInput.Text = t.NewAnswer;
                    newAnswerInput.SelectAll();
                    newAnswerInput.Focus();

                    //ding song fix
                    e.Handled = true;
                }
            };

            f.Controls.Add(newAnswerInput);


            var newAnswerConfirm = new MaterialFlatButton();
            newAnswerConfirm.Location = new System.Drawing.Point(720, 200);
            newAnswerConfirm.Text = t.Add;
            newAnswerConfirm.Click += (s, e) => {
                addAnswer(new ChoiceModel.Answer { Text = newAnswerInput.Text, Checked = false }, answersPanel);
                newAnswerInput.Text = t.NewAnswer;
                switchToEdited(f, tabControl);
                newAnswerInput.Focus();
            };

            f.Controls.Add(newAnswerConfirm);
            
        }

        List<ChoiceModel.Answer> answers;
        private void addAnswer(ChoiceModel.Answer answer, Panel answers_panel)
        {
            answers.Add(answer);
            renderAnswers(answers_panel);
        }

        private void renderAnswers(Panel answers_panel)
        {
            answers_panel.Controls.Clear();
            int i = 0;
            foreach (var a in answers)
            {
                var y = i;
                var ans = new MaterialCheckBox();
                ans.Text = a.Text;
                ans.Checked = a.Checked;
                ans.Location = new System.Drawing.Point(0, i * 30);
                ans.Size = new System.Drawing.Size(200, 30);
                ans.CheckedChanged += (s, e) => answers[y] = new ChoiceModel.Answer { Checked = !answers[y].Checked, Text = answers[y].Text }; 
                answers_panel.Controls.Add(ans);

                var ans_rem = new Button();
                ans_rem.FlatStyle = FlatStyle.Flat;
                ans_rem.FlatAppearance.BorderSize = 0;
                ans_rem.Text = "X";
                ans_rem.Size = new System.Drawing.Size(30, 30);
                ans_rem.Location = new System.Drawing.Point(205, i * 30);
                ans_rem.Click += (s, e) => removeAnswer(y, answers_panel);
                answers_panel.Controls.Add(ans_rem);


                i++;
            }
        }

        private void removeAnswer(int i, Panel answersPanel)
        {
            answers.RemoveAt(i);
            renderAnswers(answersPanel);
        }

        void switchToEdited(Control panel, TabControl tabs)
        {
            if (panel.Text == t.NewQuestion)
            {
                panel.Text = (tabs.TabPages.IndexOf((TabPage)panel)+1).ToString();
            }

        }
    }
}
