using Shaolinq;
using System;

namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class LectureModel : DataAccessObject
    {
        [PrimaryKey]
        [PersistedMember]
        public abstract string Name { get; set; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<TestModel> Tests { get; }
    }
}