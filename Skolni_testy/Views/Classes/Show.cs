using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using MaterialSkin.Controls;
using Skolni_testy.Models;

namespace Skolni_testy.Views.Classes
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
            var class_ = (ClassModel)data["class"];
            var students = (IEnumerable<StudentModel>)data["students"];


            var tests_label = new MaterialLabel();
            tests_label.Text = class_.Nazev + " - " + t.Students ;
            f.Controls.Add(tests_label);
            tests_label.Size = new System.Drawing.Size(200, 20);
            tests_label.Location = new System.Drawing.Point(10, 90);

            var students_panel = new Panel();
            students_panel.Size = new System.Drawing.Size(f.Width - 20, f.Height - 155);
            students_panel.Location = new System.Drawing.Point(10, 110);
            students_panel.HorizontalScroll.Maximum = 0;
            students_panel.AutoScroll = false;
            students_panel.VerticalScroll.Visible = false;
            students_panel.AutoScroll = true;
            f.Controls.Add(students_panel);

            int i = 0;
            foreach (var student in students)
            {
                var student_name_label = new MaterialLabel();
                student_name_label.Text = student.Name;
                students_panel.Controls.Add(student_name_label);
                student_name_label.Size = new System.Drawing.Size(250, 20);
                student_name_label.Location = new System.Drawing.Point(0, i * 20);

                var student_delete_btn = new Button();
                student_delete_btn.Text = t.Delete;
                student_delete_btn.FlatStyle = FlatStyle.Flat;
                student_delete_btn.Location = new System.Drawing.Point(250, i * 20);
                student_delete_btn.Click += (s, e) => { appContext.Router.SwitchTo("Students", "Delete", new Dictionary<string, object> { { "student", student }, { "class", class_ } }); };
                students_panel.Controls.Add(student_delete_btn);

                i++;
            }


            var new_student_btn = new MaterialFlatButton();
            var new_student_input = new MaterialSingleLineTextField();
            new_student_input.Text = t.NewStudent;
            new_student_input.Location = new System.Drawing.Point(f.Width - 330, f.Height - 30);
            new_student_input.Size = new System.Drawing.Size(200, 30);
            new_student_input.GotFocus += (s, e) => new_student_input.SelectAll();
            new_student_input.KeyPress += (s, e) =>
            {

                if (e.KeyChar == (char)Keys.Enter)
                {
                    new_student_btn.PerformClick();
                }
            };
                    f.Controls.Add(new_student_input);
            new_student_input.Focus();

            new_student_btn.Text = t.Save;
            new_student_btn.Click += (s, e) => { appContext.Router.SwitchTo("Students", "Create", new Dictionary<string, object> { { "student_name", new_student_input.Text }, { "class", class_ } }); };
            new_student_btn.Location = new System.Drawing.Point(f.Width - 130, f.Height - 38);
            f.Controls.Add(new_student_btn);

            var back_btn = new MaterialFlatButton();
            back_btn.Text = t.Back;
            back_btn.Location = new System.Drawing.Point(10, f.Height - 38);
            back_btn.Click += (s, e) => { appContext.Router.SwitchTo("Classes", "Index", null); };
            f.Controls.Add(back_btn);
        }
    }
}
