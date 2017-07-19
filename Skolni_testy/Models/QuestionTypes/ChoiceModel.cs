using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Skolni_testy.Models.QuestionTypes
{
    class ChoiceModel
    {
        public string QuestionText { get; set; }

        public struct Answer
        {
            public string Text { get; set; }
            public bool Checked { get; set; }
        }
        
        public IEnumerable<Answer> answers;
    }
}
