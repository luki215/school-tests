using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using MaterialSkin.Controls;
using Skolni_testy.Models;

namespace Skolni_testy.Views.TestInstances
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
            var student_choose_class_label = new MaterialLabel();
            student_choose_class_label.Text = t.ChooseClassForTest;
            student_choose_class_label.Location = new System.Drawing.Point(50, 110);
            student_choose_class_label.Width = f.Width;
            f.Controls.Add(student_choose_class_label);

            var student_class_radio_panel = new Panel();
            student_class_radio_panel.Location = new System.Drawing.Point(50, 140);
            student_class_radio_panel.BackColor = f.BackColor;
            student_class_radio_panel.Height = 240;
            student_class_radio_panel.AutoScroll = true;
            f.Controls.Add(student_class_radio_panel);
            int i = 0;
            foreach (var s_class in (List<ClassModel>)data["classes"])
            {
                var rad_btn = new MaterialRadioButton();
                rad_btn.Location = new System.Drawing.Point(20, i * 30);
                rad_btn.Text = s_class.Nazev;
                rad_btn.Tag = s_class;
                if (i == 0)
                    rad_btn.Checked = true;
                student_class_radio_panel.Controls.Add(rad_btn);
                i++;
            }


            var launch_test_btn = new MaterialFlatButton();
            launch_test_btn.Text = t.Launch;
            launch_test_btn.Location = new System.Drawing.Point(f.Width - 100, f.Height - 38);
            launch_test_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("TestInstances", "Create", new Dictionary<string, object> {{ "class", (from cl
                                                                                                                in student_class_radio_panel.Controls.OfType<MaterialRadioButton>()
                                                                                                                where cl.Checked
                                                                                                                select cl.Tag).FirstOrDefault()
                                                                                                        },
                                                                                                        { "test", data["test"] }  
                });

            };
            f.Controls.Add(launch_test_btn);

            var back_btn = new MaterialFlatButton();
            back_btn.Text = t.Back;
            back_btn.Location = new System.Drawing.Point(20, f.Height - 38);
            back_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("TeacherTests", "Show", new Dictionary<string, object> { {"test", data["test"] } });

            };
            f.Controls.Add(back_btn);
        }
    }
}
