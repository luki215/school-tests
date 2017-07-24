using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;

namespace Skolni_testy.Views.Partials
{

    using t = Properties.Translations;
    class TeachersTopMenu : BaseView
    {
        public TeachersTopMenu(SkolniTestyAppContext context, Form formToRender) : base(context, formToRender)
        {
        }
        public override void Render(Dictionary<string, object> data)
        {
            var f = formToRender;
            var active = (string)data["active"];

            var menu_items = new[]{ new {Text = t.Tests, Controller = "TeacherTests", Action = "Index"},
                                     new {Text = t.Classes, Controller = "Classes", Action = "Index"},
                                     new {Text = t.Teachers, Controller = "Admin", Action = "Index"}
            };

            var menu_panel = new Panel();
            menu_panel.Size = new System.Drawing.Size(f.Width, 35);
            menu_panel.Location = new System.Drawing.Point(0, 65);
            menu_panel.BackColor = System.Drawing.Color.AliceBlue;
            f.Controls.Add(menu_panel);

            int i = 0;
            foreach(var it in menu_items)
            {
                var it_btn = new MaterialSkin.Controls.MaterialFlatButton();
                it_btn.Text = it.Text;
                it_btn.Location = new System.Drawing.Point(200 * i, 0);
                it_btn.Click += (s, a) => { appContext.Router.SwitchTo(it.Controller, it.Action, null); };
                if(active == it.Controller)
                {
                    it_btn.Enabled = false;
                }

                menu_panel.Controls.Add(it_btn);

                i++;
            }
        }
    }
}
