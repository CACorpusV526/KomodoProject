using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class DevTeamRepo
    {
        //this gives you access to the _developerDirectory so you can access existing Developers and add them to a team
        private readonly List<DevTeam> _devTeams = new List<DevTeam>();

        //DevTeam Create
        public void AddDevToTeam(DevTeam dev)
        {
            _devTeams.Add(dev);
        }

        //DevTeam Read
        public List<DevTeam> GetDeveloperTeams()
        {
            return _devTeams;
        }

        //DevTeam Update
        public bool UpdateDeveloperTeam(string originalDevTeam, DevTeam newDevTeam)
        {
            DevTeam oldDevTeam = GetDeveloperTeamByName(originalDevTeam);

            if (oldDevTeam != null)
            {
                oldDevTeam.TeamName = newDevTeam.TeamName;
                oldDevTeam.Name = newDevTeam.Name;
                oldDevTeam.IDNumber = newDevTeam.IDNumber;
                oldDevTeam.HasPluralsightAccess = newDevTeam.HasPluralsightAccess;

                return true;
            }
            else
            {
                return false;
            }
        }

        //DevTeam Delete
        public bool RemoveDevTeam(int idNumber)
        {
            DevTeam dev = GetDeveloperTeamByID(idNumber);

            if (dev == null)
            {
                return false;
            }

            int initialCount = _devTeams.Count;
            _devTeams.Remove(dev);

            if (initialCount > _devTeams.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //DevTeam Helper (Get Team by ID)
        public DevTeam GetDeveloperTeamByID(int idNumber)
        {
            foreach (DevTeam dev in _devTeams)
            {
                if (dev.IDNumber == idNumber)
                {
                    return dev;
                }
            }
            return null;
        }

        public DevTeam GetDeveloperTeamByName(string teamName)
        {
            foreach (DevTeam dev in _devTeams)
            {
                if (dev.TeamName == teamName)
                {
                    return dev;
                }
            }
            return null;
        }
    }
}
