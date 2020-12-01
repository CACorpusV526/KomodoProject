using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamsProject
{
    public class DeveloperRepo
    {
        public List<Developer> _developerDirectory = new List<Developer>();

        //Developer Create
        public void AddDevToList(Developer dev)
        {
            _developerDirectory.Add(dev);
        }
        //Developer Read
        public List<Developer> GetDeveloperList()
        {
            return _developerDirectory;
        }

        //Developer Update
        public bool UpdateExistingDev(int originalDev, Developer newDev)
        {
            Developer oldDev = GetDeveloperbyID(originalDev);

            if (oldDev != null)
            {
                oldDev.Name = newDev.Name;
                oldDev.IDNumber = newDev.IDNumber;
                oldDev.HasPluralsightAccess = newDev.HasPluralsightAccess;

                return true;
            }
            else
            {
                return false;
            }
        }

        //Developer Delete
        public bool RemoveDevFromList(int idNumber)
        {
            Developer dev = GetDeveloperbyID(idNumber);

            if (dev == null)
            {
                return false;
            }

            int initialCount = _developerDirectory.Count;
            _developerDirectory.Remove(dev);

            if (initialCount > _developerDirectory.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Developer Helper (Get Developer by ID)
        public Developer GetDeveloperbyID(int idNumber)
        {
            foreach (Developer dev in _developerDirectory)
            {
                if (dev.IDNumber == idNumber)
                {
                    return dev;
                }
            }
                return null;
        }
    }
}
