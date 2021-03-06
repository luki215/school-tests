﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaolinq;
using Shaolinq.MySql;

namespace Skolni_testy.Models
{

    [DataAccessModel]
    public abstract class DBModel : DataAccessModel
    {
        [DataAccessObjects]
        public abstract DataAccessObjects<AdminModel> Admins { get; }
        [DataAccessObjects]
        public abstract DataAccessObjects<StudentModel> Students { get; }
        [DataAccessObjects]
        public abstract DataAccessObjects<ClassModel> Classes { get; }
        [DataAccessObjects]
        public abstract DataAccessObjects<ClassTestInstanceModel> ClassTestInstances { get; }
        [DataAccessObjects]
        public abstract DataAccessObjects<StudentTestInstanceModel> StudentTestInstanceModel { get; }
        [DataAccessObjects]
        public abstract DataAccessObjects<TestModel> Tests { get; }
        [DataAccessObjects]
        public abstract DataAccessObjects<LectureModel> Lectures { get; }

        [DataAccessObjects]
        public abstract DataAccessObjects<QuestionModel> Questions { get; }
        [DataAccessObjects]
        public abstract DataAccessObjects<AnswerModel> Answers { get; }


        public static DataAccessModelConfiguration GetDBConfiguration()
        {
           return MySqlConfiguration.Create("SchoolTestsDB", "localhost", "root", "usbw");
        }
    }
}
