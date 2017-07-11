using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;

namespace Skolni_testy.Controllers
{
    class AdminController : BaseController
    {
        public AdminController(SkolniTestyAppContext appContext) : base(appContext){}

        override public void ProcessAction(string action, Dictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
