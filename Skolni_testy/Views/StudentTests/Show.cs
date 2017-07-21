using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using Skolni_testy.Models;
using MaterialSkin.Controls;

namespace Skolni_testy.Views.StudentTests
{
    using t = Properties.Translations;
    class Show : BaseView
    {
        public Show(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;
            
            var test = (StudentTestInstanceModel)data["test"];

            var test_name_label = new MaterialLabel();
            test_name_label.Text = t.TestName + ":";
            test_name_label.Location = new System.Drawing.Point(10, 80);
            f.Controls.Add(test_name_label);

            var test_name = new MaterialLabel();
            test_name.Size = new System.Drawing.Size(200, 20);
            test_name.Location = new System.Drawing.Point(120, 80);
            test_name.Text = test.ClassTestInstance.Test.Name;

            f.Controls.Add(test_name);

            
            var test_tabs = new TabControl();
            test_tabs.Size = new System.Drawing.Size(f.Width, f.Height - 150);
            test_tabs.Location = new System.Drawing.Point(0, 110);


            var questions = (IEnumerable<QuestionModel>)data["questions"] ?? new List<QuestionModel>();
            foreach (var q in questions)
            {
                var q_page = new TabPage();
                q_page.Text = q.Order.ToString();
                q_page.Tag = q;
                test_tabs.TabPages.Add(q_page);

                appContext.ViewManager.RenderView($"Questions.{q.Kind}", "Show", new Dictionary<string, object> { { "questionData", q.QuestionData } }, q_page);

            }


           
            var back_btn = new MaterialFlatButton();
            back_btn.Text = t.Back;
            back_btn.Location = new System.Drawing.Point(20, f.Height - 38);
            back_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("StudentTests", "Index", null);

            };
            f.Controls.Add(back_btn);



            var save_test_btn = new MaterialFlatButton();
            save_test_btn.Text = t.Submit;
            save_test_btn.Location = new System.Drawing.Point(f.Width - 150, f.Height - 38);
            save_test_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("StudentTests", "Submit", new Dictionary<string, object> {
                                                                                                    { "test",  data["test"] },
                                                                                                    { "questionsTabs", test_tabs }
                });

            };
            f.Controls.Add(save_test_btn);


            f.Controls.Add(test_tabs);
        }
    }
}
