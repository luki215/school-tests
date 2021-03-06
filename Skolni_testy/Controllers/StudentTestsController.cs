﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skolni_testy.App;
using Skolni_testy.Models;
using Shaolinq;
using System.Windows.Forms;

namespace Skolni_testy.Controllers
{
    class StudentTestsController : BaseController
    {
        public StudentTestsController(SkolniTestyAppContext appContext) : base(appContext)
        {
        }

        public override void ProcessAction(string action, Dictionary<string, object> parameters)
        {
            switch (action)
            {
                case "Index": Index(parameters); break;
                case "Launch": Launch(parameters); break;
                case "Show": Show(parameters); break;
                case "Submit": Submit(parameters); break;
                case "Logout": Logout(parameters); break;
                case "Results": Results(parameters); break;
                default: throw new NoSuchActionInController(action, "StudentTests");
            }
        }

        private void Results(Dictionary<string, object> parameters)
        {
            var student_test = (StudentTestInstanceModel)parameters["test"];
            int OK, Wrong, DontKnow;
            OK = Wrong = DontKnow = 0;

            foreach(var ans in student_test.Answers)
            {
                switch (ans.Correct)
                {
                    case AnswerModel.AnswerStatus.OK: OK++;
                        break;
                    case AnswerModel.AnswerStatus.Wrong: Wrong++;
                        break;
                    case AnswerModel.AnswerStatus.DontKnow: DontKnow++;
                        break;
                    default:
                        break;
                }
            }

            appContext.ViewManager.RenderView("StudentTests", "Results", new Dictionary<string, object> {   { "answers", student_test.Answers.OrderBy(t=>t.Question.Order)},
                                                                                                            { "answerStats", (OK, Wrong, DontKnow) }
            });

        }

        private void Logout(Dictionary<string, object> parameters)
        {
            appContext.Session.Remove("loggedStudent");
            appContext.Router.SwitchTo("MainScreen", "Index", parameters);
        }

        private void Submit(Dictionary<string, object> parameters)
        {
            var tabs = (TabControl)parameters["questionsTabs"];


            var answers = from TabPage tab in tabs.TabPages select (Tab: tab, Question: (QuestionModel)tab.Tag);

            var test = answers.First().Question.Test;

            foreach (var a in answers)
            {
                var q_type = a.Question.Kind;
                appContext.Router.SwitchTo($"QuestionTypes.{q_type}", "SaveAnswer", new Dictionary<string, object> { { "answerTab", a.Tab }, { "question", a.Question }, { "test", parameters["test"] } });
            }
            appContext.Router.SwitchTo($"StudentTests", "Results", new Dictionary<string, object> { { "test", parameters["test"] } });
        }

        private void Show(Dictionary<string, object> parameters)
        {
            var test = (StudentTestInstanceModel)parameters["test"];
            var questions = test.ClassTestInstance.Test.Questions.OrderBy(t=>t.Order);

            appContext.ViewManager.RenderView("StudentTests", "Show", new Dictionary<string, object> { { "test", test },
                                                                                                        {  "questions", questions } 
            });
        }

        private void Launch(Dictionary<string, object> parameters)
        {
            var testToLunch = (ClassTestInstanceModel)parameters["testInstance"];

            var student = ((StudentModel Student, ClassModel Class))appContext.Session["loggedStudent"];

            using (var scope = new DataAccessScope())
            {
                var student_test = appContext.DB.StudentTestInstanceModel.Create();
                
                student_test.ClassTestInstance = appContext.DB.ClassTestInstances.GetByPrimaryKey(testToLunch.Id);
                student_test.Student = appContext.DB.Students.GetByPrimaryKey(student.Student.Id);
                scope.Complete();
                appContext.Router.SwitchTo("StudentTests", "Show", new Dictionary<string, object> { { "test", student_test } });
            }
        }

        private void Index(Dictionary<string, object> parameters)
        {
            var activeTests = appContext.DB.ClassTestInstances.Where(t => t.Active).ToList();
            appContext.ViewManager.RenderView("StudentTests", "Index", new Dictionary<string, object> { { "activeTests", activeTests } });
        }
    }
}
