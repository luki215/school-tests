using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaolinq;
namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class Class_TestModel : DataAccessObject
    {
        [PrimaryKey]
        [BackReference]
        public abstract ClassModel Class { get; set; }

        [PrimaryKey]
        [BackReference]
        public abstract TestModel Test { get; set; }
    }
}
