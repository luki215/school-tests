using Shaolinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class StudentTestInstanceModel : DataAccessObject<Guid>
    {
        [PersistedMember]
        [PrimaryKey]
        [AutoIncrement]
        public abstract Guid Id { get; set; }

        [BackReference]
        public abstract ClassTestInstanceModel ClassTestInstance { get; set; }

        [BackReference]
        public abstract StudentModel Student { get; set; }

        [RelatedDataAccessObjects]
        public abstract RelatedDataAccessObjects<AnswerModel> Answers { get; }

    }
}
