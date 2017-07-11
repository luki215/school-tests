using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shaolinq;
using DevOne.Security.Cryptography.BCrypt;

namespace Skolni_testy.Models
{

    [DataAccessObject]
    public abstract class AdminModel : DataAccessObject
    {
        [PrimaryKey]
        [PersistedMember]
        public abstract string Login { get; set; }
        [PersistedMember]
        public abstract string PasswordHash { get; set; }
        
        public static string GetPasswordHash(string password)
        {
            return BCryptHelper.HashPassword(password + "6P!f!y", BCryptHelper.GenerateSalt());
        }

        public bool CheckPassword(string password)
        {
            return BCryptHelper.CheckPassword(password + "6P!f!y", PasswordHash);

        }
    }
}
