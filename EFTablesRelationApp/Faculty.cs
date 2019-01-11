using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTablesRelationApp
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Group> Groups { get; set; }

        public Faculty()
        {
            Groups = new List<Group>();
        }
    }
}
