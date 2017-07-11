using Shaolinq;
using System;

namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class TestModel : DataAccessObject<Guid>
    {
        [PersistedMember]
        public abstract string Name { get; set; }


        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<Class_TestModel> Class_Tests { get; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<QuestionModel> Questions { get; }

        [BackReference]
        public abstract LectureModel Lecture { get; set; }
    }
}