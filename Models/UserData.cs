using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class UserData
    {
        public string User { get; set; }
        public string Password { get; set; }
        public List<PasswordEntry> PasswordSites { get; set; }
    }
}
