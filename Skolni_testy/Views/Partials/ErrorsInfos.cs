using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;

namespace Skolni_testy.Views.Partials
{
    class ErrorsInfos : BaseView
    {
        public ErrorsInfos(SkolniTestyAppContext context, ContainerControl formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            object error_msg_obj;
            if (data.TryGetValue("errors", out error_msg_obj))
            {
                string error_msg = (string)error_msg_obj;
                renderInfoBox(System.Drawing.Color.FromArgb(150, 200, 0, 0), error_msg);
            }
            if (data.TryGetValue("infos", out error_msg_obj))
            {
                string info_msg = (string)error_msg_obj;
                renderInfoBox(System.Drawing.Color.FromArgb(150, 0, 100, 255), info_msg);
            }
        }

        private void renderInfoBox(System.Drawing.Color bg, string msg)
        {
            var f = formToRender;
            var panel = new Panel();
            var text = new Label();
            var close_btn = new Button();

            var newlines = msg.Count((c) => { return c == '\n'; });


            panel.BackColor = bg;
            panel.Controls.Add(close_btn);
            panel.Controls.Add(text);
            panel.ForeColor = System.Drawing.Color.White;
            panel.Size = new System.Drawing.Size(f.Width, 50 + newlines * 25);
            panel.Location = new System.Drawing.Point(0, 63);
            
            close_btn.BackColor = System.Drawing.Color.Transparent;
            close_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            close_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            close_btn.MouseEnter += (s, a) => { close_btn.Font = new System.Drawing.Font(close_btn.Font, System.Drawing.FontStyle.Bold); };
            close_btn.MouseLeave += (s, a) => { close_btn.Font = new System.Drawing.Font(close_btn.Font, System.Drawing.FontStyle.Regular); };
            close_btn.FlatAppearance.BorderSize = 0;
            close_btn.ForeColor = System.Drawing.Color.White;
            close_btn.UseVisualStyleBackColor = false;
            close_btn.Text = "X";
            close_btn.Font = new System.Drawing.Font(close_btn.Font.Name, 13);
            close_btn.Width = 30;
            close_btn.Height = 30;
            close_btn.Location = new System.Drawing.Point(f.Width - 30, 5);
            close_btn.FlatStyle = FlatStyle.Flat;
            close_btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            close_btn.Click += (s, a) => { panel.Hide(); };

            text.Text = msg;
            text.Font = new System.Drawing.Font(text.Font.Name, 15);
            text.Location = new System.Drawing.Point(20, 10);
            text.BackColor = System.Drawing.Color.Transparent;
            text.Width = f.Width - 40;
            text.AutoSize = true;

            f.Controls.Add(panel);
            panel.BringToFront();

        }
    }
}
