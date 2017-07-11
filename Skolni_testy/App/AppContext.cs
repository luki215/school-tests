using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skolni_testy.App
{

    class SkolniTestyAppContext
    {
        public ViewManager ViewManager { get; private set; }
        public Router Router { get; private set; }
        public SkolniTestyAppContext(Router router, ViewManager viewManager) {
            Router = router;
            ViewManager = viewManager;
        }
    }
}
