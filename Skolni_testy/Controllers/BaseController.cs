using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skolni_testy.App;

namespace Skolni_testy.Controllers
{
    
    abstract class BaseController
    {
        protected SkolniTestyAppContext appContext;
        public BaseController(SkolniTestyAppContext appContext) { this.appContext = appContext; }
        public abstract void ProcessAction(string action, Dictionary<String, String> parameters);
    }
    
}
