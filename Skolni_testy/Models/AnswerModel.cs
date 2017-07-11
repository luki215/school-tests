using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaolinq;
namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class AnswerModel : DataAccessObject
    {
        [PersistedMember]
        public abstract string Answer { get; set; }


        [PersistedMember]
        public abstract int Points { get; set; }

        [BackReference]
        public abstract StudentModel Student { get; set; }

        [BackReference]
        public abstract QuestionModel Question { get; set; }

    }
}
