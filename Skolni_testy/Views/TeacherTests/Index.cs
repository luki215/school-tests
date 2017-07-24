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
    class Index : BaseView
    {
        public Index(SkolniTestyAppContext context, Form formToRender) : base(context, formToRender){ }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;

            appContext.ViewManager.RenderPartial("TeachersTopMenu", new Dictionary<string, object> { {"active", "TeacherTests"}});

            var lectures_tests = (Dictionary<string, List<TestModel>>)data["LecturesTests"];

            var lectures_panel = new Panel();
            lectures_panel.Size = new System.Drawing.Size (f.Width-20, f.Height - 155);
            lectures_panel.Location = new System.Drawing.Point(10, 110);
            lectures_panel.HorizontalScroll.Maximum = 0;
            lectures_panel.AutoScroll = false;
            lectures_panel.VerticalScroll.Visible = false;
            lectures_panel.AutoScroll = true;
            f.Controls.Add(lectures_panel);

            int before_y = 0;
            foreach(var lect in lectures_tests)
            {
                before_y = displayLecture(lect, lectures_panel, before_y);
               
            }

            var new_lect_btn = new MaterialFlatButton();
            new_lect_btn.Text = t.NewLecture;
            new_lect_btn.Click += (s, e) => { appContext.Router.SwitchTo("Lectures", "New", null); };
            new_lect_btn.Location = new System.Drawing.Point(f.Width-130, f.Height-38);
            f.Controls.Add(new_lect_btn);

            var logout_btn = new MaterialFlatButton();
            logout_btn.Text = t.Logout;
            logout_btn.Location = new System.Drawing.Point(10, f.Height - 38);
            logout_btn.Click += (s, e) => { appContext.Router.SwitchTo("MainScreen", "Index", new Dictionary<string, object> { { "infos", t.YouVeBeenLoggedOut } }); };
            f.Controls.Add(logout_btn);

            f.Refresh();
        }

        private int displayLecture(KeyValuePair<string, List<TestModel>> lect, Panel p, int y_from)
        {
            var lect_label = new MaterialLabel();
            lect_label.Text = lect.Key;
            lect_label.Location = new System.Drawing.Point(0, y_from);
            lect_label.Size = new System.Drawing.Size(200, 20);
            p.Controls.Add(lect_label);


            var divider = new MaterialDivider();
            divider.Size = new System.Drawing.Size(p.Width-30, 2);
            divider.Location = new System.Drawing.Point(0, y_from + 25);
            p.Controls.Add(divider);

            var test_panel = new Panel();
            test_panel.BackColor = System.Drawing.Color.White;
            test_panel.AutoSize = true;
            test_panel.Location = new System.Drawing.Point(20, y_from + 30);
            p.Controls.Add(test_panel);
            test_panel.Height = 0;
            test_panel.MinimumSize = new System.Drawing.Size(p.Width - 20, 0);

            int i = 0;
            foreach (var test in lect.Value)
            {
                var test_name_label = new MaterialLabel();
                test_name_label.Text = test.Name;
                test_panel.Controls.Add(test_name_label);
                test_name_label.Size = new System.Drawing.Size(250, 20);
                test_name_label.Location = new System.Drawing.Point(0, i * 20);

                var test_details_btn = new Button();
                test_details_btn.Text = t.Detail;
                test_details_btn.FlatStyle = FlatStyle.Flat;
                test_details_btn.Location = new System.Drawing.Point(260, i * 20);
                test_details_btn.Click += (s, e) => { appContext.Router.SwitchTo("TeacherTests", "Show", new Dictionary<string, object> { { "test", test } }); };
                test_panel.Controls.Add(test_details_btn);

                var test_edit_btn = new Button();
                test_edit_btn.Text = t.Edit;
                test_edit_btn.FlatStyle = FlatStyle.Flat;
                test_edit_btn.Location = new System.Drawing.Point(350, i * 20);
                test_edit_btn.Click += (s, e) => { appContext.Router.SwitchTo("TeacherTests", "Edit", new Dictionary<string, object> { { "id", test.Id } }); };
                test_panel.Controls.Add(test_edit_btn);

                i++;
            }

            var new_test_btn = new MaterialFlatButton();
            new_test_btn.Text = t.NewTest;
            new_test_btn.Location = new System.Drawing.Point(10, i * 20 +5);
            new_test_btn.Click += (s, e) => { appContext.Router.SwitchTo("TeacherTests", "New", new Dictionary<string, object> { {"lecture", lect.Key } }); };
            test_panel.Controls.Add(new_test_btn);

           
            return y_from + i*20 + 92;
        }
    }
}
