using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shaolinq;
namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class QuestionModel : DataAccessObject<Guid>
    {
        [AutoIncrement]
        [PersistedMember]
        public abstract Guid Id { get; set; }

        [Index]
        [PersistedMember]
        public abstract string Kind { get; set; }

        [Index]
        [PersistedMember]
        public abstract int Order { get; set; }
        
        [PersistedMember]
        public abstract int MaxPoints { get; set; }

        [PersistedMember]
        public abstract string QuestionData { get; set; }

        [BackReference]
        public abstract TestModel Test { get; set; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<AnswerModel> Answers { get; }


        public static(string Name, string Translation)[] QuestionTypes = new ValueTuple<string, string>[] {   ("Choices", Properties.Translations.QuestionsChoices),
                                                                                                        ("FreeAnswer", Properties.Translations.QuestionsFreeAnswer) };

        internal AnswerModel.AnswerStatus CorrectStatus(AnswerModel ans)
        {
            throw new NotImplementedException();
        }
    }
}
