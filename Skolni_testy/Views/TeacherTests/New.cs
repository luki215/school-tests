﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using MaterialSkin.Controls;
using Skolni_testy.Models;

namespace Skolni_testy.Views.TeacherTests
{
    using t = Properties.Translations;
    class New : BaseView
    {
        public New(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;
            appContext.ViewManager.RenderPartial("TeachersTopMenu", new Dictionary<string, object> { { "active", "Tests" } });
            
            var test_name_label = new MaterialLabel();
            test_name_label.Text = t.TestName + ":";
            test_name_label.Location = new System.Drawing.Point(10, 130);
            f.Controls.Add(test_name_label);

            var test_name_input = new MaterialSingleLineTextField();
            test_name_input.Size = new System.Drawing.Size(200, 30);
            test_name_input.Location = new System.Drawing.Point(120, 130);
            test_name_input.Text = t.TestName;
            test_name_input.GotFocus += (s, e) => test_name_input.SelectAll();

            f.Controls.Add(test_name_input);


            var test_tabs = new TabControl();
            test_tabs.Size = new System.Drawing.Size(f.Width, f.Height - 200);
            test_tabs.Location = new System.Drawing.Point(0, 160);

            
            var new_q_page = new TabPage();
            new_q_page.Text = t.NewQuestion;
            test_tabs.TabPages.Add(new_q_page);
            appContext.ViewManager.RenderView("Questions.Choices", "New", null, new_q_page);


            var back_btn = new MaterialFlatButton();
            back_btn.Text = t.Back;
            back_btn.Location = new System.Drawing.Point(20, f.Height - 38);
            back_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("TeacherTests", "Index", null);

            };
            f.Controls.Add(back_btn);


            var new_q_btn = new MaterialFlatButton();
            new_q_btn.Text = t.NewQuestion;
            new_q_btn.Location = new System.Drawing.Point(120, f.Height - 38);
            new_q_btn.Click += (s, e) => {
                test_tabs.TabPages[test_tabs.TabPages.Count - 1].Text = test_tabs.TabPages.Count.ToString();
                var new_q_page2 = new TabPage();
                new_q_page2.Text = t.NewQuestion;
                test_tabs.TabPages.Add(new_q_page2);
                test_tabs.SelectedTab = new_q_page2;
                appContext.ViewManager.RenderView("Questions.Choices", "New", null, new_q_page2);

            };
            f.Controls.Add(new_q_btn);




            var save_test_btn = new MaterialFlatButton();
            save_test_btn.Text = t.Save;
            save_test_btn.Location = new System.Drawing.Point(f.Width - 150, f.Height - 38);
            save_test_btn.Click += (s, e) => {

                if (test_tabs.TabPages[test_tabs.TabPages.Count - 1].Text == t.NewQuestion)
                    test_tabs.TabPages.RemoveAt(test_tabs.TabPages.Count - 1);


                appContext.Router.SwitchTo("TeacherTests", "Create", new Dictionary<string, object> {{ "testName", test_name_input.Text },
                                                                                                    { "questionsTabs", test_tabs },
                                                                                                    { "lecture", ((LectureModel)data["lecture"])}
                });

            };
            f.Controls.Add(save_test_btn);


            f.Controls.Add(test_tabs);
        }
    }
}
