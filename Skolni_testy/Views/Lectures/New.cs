using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;

namespace Skolni_testy.Views.Lectures
{
    class New : BaseView
    {
        public New(SkolniTestyAppContext context, Form formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            var lect_name_input = new MaterialSkin.Controls.MaterialSingleLineTextField();
            lect_name_input.Size = new System.Drawing.Size(200, 30);
            lect_name_input.Location = new System.Drawing.Point(60, 120);
            lect_name_input.Text = Properties.Translations.LectureName;
            lect_name_input.GotFocus += (o, a) => { lect_name_input.SelectAll(); };
            formToRender.Controls.Add(lect_name_input);


            var save_btn = new MaterialSkin.Controls.MaterialFlatButton();
            save_btn.Text = Properties.Translations.Save;
            save_btn.Location = new System.Drawing.Point(180, 160);
            save_btn.Click += (s, e) => { appContext.Router.SwitchTo("Lectures", "Create", new Dictionary<string, object> { { "name", lect_name_input.Text } }); };
            formToRender.Controls.Add(save_btn);

            var back_btn = new MaterialSkin.Controls.MaterialFlatButton();
            back_btn.Text = Properties.Translations.Back;
            back_btn.Location = new System.Drawing.Point(60, 160);
            back_btn.Click += (s, e) => { appContext.Router.SwitchTo("TeacherTests", "Index", null); };
            formToRender.Controls.Add(back_btn);

        }
    }
}
