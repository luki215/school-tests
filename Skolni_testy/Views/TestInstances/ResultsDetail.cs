﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using Skolni_testy.Models;
using MaterialSkin.Controls;

namespace Skolni_testy.Views.TestInstances
{
    using t = Properties.Translations;
    class ResultsDetail : BaseView
    {
        public ResultsDetail(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;

            var results_label = new MaterialLabel();
            results_label.Text = t.Results + ":";
            results_label.Location = new System.Drawing.Point(10, 80);
            f.Controls.Add(results_label);

            var (OK, Wrong, DontKnow) = ((int, int, int))data["answerStats"];

            var results = new MaterialLabel();
            results.Size = new System.Drawing.Size(600, 20);
            results.Location = new System.Drawing.Point(120, 80);
            results.Text = t.Correct + " (√) : " + OK + ";      " + t.Wrong + " (X) :" + Wrong + ";       " + t.DontKnow + " (?) :" + DontKnow;

            f.Controls.Add(results);


            var test_tabs = new TabControl();
            test_tabs.Size = new System.Drawing.Size(f.Width, f.Height - 150);
            test_tabs.Location = new System.Drawing.Point(0, 110);


            var answers = (IEnumerable<AnswerModel>)data["answers"] ?? new List<AnswerModel>();
            foreach (var a in answers)
            {
                var q_page = new TabPage();
                char aStatus = ' ';
                if (a.Correct == AnswerModel.AnswerStatus.OK) aStatus = '√';
                if (a.Correct == AnswerModel.AnswerStatus.Wrong) aStatus = 'X';
                if (a.Correct == AnswerModel.AnswerStatus.DontKnow) aStatus = '?';

                q_page.Text = a.Question.Order.ToString() + " " + aStatus;
                q_page.Tag = a;
                test_tabs.TabPages.Add(q_page);

                appContext.ViewManager.RenderView($"Questions.{a.Question.Kind}", "TeacherResult", new Dictionary<string, object> { { "answer", a }, { "test", data["test"] } }, q_page);

            }



            var back_btn = new MaterialFlatButton();
            back_btn.Text = t.Back;
            back_btn.Location = new System.Drawing.Point(20, f.Height - 38);
            back_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("TestInstances", "Results", new Dictionary<string, object> { {"test", answers.First().StudentTestInstance.ClassTestInstance } });

            };
            f.Controls.Add(back_btn);



            f.Controls.Add(test_tabs);
        }
    }
}
