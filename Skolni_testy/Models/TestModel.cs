using Shaolinq;
using System;

namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class TestModel : DataAccessObject<Guid>
    {
        [AutoIncrement]
        [PersistedMember]
        public abstract Guid Id { get; set; }
        [PersistedMember]
        public abstract string Name { get; set; }


        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<ClassTestInstanceModel> Class_Tests { get; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<QuestionModel> Questions { get; }

        [BackReference]
        public abstract LectureModel Lecture { get; set; }
    }
}