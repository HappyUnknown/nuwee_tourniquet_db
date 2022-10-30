using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourniqetDB.Classes.Contexts
{
    public class StudentAccountContext: System.Data.Entity.DbContext
    {
        public StudentAccountContext() : base("studAccsConn")
        {
            
        }
        public System.Data.Entity.DbSet<StudentAccount> StudentAccounts { get; set; }
    }
}
