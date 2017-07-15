using System;
using System.Collections.Generic;
using Shaolinq;
using Shaolinq.MySql;


using Skolni_testy.Models;
using DevOne.Security.Cryptography.BCrypt;
using System.Linq;

namespace Skolni_testy.Controllers
{
    class SetUpController
    {
        public static DBModel SetUpDB()
        {
            var configuration = DBModel.GetDBConfiguration();
            var db = DataAccessModel.BuildDataAccessModel<DBModel>(configuration);

            var fresh_setup = true;
            try { db.Create(DatabaseCreationOptions.DeleteExistingDatabase); }
            catch { fresh_setup = false; }

            if (fresh_setup)
            {
                using (var scope = new DataAccessScope())
                {
                    var defaultAdmin = db.Admins.Create();

                    defaultAdmin.Login = "default_admin";
                    defaultAdmin.PasswordHash = AdminModel.GetPasswordHash("ahoj");


                    var trida1 = db.Classes.Create();
                    trida1.Nazev = "1.A";
                    var trida2 = db.Classes.Create();
                    trida2.Nazev = "2.A";
                    var trida3 = db.Classes.Create();
                    trida3.Nazev = "3.A";
                    var trida4 = db.Classes.Create();
                    trida4.Nazev = "4.A";
                    var trida5 = db.Classes.Create();
                    trida5.Nazev = "5.A";
                    var trida6 = db.Classes.Create();
                    trida6.Nazev = "7.A";

                    var student1 = db.Students.Create();
                    student1.Name = "Lukáš";
                    student1.Class = trida1;

                    var student2 = db.Students.Create();
                    student2.Name = "Karel";
                    student2.Class = trida1;

                    var student3 = db.Students.Create();
                    student3.Name = "Pavel";
                    student3.Class = trida2;

                    var predmet = db.Lectures.Create();
                    predmet.Name = "predmet";

                    var t1 = db.Tests.Create();
                    t1.Name = "T1";
                    t1.Lecture = predmet;
                    var t2 = db.Tests.Create();
                    t2.Name = "T2";
                    t2.Lecture = predmet;

                    scope.Complete();
                }

                using (var scope = new DataAccessScope())
                {

                    AdminModel default_admin = db.Admins.Where(c => c.Login == "default_admin").First();

                    if (default_admin.CheckPassword("Q2nwGGWXNZtuvr6v"))
                        Console.WriteLine(default_admin.Login);
                    else
                        Console.WriteLine("Nic no");
                }
            }


            return db;
        }
    }
}
