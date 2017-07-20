using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using Skolni_testy.Models;
using MaterialSkin.Controls;

namespace Skolni_testy.Views.TeacherTests
{
    using t = Properties.Translations;
    class Show : BaseView
    {
        public Show(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender) { }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;

            appContext.ViewManager.RenderPartial("TeachersTopMenu", new Dictionary<string, object> { { "active", "Tests" } });


            var launchedTests = (List<TestInstanceModel>)data["launchedTests"];

            var tests_panel = new Panel();
            tests_panel.Size = new System.Drawing.Size(f.Width - 20, f.Height - 155);
            tests_panel.Location = new System.Drawing.Point(10, 110);
            tests_panel.HorizontalScroll.Maximum = 0;
            tests_panel.AutoScroll = false;
            tests_panel.VerticalScroll.Visible = false;
            tests_panel.AutoScroll = true;
            f.Controls.Add(tests_panel);


            int i = 0;
            foreach (var test in launchedTests)
            {
                var test_class_label = new MaterialLabel();
                test_class_label.Text = test.Class.Nazev;
                tests_panel.Controls.Add(test_class_label);
                test_class_label.Size = new System.Drawing.Size(50, 20);
                test_class_label.Location = new System.Drawing.Point(0, i * 20);


                var test_date_label = new MaterialLabel();
                test_date_label.Text = test.LaunchedAt.ToLongDateString();
                tests_panel.Controls.Add(test_date_label);
                test_date_label.Size = new System.Drawing.Size(200, 20);
                test_date_label.Location = new System.Drawing.Point(55, i * 20);

                var test_running_label = new MaterialLabel();
                test_running_label.Text = t.Finished;
                tests_panel.Controls.Add(test_running_label);
                test_running_label.Size = new System.Drawing.Size(100, 20);
                test_running_label.Location = new System.Drawing.Point(260, i * 20);

                if (test.Active)
                {
                    var time = (DateTime.Now - test.LaunchedAt);
                    test_running_label.Text = time.Hours + ":" + time.Minutes + ":" + time.Seconds;
             
                    var timer = new Timer();
                    timer.Interval = 1000;
                    timer.Tick += (s, e) => {
                        var t = (DateTime.Now - test.LaunchedAt);
                        test_running_label.Text = t.Hours + ":" + t.Minutes + ":" + t.Seconds; };
                    timer.Start();

                    var test_stop_btn = new Button();
                    test_stop_btn.Text = t.Stop;
                    test_stop_btn.FlatStyle = FlatStyle.Flat;
                    test_stop_btn.Location = new System.Drawing.Point(380, i * 20);
                    test_stop_btn.Click += (s, e) => { appContext.Router.SwitchTo("TestInstances", "Stop", new Dictionary<string, object> { { "testInstance", test } }); };
                    tests_panel.Controls.Add(test_stop_btn);
                }
                else
                {
                    var test_results_btn = new Button();
                    test_results_btn.Text = t.Results;
                    test_results_btn.FlatStyle = FlatStyle.Flat;
                    test_results_btn.Location = new System.Drawing.Point(380, i * 20);
                    test_results_btn.Click += (s, e) => { appContext.Router.SwitchTo("TestInstances", "Results", new Dictionary<string, object> { { "id", test.Id } }); };
                    tests_panel.Controls.Add(test_results_btn);
                }

                i++;
            }

            var new_test_instance_btn = new MaterialFlatButton();
            new_test_instance_btn.Text = t.NewTestInstance;
            new_test_instance_btn.Click += (s, e) => { appContext.Router.SwitchTo("TestInstances", "New", new Dictionary<string, object> { { "test", data["test"] } } ); };
            new_test_instance_btn.Location = new System.Drawing.Point(f.Width-160, f.Height - 38);
            f.Controls.Add(new_test_instance_btn);

            var back_btn = new MaterialFlatButton();
            back_btn.Text = t.Back;
            back_btn.Location = new System.Drawing.Point(20, f.Height - 38);
            back_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("TeacherTests", "Index", null);

            };
            f.Controls.Add(back_btn);


            f.Refresh();
        }
    }
}
