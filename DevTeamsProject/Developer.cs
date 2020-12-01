using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class Developer
    {
        public string Name { get; set; }
        public int IDNumber { get; set; }
        public bool HasPluralsightAccess { get; set; }

        public Developer() { }

        public Developer(string name, int idNumber, bool hasPluralsightAccess)
        {
            Name = name;
            IDNumber = idNumber;
            HasPluralsightAccess = hasPluralsightAccess;
        }
    }
}
