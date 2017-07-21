using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.Models;

namespace Skolni_testy.App
{

    class SkolniTestyAppContext
    {
        public ViewManager ViewManager { get; private set; }
        public Router Router { get; private set; }
        public DBModel DB { get; private set; }
        public SkolniTestyAppContext(Router router, ViewManager viewManager, DBModel db) {
            Router = router;
            ViewManager = viewManager;
            DB = db;
        }
        public Dictionary<string, object> Session = new Dictionary<string, object>();
            
    }
}
