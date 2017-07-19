using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;

namespace Skolni_testy.Views.Questions.FreeAnswer
{
    class New : BaseView
    {
        public New(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            appContext.ViewManager.RenderPartial("QuestionTypeSelector", new Dictionary<string, object> { { "selected", "FreeAnswer" } }, formToRender);
        }
    }
}
