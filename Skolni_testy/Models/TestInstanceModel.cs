using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaolinq;
namespace Skolni_testy.Models
{
    [DataAccessObject]
    public abstract class TestInstanceModel : DataAccessObject<Guid>
    {
        [PersistedMember]
        [PrimaryKey]
        public abstract Guid Id { get; set; }
            
        [BackReference]
        public abstract ClassModel Class { get; set; }
        
        [BackReference]
        public abstract TestModel Test { get; set; }

        [PersistedMember]
        public abstract bool Active { get; set; }

        [PersistedMember]
        public abstract DateTime LaunchedAt { get; set; }
    }
}
