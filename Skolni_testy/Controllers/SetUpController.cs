using System;
using System.Collections.Generic;
using Shaolinq;
using Shaolinq.MySql;


using Skolni_testy.Models;
using DevOne.Security.Cryptography.BCrypt;

namespace Skolni_testy.Controllers
{
    class SetUpController
    {
        public static void SetUpDB()
        {
            var configuration = DBModel.GetDBConfiguration();
            var db = DataAccessModel.BuildDataAccessModel<DBModel>(configuration);
            db.Create(DatabaseCreationOptions.DeleteExistingDatabase);


            using (var scope = new DataAccessScope())
            {
                var defaultAdmin = db.Admins.Create();

                defaultAdmin.Login = "default_admin";
                defaultAdmin.PasswordHash = AdminModel.GetPasswordHash("Q2nwGGWXNZtuvr6v");
                scope.Complete();
            }

            using (var scope = new DataAccessScope())
            {

                AdminModel default_admin =  db.Admins.FirstAsync().Result;
                
                if( default_admin.CheckPassword("Q2nwGGWXNZtuvr6v") )
                    Console.WriteLine(default_admin.Login);
                else
                    Console.WriteLine("Nic no");
            }
        }
    }
}
