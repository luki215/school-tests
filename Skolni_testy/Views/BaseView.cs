using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skolni_testy.App;
using System.Windows.Forms;

namespace Skolni_testy.Views
{
    abstract class BaseView
    {
        protected readonly SkolniTestyAppContext appContext;
        protected readonly Form formToRender;
        public abstract void Render(Dictionary<string, object> data);
        public BaseView(SkolniTestyAppContext context, Form formToRender)
        {
            appContext = context;
            this.formToRender = formToRender;
        }


    }
}
