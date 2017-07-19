using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skolni_testy.Views;
using System.Windows.Forms;
using System.ComponentModel;

namespace Skolni_testy.App
{
    class ViewManager
    {
        Dictionary<(String, Control), BaseView> views = new Dictionary<(string, Control), BaseView>();
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
            RenderView(controller, action,data, formToRender);
        }
        public void RenderView(string controller, string action, Dictionary<string, object> data, Control target)
        {
            BaseView viewToRender;

            if (!views.TryGetValue((controller+action,target), out viewToRender))
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

                if(view_type == null)
                    throw new NoSuchViewException(controller, action);

                viewToRender = (BaseView)Activator.CreateInstance(view_type, Context, target);

                views.Add((controller+action,target), viewToRender);
            }


            //dont clear screen if its only partial
            if (controller != "Partials")
                target.Controls.Clear();

            viewToRender.Render(data);
            //render errors
            if (data != null &&
                controller != "Partials" &&
                action != "ErrorsInfos" &&
                (data.ContainsKey("errors") || data.ContainsKey("infos"))
            )
                RenderPartial("ErrorsInfos", data);

            

        }
        public void RenderPartial(string name, Dictionary<string, object> data) { RenderPartial(name, data, formToRender); }


        public void RenderPartial(string name, Dictionary<string, object> data, Control target)
        {
            RenderView("Partials", name, data, target);
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
