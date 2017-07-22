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
    class ChoicesController : BaseController
    {
        public ChoicesController(SkolniTestyAppContext appContext) : base(appContext)
        {
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Process": Process(parameters); break;
                case "SaveAnswer": SaveAnswer(parameters); break;
                default: throw new NoSuchActionInController(action, "ChoicesController");
            }
        }

        private void SaveAnswer(Dictionary<string, object> parameters)
        {
            var qTab = (TabPage)parameters["answerTab"];
            var question = (QuestionModel)parameters["question"];
            var student = ((StudentModel Student, ClassModel Class)) appContext.Session["loggedStudent"];
            var answers = (from MaterialCheckBox ans
                         in ((Panel)qTab.Controls.Find("AnswersPanel", false).FirstOrDefault()).Controls.OfType<MaterialCheckBox>()
                          select new Models.QuestionTypes.ChoiceModel.Answer { Text = ans.Text, Checked = ans.Checked }).ToList();


            using (var scope = new DataAccessScope())
            {
                var ans = appContext.DB.Answers.Create();
                ans.Question = question;
                ans.StudentTestInstance = (StudentTestInstanceModel)parameters["test"];
                ans.Data = JsonConvert.SerializeObject(answers);
                ans.Correct = (JsonConvert.DeserializeObject<Models.QuestionTypes.ChoiceModel>(ans.Question.QuestionData)).CorrectStatus(answers);
                scope.Complete();
            }



            }

        private void Process(Dictionary<string, object> parameters)
        {
            var qTab = (TabPage)parameters["questionTab"];
            var qText = ((TextBox)qTab.Controls.Find("QuestionText", false).FirstOrDefault()).Text;

            var qData = new Models.QuestionTypes.ChoiceModel();
            qData.QuestionText = qText;

            var answers = from MaterialCheckBox ans
                          in ((Panel)qTab.Controls.Find("AnswersPanel", false).FirstOrDefault()).Controls.OfType<MaterialCheckBox>()
                          select new Models.QuestionTypes.ChoiceModel.Answer { Text = ans.Text, Checked = ans.Checked };

            //var a = answers.ToList();
            var a = qTab.Controls.Find("AnswersPanel", false);
            Console.WriteLine( answers.Count());
            qData.answers = answers;

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
                question.Kind = "Choices";
                question.Order = int.Parse(qTab.Text);
                question.Test = (TestModel)parameters["test"];

                scope.Complete();

            }


        }
    }
}
