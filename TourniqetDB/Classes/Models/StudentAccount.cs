using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourniqetDB.Classes
{
    [System.ComponentModel.DataAnnotations.Schema.Table("tblStudAccs")]
    public class StudentAccount
    {
        public int Id { get; set; } = 0;
        public string Email { get; set; } = "email@domain.cc";
        public string Soil { get; set; } = "0";
        public override string ToString()
        {
            return string.Join(" - ", Id, Email, Soil);
        }
    }
}
