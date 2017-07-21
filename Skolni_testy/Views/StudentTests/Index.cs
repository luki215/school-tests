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
    class Index : BaseView
    {
        public Index(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;

            var tests_label = new MaterialLabel();
            tests_label.Text = t.Tests;
            f.Controls.Add(tests_label);
            tests_label.Size = new System.Drawing.Size(200, 20);
            tests_label.Location = new System.Drawing.Point(10, 90);
            


            var tests_radio_panel = new Panel();
            tests_radio_panel.Location = new System.Drawing.Point(50, 140);
            tests_radio_panel.BackColor = f.BackColor;
            tests_radio_panel.Height = 240;
            tests_radio_panel.AutoScroll = true;
            f.Controls.Add(tests_radio_panel);
            int i = 0;
            foreach (var test in (List<ClassTestInstanceModel>)data["activeTests"])
            {
                var rad_btn = new MaterialRadioButton();
                rad_btn.Location = new System.Drawing.Point(20, i * 30);
                rad_btn.Text = test.Test.Name;
                rad_btn.Tag = test;
                if (i == 0)
                    rad_btn.Checked = true;
                tests_radio_panel.Controls.Add(rad_btn);
                i++;
            }
            
            var launch_test__btn = new MaterialFlatButton();
            launch_test__btn.Text = t.Launch;
            launch_test__btn.Click += (s, e) => {
                appContext.Router.SwitchTo("StudentTests", "Launch", new Dictionary<string, object> {{ "testInstance", (from cl
                                                                                                                in tests_radio_panel.Controls.OfType<MaterialRadioButton>()
                                                                                                                where cl.Checked
                                                                                                                select cl.Tag).FirstOrDefault()
                                                                                                        }
                });
            };
            launch_test__btn.Location = new System.Drawing.Point(f.Width - 160, f.Height - 38);
            f.Controls.Add(launch_test__btn);

            var back_btn = new MaterialFlatButton();
            back_btn.Text = t.Logout;
            back_btn.Location = new System.Drawing.Point(20, f.Height - 38);
            back_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("StudentTests", "Logout", null);

            };
            f.Controls.Add(back_btn);

            var refresh_btn = new MaterialFlatButton();
            refresh_btn.Text = t.RefreshTests;
            refresh_btn.Location = new System.Drawing.Point(120, f.Height - 38);
            refresh_btn.Click += (s, e) => {
                appContext.Router.SwitchTo("StudentTests", "Index", null);

            };
            f.Controls.Add(refresh_btn);

        }
    }
}
