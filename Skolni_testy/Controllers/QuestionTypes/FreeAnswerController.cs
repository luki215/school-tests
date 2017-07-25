using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;
using System.Windows.Forms;
using MaterialSkin.Controls;
using Shaolinq;
using Newtonsoft.Json;
using Skolni_testy.Models;

namespace Skolni_testy.Controllers.QuestionTypes
{
    class FreeAnswerController : BaseController
    {
        public FreeAnswerController(SkolniTestyAppContext appContext) : base(appContext)
        {
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Process": Process(parameters); break;
                case "SaveAnswer": SaveAnswer(parameters); break;
                case "MarkCorrect": MarkCorrect(parameters); break;
                default: throw new NoSuchActionInController(action, "ChoicesController");
            }
        }
        private void MarkCorrect(Dictionary<string, object> parameters)
        {

            var correct = (bool)parameters["correct"];
            var answer = (AnswerModel)parameters["answer"];
            using (var scope = new DataAccessScope())
            {
                var ans_model = appContext.DB.Answers.GetReference(answer);
                if (correct) ans_model.Correct = AnswerModel.AnswerStatus.OK;
                else ans_model.Correct = AnswerModel.AnswerStatus.Wrong;
                scope.Complete();
            }
            appContext.Router.SwitchTo("TestInstances", "ResultsDetail", new Dictionary<string, object> { {"test", parameters["test"] } } );
        }
        private void SaveAnswer(Dictionary<string, object> parameters)
        {
            var qTab = (TabPage)parameters["answerTab"];
            var question = (QuestionModel)parameters["question"];
            var student = ((StudentModel Student, ClassModel Class))appContext.Session["loggedStudent"];
            var answer_text = qTab.Controls.Find("Answer", false).FirstOrDefault().Text;
            var answer = new Models.QuestionTypes.FreeAnswerModel.Answer { Text = answer_text};


            using (var scope = new DataAccessScope())
            {
                var ans = appContext.DB.Answers.Create();
                ans.Question = question;
                ans.StudentTestInstance = (StudentTestInstanceModel)parameters["test"];
                ans.Data = JsonConvert.SerializeObject(answer);
                ans.Correct =AnswerModel.AnswerStatus.DontKnow;
                scope.Complete();
            }



            }

        private void Process(Dictionary<string, object> parameters)
        {
            var qTab = (TabPage)parameters["questionTab"];
            var qText = ((TextBox)qTab.Controls.Find("QuestionText", false).FirstOrDefault()).Text;

            var qData = new Models.QuestionTypes.FreeAnswerModel();
            qData.QuestionText = qText;

            using (var scope = new DataAccessScope())
            {
                Skolni_testy.Models.QuestionModel question;
                /* we're processing existing question */
                if (qTab.Tag != null)
                {
                    question = appContext.DB.Questions.GetReference(new { Id = (Guid)qTab.Tag });
                }
                // new question
                else
                {              
                    question = appContext.DB.Questions.Create();                    
                }

                question.QuestionData = JsonConvert.SerializeObject(qData);
                question.Kind = "FreeAnswer";
                question.Order = int.Parse(qTab.Text);
                question.Test = (TestModel)parameters["test"];

                scope.Complete();

            }


        }
    }
}
