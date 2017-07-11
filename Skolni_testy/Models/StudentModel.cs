using System;
using Shaolinq;

namespace Skolni_testy.Models
{

    [DataAccessObject]
    public abstract class StudentModel : DataAccessObject<Guid>
    {
        [PersistedMember]
        public abstract string Name { get; set; }

        [BackReference]
        public abstract ClassModel Class { get; set; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<AnswerModel> Answers { get; }

    }
}