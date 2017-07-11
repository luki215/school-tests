using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialSkin.Controls;
using System.Windows.Forms;
using Skolni_testy.App;

namespace Skolni_testy.Views.MainScreen
{
    class Index : BaseView
    {
        public Index(SkolniTestyAppContext context, Form formToRender) : base(context, formToRender) {   }

        public override void Render(Dictionary<string, object> data)
        {
            var b = new MaterialRaisedButton();
            b.Text = "Ahoj";
            b.Left = 50;
            b.Top = 100;
            b.Size = new System.Drawing.Size(100, 30);
            formToRender.Controls.Add(b);
            formToRender.Refresh();
        }
    }
}
