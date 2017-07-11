using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skolni_testy.Views;
using System.Windows.Forms;

namespace Skolni_testy.App
{
    class ViewManager
    {
        Dictionary<String, BaseView> views = new Dictionary<string, BaseView>();
        readonly Form formToRender;
        private SkolniTestyAppContext _context;
        public SkolniTestyAppContext Context
        {
            private get { return _context; }
            set
            {
                if (_context == null)
                    _context = value;
                else throw new Exception("Cannot change viewManager context, when is already set");
            }
        }

        public void RenderView(string controller, string action, Dictionary<string, object> data)
        {
            BaseView viewToRender;

            if (!views.TryGetValue(controller+action, out viewToRender))
            {

                Type view_type;
                try
                {
                    view_type = Type.GetType($"Skolni_testy.Views.{controller}.{action}");
                }
                catch (TypeLoadException e)
                {
                    throw new NoSuchViewException(controller, action);
                }

                viewToRender = (BaseView)Activator.CreateInstance(view_type, Context, formToRender);

                views.Add(controller, viewToRender);
            }

            formToRender.Controls.Clear();
            viewToRender.Render(data);
                
        }
        public ViewManager(Form formToRender)
        {
            this.formToRender = formToRender;
        }
    }

    class NoSuchViewException : Exception
    {
        public NoSuchViewException(string controller, string action) : base($"No such view for {controller} - {action}") { }
    }
}
