using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;

using MaterialSkin.Controls;

namespace Skolni_testy.Controllers
{
    class MainScreenController : BaseController
    {
        public MainScreenController(SkolniTestyAppContext appContext) : base(appContext){}

        public void Index()
        {
            appContext.ViewManager.RenderView("MainScreen", "Index", null);
        }

        override public void ProcessAction(string action, Dictionary<string, string> parameters)
        {
            switch (action)
            {
                case "Index": Index(); break;
                default: throw new NoSuchActionInController(action, "MainScreenController");
            }
        }
    }
}
