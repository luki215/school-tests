using System;
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
    class Results : BaseView
    {
        public Results(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;
            appContext.ViewManager.RenderPartial("TeachersTopMenu", new Dictionary<string, object> { { "active", "Tests" } });


            var student_tests = (IEnumerable<StudentTestInstanceModel>)data["st_tests"];

            var tests_panel = new Panel();
            tests_panel.Size = new System.Drawing.Size(f.Width - 20, f.Height - 155);
            tests_panel.Location = new System.Drawing.Point(10, 110);
            tests_panel.HorizontalScroll.Maximum = 0;
            tests_panel.AutoScroll = false;
            tests_panel.VerticalScroll.Visible = false;
            tests_panel.AutoScroll = true;
            f.Controls.Add(tests_panel);


            int i = 0;
            foreach (var test in student_tests)
            {
                if (test.Student != null)
                {
                    var test_class_label = new MaterialLabel();
                    test_class_label.Text = test.Student.Name;
                    tests_panel.Controls.Add(test_class_label);
                    test_class_label.Size = new System.Drawing.Size(50, 20);
                    test_class_label.Location = new System.Drawing.Point(0, i * 20);

                    var test_results = new MaterialLabel();
                    test_results.Text = printResults(test);
                    tests_panel.Controls.Add(test_results);
                    test_results.Size = new System.Drawing.Size(330, 20);
                    test_results.Location = new System.Drawing.Point(55, i * 20);

                    var test_details_btn = new Button();
                    test_details_btn.Text = t.Detail;
                    test_details_btn.FlatStyle = FlatStyle.Flat;
                    test_details_btn.Location = new System.Drawing.Point(400, i * 20);
                    test_details_btn.Click += (s, e) => { appContext.Router.SwitchTo("TestInstances", "ResultsDetail", new Dictionary<string, object> { { "test", test } }); };
                    tests_panel.Controls.Add(test_details_btn);

                    i++;
                }
            }

            var back_btn = new MaterialFlatButton();
            back_btn.Text = t.Back;
            back_btn.Location = new System.Drawing.Point(20, f.Height - 38);
            back_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("TeacherTests", "Show", new Dictionary<string, object> { { "test", student_tests.First().ClassTestInstance.Test } });

            };
            f.Controls.Add(back_btn);
        }

        private string printResults(StudentTestInstanceModel test)
        {
            int OK, Wrong, DontKnow;
            OK = Wrong = DontKnow = 0;

            foreach (var ans in test.Answers)
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
            return t.Correct + " (√) : " + OK + ";  " + t.Wrong + " (X) :" + Wrong + ";  " + t.DontKnow + " (?) :" + DontKnow;
        }
    }
}
