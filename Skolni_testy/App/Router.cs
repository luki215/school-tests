using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skolni_testy.Controllers;

namespace Skolni_testy.App
{
    class Router
    {
        private SkolniTestyAppContext context;
        public SkolniTestyAppContext Context { private get { return context; } set {
                if (context == null)
                    context = value;
                else throw new Exception("Cannot change router context, when is already set");
            } }
        Dictionary<string, BaseController> controllers = new Dictionary<string, BaseController>();
        public void SwitchTo(string controller, string action, Dictionary<String, String> parameters)
        {
            BaseController processingCtrl;
            if (!controllers.TryGetValue(controller, out processingCtrl))
            {
                Type ctrl_type;
                try
                {
                    ctrl_type = Type.GetType("Skolni_testy.Controllers."+controller+"Controller");
                }
                catch(TypeLoadException e)
                {
                    throw new NoControllerWithSuchNameException(controller + "Controller");
                }

                processingCtrl = (BaseController)Activator.CreateInstance(ctrl_type, Context);

                controllers.Add(controller, processingCtrl);
            }
            
            processingCtrl.ProcessAction(action, parameters);
        }
    }

    class NoControllerWithSuchNameException : Exception
    {
        public NoControllerWithSuchNameException(string controllerName) : base("No controller with such name: " + controllerName) { }
    }

    class NoSuchActionInController : Exception
    {
        public NoSuchActionInController(string actionName, string controllerName) : base($"Controller {controllerName} has no action {actionName} ") { }
    }

}
