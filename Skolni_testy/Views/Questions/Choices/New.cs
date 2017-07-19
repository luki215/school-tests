using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;

using MaterialSkin.Controls;

namespace Skolni_testy.Views.Questions.Choices
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

            appContext.ViewManager.RenderView("Questions.Choices", "Edit", new Dictionary<string, object>() { { "questionData", null } }, formToRender);
        }
    }
}
