using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaolinq;
using Shaolinq.MySql;

namespace Skolni_testy.Models
{

    [DataAccessModel]
    public abstract class DBModel : DataAccessModel
    {
        [DataAccessObjects]
        public abstract DataAccessObjects<AdminModel> Admins { get; }

        public static DataAccessModelConfiguration GetDBConfiguration()
        {
           return MySqlConfiguration.Create("SchoolTestsDB", "localhost", "root", "usbw");
        }
    }
}
