using System;
using Shaolinq;

namespace Skolni_testy.Models
{

    [DataAccessObject]
    public abstract class StudentModel : DataAccessObject<Guid>
    {
        [AutoIncrement]
        [PersistedMember]
        public abstract Guid Id { get; set; }
        [PersistedMember]
        public abstract string Name { get; set; }

        [BackReference]
        public abstract ClassModel Class { get; set; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<StudentTestInstanceModel> Tests { get; }

    }
}