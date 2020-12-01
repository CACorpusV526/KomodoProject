using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class DevTeam
    {
        public string TeamName { get; set; }
        public string Name { get; set; }
        public int IDNumber { get; set; }
        public bool HasPluralsightAccess { get; set; }

        public DevTeam(string teamname, string name, int idNumber, bool hasPluralsightAccess)
        {
            TeamName = teamname;
            Name = name;
            IDNumber = idNumber;
            HasPluralsightAccess = hasPluralsightAccess;
        }

        public DevTeam() { }
    }
}
