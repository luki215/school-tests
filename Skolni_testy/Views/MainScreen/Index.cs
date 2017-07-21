using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialSkin.Controls;
using System.Windows.Forms;
using Skolni_testy.App;
using Skolni_testy.Models;

namespace Skolni_testy.Views.MainScreen
{
    using t = Properties.Translations;

    class Index : BaseView
    {
        public Index(SkolniTestyAppContext context, Form formToRender) : base(context, formToRender) {   }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;

            var classes_students = (Dictionary<ClassModel, List<StudentModel>>)data["classes_students"];

            var divider = new MaterialDivider();
            divider.Width = 2;
            divider.Height = f.Height-150;
            divider.Location = new System.Drawing.Point(f.Width / 2 - 1, 100);
            f.Controls.Add(divider);


            // Student part
            var student_top_label = new MaterialLabel();
            student_top_label.Width = 100;
            student_top_label.Height = 40;
            student_top_label.Text = t.Students;
            student_top_label.Location = new System.Drawing.Point(50, 80);
            f.Controls.Add(student_top_label);

            var student_choose_class_panel = new Panel();
            student_choose_class_panel.BackColor = f.BackColor;
            student_choose_class_panel.Location = new System.Drawing.Point(50, 140);
            student_choose_class_panel.Height = 320;
            student_choose_class_panel.Width = 350;
            f.Controls.Add(student_choose_class_panel);


            var student_choose_class_label = new MaterialLabel();
            student_choose_class_label.Text = t.ChooseYourClass;
            student_choose_class_label.Location = new System.Drawing.Point(0, 0);
            student_choose_class_label.Width = student_choose_class_panel.Width;
            student_choose_class_panel.Controls.Add(student_choose_class_label);

            var student_class_radio_panel = new Panel();
            student_class_radio_panel.Location = new System.Drawing.Point(0, 30);
            student_class_radio_panel.BackColor = student_choose_class_panel.BackColor;
            student_class_radio_panel.Height = 240;
            student_class_radio_panel.AutoScroll = true;
            student_choose_class_panel.Controls.Add(student_class_radio_panel);
            var class_radio_buttons = new List<MaterialRadioButton>();
            int i = 0;
            foreach (var s_class in classes_students )
            {
                var rad_btn = new MaterialRadioButton();
                rad_btn.Location = new System.Drawing.Point(20, i * 30);
                rad_btn.Text = s_class.Key.Nazev;
                rad_btn.Tag = s_class.Key;
                if (i == 0)
                    rad_btn.Checked = true;
                student_class_radio_panel.Controls.Add(rad_btn);
                class_radio_buttons.Add(rad_btn);
                i++;
            }


            var student_choose_student_panel = new Panel();
            student_choose_student_panel.BackColor = student_choose_class_panel.BackColor;
            student_choose_student_panel.Height =  student_choose_class_panel.Height;
            student_choose_student_panel.Width = student_choose_class_panel.Width;
            student_choose_student_panel.Location = student_choose_class_panel.Location;
            student_choose_student_panel.Visible = false;
            f.Controls.Add(student_choose_student_panel);

            var student_choose_student_radio_panel = new Panel();
            student_choose_student_radio_panel.Location = student_class_radio_panel.Location;
            student_choose_student_radio_panel.BackColor = student_class_radio_panel.BackColor;
            student_choose_student_radio_panel.Height = student_class_radio_panel.Height;
            student_choose_student_radio_panel.AutoScroll = student_class_radio_panel.AutoScroll;

            student_choose_student_panel.Controls.Add(student_choose_student_radio_panel);

            var student_class_next_btn = new MaterialFlatButton();
            student_class_next_btn.Text = t.Next;
            student_class_next_btn.Location = new System.Drawing.Point(200, 270);
            student_class_next_btn.Click += (object sender, EventArgs e) => { student_choose_class_panel.Hide();
                                                                              var s_class = (ClassModel)class_radio_buttons.FirstOrDefault(r => r.Checked).Tag;
                                                                              
                                                                              LoadStudentsRadioButtons( classes_students[s_class], student_choose_student_radio_panel);
                                                                              student_choose_student_panel.Show(); };
            student_choose_class_panel.Controls.Add(student_class_next_btn);

            var student_choose_name_label = new MaterialLabel();
            student_choose_name_label.Text = t.ChooseYourName;
            student_choose_name_label.Location = student_choose_class_label.Location;
            student_choose_name_label.Width = student_choose_class_label.Width;

