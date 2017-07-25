using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skolni_testy.App;

namespace Skolni_testy.Views.Questions.Choices
{
    class TeacherResult : BaseView
    {
        public TeacherResult(SkolniTestyAppContext context, Control formToRender) : base(context, formToRender)
        {
        }

        public override void Render(Dictionary<string, object> data)
        {
            appContext.ViewManager.RenderView("Questions.Choices", "Result", data, formToRender);
        }
    }
}
