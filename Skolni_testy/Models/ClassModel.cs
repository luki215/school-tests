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
        [AutoIncrement]
        [PersistedMember]
        public abstract Guid Id { get; set; }

        [PersistedMember]
        public abstract string Nazev { get; set; }


        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<ClassTestInstanceModel> Class_Tests { get; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<StudentModel> Students { get; }
    }
}