            student_choose_student_panel.Controls.Add(student_choose_name_label);

            var student_student_prev_button = new MaterialFlatButton();
            student_student_prev_button.Text = t.Prev;
            student_student_prev_button.Location = new System.Drawing.Point(50, 270);
            student_student_prev_button.Click += (object sender, EventArgs e) => {
                student_choose_class_panel.Show();
                student_choose_student_panel.Hide();
            };
            student_choose_student_panel.Controls.Add(student_student_prev_button);


            var student_student_next_btn = new MaterialFlatButton();
            student_student_next_btn.Text = t.Next;
            student_student_next_btn.Location = new System.Drawing.Point(200, 270);
            student_student_next_btn.Click += (object sender, EventArgs e) =>
            {
                var s_class = class_radio_buttons.FirstOrDefault(r => r.Checked).Tag;
                var s_name = student_choose_student_radio_panel.Controls.OfType<MaterialRadioButton>().FirstOrDefault(r => r.Checked).Tag;
                appContext.Router.SwitchTo("MainScreen", "SetStudent", new Dictionary<string, object> { { "class", s_class }, { "student", s_name } });
            };
            student_choose_student_panel.Controls.Add(student_student_next_btn);





            // teachers part
            
            var teacher_top_label = new MaterialLabel();
            teacher_top_label.Width = 100;
            teacher_top_label.Height = 40;
            teacher_top_label.Text = t.Teachers;
            teacher_top_label.Location = new System.Drawing.Point(470, 80);
            f.Controls.Add(teacher_top_label);

            var teachers_panel = new Panel();
            teachers_panel.BackColor = f.BackColor;
            teachers_panel.Location = new System.Drawing.Point(470, 140);
            teachers_panel.Height = 320;
            teachers_panel.Width = 350;
            f.Controls.Add(teachers_panel);

            var teacher_login_label = new MaterialLabel();
            teacher_login_label.Text = t.PleaseLogIn;
            teacher_login_label.Location = new System.Drawing.Point(0, 0);
            teacher_login_label.Width = student_choose_class_panel.Width;
            teachers_panel.Controls.Add(teacher_login_label);

            var teacher_username_input = new MaterialSingleLineTextField();
            teacher_username_input.Text = t.UserName;
            teacher_username_input.Location = new System.Drawing.Point(10, 50);
            teacher_username_input.Size = new System.Drawing.Size(200, 30);
            teacher_username_input.GotFocus += (object s, EventArgs a) => { teacher_username_input.SelectAll(); };
            teachers_panel.Controls.Add(teacher_username_input);

            var teacher_pass_input = new MaterialSingleLineTextField();
            teacher_pass_input.Text = t.Password;
            teacher_pass_input.Location = new System.Drawing.Point(10, 90);
            teacher_pass_input.Size = new System.Drawing.Size(200, 30);
            teacher_pass_input.GotFocus += (object s, EventArgs a) => {
                teacher_pass_input.UseSystemPasswordChar = true;
                teacher_pass_input.SelectAll();
            };
            teacher_pass_input.LostFocus += (object s, EventArgs a) => { if(teacher_pass_input.Text == "Heslo") teacher_pass_input.UseSystemPasswordChar = false; };
            teachers_panel.Controls.Add(teacher_pass_input);


            var teachers_login_btn = new MaterialFlatButton();
            teachers_login_btn.Text = t.LogIn;
            teachers_login_btn.Location = new System.Drawing.Point(200, 150);
            teachers_login_btn.Click += (object sender, EventArgs e) =>
            {
                appContext.Router.SwitchTo("MainScreen", "TeacherLogin", new Dictionary<string, object> { { "username", teacher_username_input.Text }, { "password", teacher_pass_input.Text } });
            };
            teachers_panel.Controls.Add(teachers_login_btn);


            formToRender.Refresh();
        }


        private void LoadStudentsRadioButtons(List<StudentModel> students,  Panel p)
        {
            p.Controls.Clear();
            int i = 0;
            foreach (var student in students)
            {
                var rad_btn = new MaterialRadioButton();
                rad_btn.Location = new System.Drawing.Point(20, i * 30);
                rad_btn.Text = student.Name;
                rad_btn.BackColor = System.Drawing.Color.White;
                rad_btn.Tag = student;
                if (i == 0)
                    rad_btn.Checked = true;
                p.Controls.Add(rad_btn);
                i++;
            }

        }
    }
}
