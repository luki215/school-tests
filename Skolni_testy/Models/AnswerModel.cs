using Shaolinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class AnswerModel : DataAccessObject<Guid>
    {

        [BackReference]
        public abstract StudentTestInstanceModel StudentTestInstance { get; set; }
        
        [BackReference]
        public abstract QuestionModel Question { get; set; }

        [PersistedMember]
        public abstract string Data { get; set; }
    }
}
