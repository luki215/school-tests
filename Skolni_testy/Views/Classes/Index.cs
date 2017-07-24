using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;
using MaterialSkin.Controls;

namespace Skolni_testy.Views.Classes
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
            appContext.ViewManager.RenderPartial("TeachersTopMenu", new Dictionary<string, object> { { "active", "Classes" } });




            var classes_panel = new Panel();
            classes_panel.Size = new System.Drawing.Size(f.Width - 20, f.Height - 155);
            classes_panel.Location = new System.Drawing.Point(10, 110);
            classes_panel.HorizontalScroll.Maximum = 0;
            classes_panel.AutoScroll = false;
            classes_panel.VerticalScroll.Visible = false;
            classes_panel.AutoScroll = true;
            f.Controls.Add(classes_panel);

            int i = 0;
            foreach (var class_ in (IEnumerable<Models.ClassModel>) data["classes"])
            {
                var class_name_label = new MaterialLabel();
                class_name_label.Text = class_.Nazev;
                classes_panel.Controls.Add(class_name_label);
                class_name_label.Size = new System.Drawing.Size(250, 20);
                class_name_label.Location = new System.Drawing.Point(0, i * 20);

                var class_details_btn = new Button();
                class_details_btn.Text = t.Detail;
                class_details_btn.FlatStyle = FlatStyle.Flat;
                class_details_btn.Location = new System.Drawing.Point(260, i * 20);
                class_details_btn.Click += (s, e) => { appContext.Router.SwitchTo("Classes", "Show", new Dictionary<string, object> { { "class", class_ } }); };
                classes_panel.Controls.Add(class_details_btn);

                var class_delete_btn = new Button();
                class_delete_btn.Text = t.Delete;
                class_delete_btn.FlatStyle = FlatStyle.Flat;
                class_delete_btn.Location = new System.Drawing.Point(350, i * 20);
                class_delete_btn.Click += (s, e) => { appContext.Router.SwitchTo("Classes", "Delete", new Dictionary<string, object> { { "class", class_ } }); };
                classes_panel.Controls.Add(class_delete_btn);


                i++;
            }

            var new_class_name_input = new MaterialSingleLineTextField();
            new_class_name_input.Text = t.NewClassName;
            new_class_name_input.Location = new System.Drawing.Point(f.Width - 330, f.Height - 30);
            new_class_name_input.Size = new System.Drawing.Size(200, 30);
            new_class_name_input.GotFocus += (s, e) => new_class_name_input.SelectAll();

            f.Controls.Add(new_class_name_input);

            var new_class_btn = new MaterialFlatButton();
            new_class_btn.Text = t.Save;
            new_class_btn.Click += (s, e) => { appContext.Router.SwitchTo("Classes", "Create", new Dictionary<string, object> { { "class_name", new_class_name_input.Text } }); };
            new_class_btn.Location = new System.Drawing.Point(f.Width - 130, f.Height - 38);
            f.Controls.Add(new_class_btn);

            var logout_btn = new MaterialFlatButton();
            logout_btn.Text = t.Logout;
            logout_btn.Location = new System.Drawing.Point(10, f.Height - 38);
            logout_btn.Click += (s, e) => { appContext.Router.SwitchTo("MainScreen", "Index", new Dictionary<string, object> { { "infos", t.YouVeBeenLoggedOut } }); };
            f.Controls.Add(logout_btn);

        }
    }
}
