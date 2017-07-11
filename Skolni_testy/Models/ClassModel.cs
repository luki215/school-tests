using Shaolinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skolni_testy.Models
{

    [DataAccessObject]
    public abstract class ClassModel: DataAccessObject<Guid>
    {
        [PersistedMember]
        public abstract string Nazev { get; set; }


        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<Class_TestModel> Class_Tests { get; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<StudentModel> Students { get; }
    }
}
