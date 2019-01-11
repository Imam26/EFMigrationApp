using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTablesRelationApp
{
    public class Student
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
