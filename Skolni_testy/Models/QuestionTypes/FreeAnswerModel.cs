using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Skolni_testy.Models.QuestionTypes
{
    class FreeAnswerModel
    {
        public string QuestionText { get; set; }

        
        public struct Answer
        {
            public string Text;
        }
        
    }
}
