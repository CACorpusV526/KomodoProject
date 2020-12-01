using DevTeamsProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTeamManagement
{
    class DeveloperUI
    {
        private readonly DeveloperRepo _devRepo = new DeveloperRepo();
        private readonly DevTeamRepo _devTeamRepo = new DevTeamRepo();
        

        public void Run()
        {
            SeedDevList();
            Menu();
        }

        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Please select an option:\n" +
                    "1. Add a Developer\n" +
                    "2. View All Developers\n" +
                    "3. Find a Developer (Requires ID #)\n" +
                    "4. Update Developer Information\n" +
                    "5. Remove Developer\n" +
                    "6. Add a Team\n" +
                    "7. View Current Teams\n" +
                    "8. Find Team a Developer is a part of \n" +
                    "9. Update a Team\n" +
                    "10. Remove a Team\n" +
                    "11. Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddADeveloper();
                        break;
                    case "2":
                        ViewAllDevelopers();
                        break;
                    case "3":
                        FindDeveloperByID();
                        break;
                    case "4":
                        UpdateDeveloperInfo();
                        break;
                    case "5":
                        RemoveDeveloper();
                        break;
                    case "6":
                        AddATeam();
                        break;
                    case "7":
                        ViewCurrentTeams();
                        break;
                    case "8":
                        GetTeamById();
                        break;
                    case "9":
                        UpdateTeam();
                        break;
                    case "10":
                        RemoveDeveloperTeam();
                        break;
                    case "11":
                        Console.WriteLine("Enjoy your developers!");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please select a number from the list provided.");
                        break;
                }

                Console.WriteLine("Press any key to continue with your selection...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddADeveloper()
        {
            Console.Clear();
            Developer newDev = new Developer();

            Console.WriteLine("Please give the name of the developer.");
            newDev.Name = Console.ReadLine();

            Console.WriteLine("Please wait for ID # to be generated.");
            newDev.IDNumber = IDNumberGenerator();
            Console.WriteLine("ID generated, please press any key to continue.");
            Console.ReadKey();

            Console.WriteLine("Will this developer need Pluralsight Access? (y/n)");
            string pluralsightAccessString = Console.ReadLine().ToLower();

            if (pluralsightAccessString == "y")
            {
                newDev.HasPluralsightAccess = true;
            }
            else
            {
                newDev.HasPluralsightAccess = false;
            }

            _devRepo.AddDevToList(newDev);
        }
        private void ViewAllDevelopers()
        {
            Console.Clear();
            List<Developer> listofDevelopers = _devRepo.GetDeveloperList();

            foreach (Developer dev in listofDevelopers)
            {
                Console.WriteLine($"Name: {dev.Name}\n" +
                    $"ID #: {dev.IDNumber}\n" +
                    $"Pluralsight Access: {dev.HasPluralsightAccess}");
            }
        }

        private void FindDeveloperByID()
        {
            Console.Clear();
            Console.WriteLine("Enter the ID # of the developer you are looking for:");

            string idInputAsString = Console.ReadLine();
            int idInputAsInt = int.Parse(idInputAsString);

            Developer dev = _devRepo.GetDeveloperbyID(idInputAsInt);

            if (dev != null)
            {
                Console.WriteLine($"Name: {dev.Name}\n" +
                    $"ID #: {dev.IDNumber}\n" +
                    $"Pluralsight Access: {dev.HasPluralsightAccess}");
            }
            else
            {
                Console.WriteLine("No developer by that ID Number");
            }
        }

        private void UpdateDeveloperInfo()
        {
            ViewAllDevelopers();

            Console.WriteLine("Enter ID # of the developer you'd like to update.");

            string oldDevAsString = Console.ReadLine();
            int oldDevAsInt = int.Parse(oldDevAsString);

            Developer newDev = new Developer();

            Console.WriteLine("Please give the name of the developer:");
            newDev.Name = Console.ReadLine();

            Console.WriteLine("Please input original ID #:");
            string idAsString = Console.ReadLine();
            newDev.IDNumber = int.Parse(idAsString);

            Console.WriteLine("Will this developer need Pluralsight Access? (y/n)");
            string hasPluralsightString = Console.ReadLine().ToLower();

            if (hasPluralsightString == "y")
            {
                newDev.HasPluralsightAccess = true;
            }
            else
            {
                newDev.HasPluralsightAccess = false;
            }

            bool wasUpdated = _devRepo.UpdateExistingDev(oldDevAsInt, newDev);

            if (wasUpdated)
            {
                Console.WriteLine("Developer information updated");
            }
            else
            {
                Console.WriteLine("Developer could not be updated");
            }
        }

        private void RemoveDeveloper()
        {
            ViewAllDevelopers();

            Console.WriteLine("\nEnter the ID # of the developer you would like to remove.");

            string inputAsString = Console.ReadLine();
            int inputAsInt = int.Parse(inputAsString);

            bool wasDeleted = _devRepo.RemoveDevFromList(inputAsInt);

            if (wasDeleted)
            {
                Console.WriteLine("Developer successfully removed");
            }
            else
            {
                Console.WriteLine("Developer could not be deleted");
            }
        }

        private void AddATeam()
        {
            {
                Console.Clear();
                DevTeam newDevTeam = new DevTeam();
                Developer newDev = new Developer();

                Console.WriteLine("Enter a name for this team.");
                newDevTeam.TeamName = Console.ReadLine();

                Console.WriteLine("Enter the name of the developer that will be on this team.");
                newDevTeam.Name = Console.ReadLine();
                newDev.Name = newDevTeam.Name;

                Console.WriteLine("Please wait for ID # to be generated.");
                newDevTeam.IDNumber = IDNumberGenerator();
                newDev.IDNumber = newDevTeam.IDNumber;
                Console.WriteLine("ID generated, please press any key to continue.");
                Console.ReadKey();

                Console.WriteLine("Will this developer need Pluralsight Access? (y/n)");
                string pluralsightAccessString = Console.ReadLine().ToLower();

                if (pluralsightAccessString == "y")
                {
                    newDevTeam.HasPluralsightAccess = true;
                    newDev.HasPluralsightAccess = true;
                }
                else
                {
                    newDevTeam.HasPluralsightAccess = false;
                    newDev.HasPluralsightAccess = false;
                }

                _devTeamRepo.AddDevToTeam(newDevTeam);
                _devRepo.AddDevToList(newDev);

                bool additionalMember = true;
                while (additionalMember)
                {
                    Console.WriteLine("Would you like to add another member to this team? (y/n)");
                    string additionalMemberAsString = Console.ReadLine().ToLower();
                    if (additionalMemberAsString == "y")
                    {
                        Console.Clear();
                        DevTeam newDevTeamx2 = new DevTeam();
                        Developer newDevx2 = new Developer();

                        newDevTeamx2.TeamName = newDevTeam.TeamName;
                        Console.WriteLine(newDevTeamx2.TeamName);

                        Console.WriteLine("Enter the name of the developer that will be on this team.");
                        newDevTeamx2.Name = Console.ReadLine();
                        newDevx2.Name = newDevTeamx2.Name;

                        Console.WriteLine("Please wait for ID # to be generated.");
                        newDevTeamx2.IDNumber = IDNumberGenerator();
                        newDevx2.IDNumber = newDevTeamx2.IDNumber;
                        Console.WriteLine("ID generated, please press any key to continue.");
                        Console.ReadKey();

                        Console.WriteLine("Will this developer need Pluralsight Access? (y/n)");
                        string pluralsightAccessStringx2 = Console.ReadLine().ToLower();

                        if (pluralsightAccessStringx2 == "y")
                        {
                            newDevTeamx2.HasPluralsightAccess = true;
                            newDevx2.HasPluralsightAccess = true;
                        }
                        else
                        {
                            newDevTeamx2.HasPluralsightAccess = false;
                            newDevx2.HasPluralsightAccess = false;
                        }

                        _devTeamRepo.AddDevToTeam(newDevTeamx2);
                        _devRepo.AddDevToList(newDevx2);
                    }
                    else if (additionalMemberAsString == "n")
                    {
                        additionalMember = false;
                        Console.WriteLine("Congrats on the new team!");
                    }
                }
            }
        }

        private void ViewCurrentTeams()
        {
            Console.Clear();
            List<DevTeam> listofTeams = _devTeamRepo.GetDeveloperTeams();

            foreach (DevTeam dev in listofTeams)
            {
                Console.WriteLine($"Team: {dev.TeamName}\n" +
                    $"Name: {dev.Name}\n" +
                    $"ID #: {dev.IDNumber}\n" +
                    $"Pluralsight Access: {dev.HasPluralsightAccess}");
            }
        }

        private void GetTeamById()
        {
            Console.Clear();
            Console.WriteLine("Enter the ID # of the developer you are looking for and see their current team:");

            string idInputAsString = Console.ReadLine();
            int idInputAsInt = int.Parse(idInputAsString);

            DevTeam dev = _devTeamRepo.GetDeveloperTeamByID(idInputAsInt);

            if (dev != null)
            {
                Console.WriteLine($"Team: {dev.TeamName}\n" +
                    $"Name: {dev.Name}\n" +
                    $"ID #: {dev.IDNumber}\n" +
                    $"Pluralsight Access: {dev.HasPluralsightAccess}");
            }
            else
            {
                Console.WriteLine("No developer by that ID Number");
            }
        }

        private void UpdateTeam()
        {
            ViewCurrentTeams();

            Console.WriteLine("Enter the name of the Team you'd like to update.");

            string oldDevAsString = Console.ReadLine();

            DevTeam newDev = new DevTeam();
            Developer newDeveloper = new Developer();

            Console.WriteLine("Enter the team this developer will be on.");
            newDev.TeamName = Console.ReadLine();

            Console.WriteLine("Please give the name of the developer:");
            newDev.Name = Console.ReadLine();
            newDeveloper.Name = newDev.Name;

            Console.WriteLine("Please input original ID #:");
            string idAsString = Console.ReadLine();
            newDev.IDNumber = int.Parse(idAsString);
            newDeveloper.IDNumber = newDev.IDNumber;

            Console.WriteLine("Will this developer need Pluralsight Access? (y/n)");
            string hasPluralsightString = Console.ReadLine().ToLower();

            if (hasPluralsightString == "y")
            {
                newDev.HasPluralsightAccess = true;
                newDeveloper.HasPluralsightAccess = true;
            }
            else
            {
                newDev.HasPluralsightAccess = false;
                newDeveloper.HasPluralsightAccess = false;
            }

            bool additionalMember = true;
            while (additionalMember)
            {
                Console.WriteLine("Would you like to update another member of this team? (y/n)");
                string additionalMemberAsString = Console.ReadLine().ToLower();
                if (additionalMemberAsString == "y")
                {
                    DevTeam newDevTeamx2 = new DevTeam();
                    Developer newDevx2 = new Developer();

                    Console.WriteLine("Enter the name for this team.");
                    newDevTeamx2.TeamName = Console.ReadLine();

                    Console.WriteLine("Enter the name of the developer that will be on this team.");
                    newDevTeamx2.Name = Console.ReadLine();
                    newDevx2.Name = newDevTeamx2.Name;

                    Console.WriteLine("Please input original ID #:");
                    string idAsStringx2 = Console.ReadLine();
                    newDev.IDNumber = int.Parse(idAsStringx2);
                    newDeveloper.IDNumber = newDev.IDNumber;

                    Console.WriteLine("Will this developer need Pluralsight Access? (y/n)");
                    string pluralsightAccessStringx2 = Console.ReadLine().ToLower();

                    if (pluralsightAccessStringx2 == "y")
                    {
                        newDevTeamx2.HasPluralsightAccess = true;
                        newDevx2.HasPluralsightAccess = true;
                    }
                    else
                    {
                        newDevTeamx2.HasPluralsightAccess = false;
                        newDevx2.HasPluralsightAccess = false;
                    }

                    _devTeamRepo.AddDevToTeam(newDevTeamx2);
                    _devRepo.AddDevToList(newDevx2);
                }
                else if (additionalMemberAsString == "n")
                {
                    additionalMember = false;
                }
            }

            bool wasUpdated = _devTeamRepo.UpdateDeveloperTeam(oldDevAsString, newDev);

            if (wasUpdated)
            {
                Console.WriteLine("Developer team information updated");
            }
            else
            {
                Console.WriteLine("Developer team could not be updated");
            }
        }

        private void RemoveDeveloperTeam()
        {
            ViewCurrentTeams();

            Console.WriteLine("\nEnter the ID # of the team member you would like to remove.");

            string inputAsString = Console.ReadLine();
            int inputAsInt = int.Parse(inputAsString);

            bool wasDeleted = _devTeamRepo.RemoveDevTeam(inputAsInt);

            if (wasDeleted)
            {
                Console.WriteLine("Developer team successfully removed");
            }
            else
            {
                Console.WriteLine("Developer team could not be deleted");
            }
        }

        public void SeedDevList()
        {
            Developer J_Lowery = new Developer("Jonathan Lowery", 1548, false);
            Developer L_Jones = new Developer("Laura Jones", 1432, true);
            Developer M_Rodriguez = new Developer("Manuel Rodriguez", 1186, true);
            Developer W_James = new Developer("William James", 8752, true);
            Developer C_Monroe = new Developer("Charles Monroe", 7648, false);
            Developer L_McQueen = new Developer("Lightning McQueen", 7241, true);
            Developer W_Pierce = new Developer("Wendy Pierce", 9562, false);
            Developer E_Cartman = new Developer("Eric Cartman", 2845, true);
            Developer B_Webber = new Developer("Bailey Webber", 1754, true);
            _devRepo.AddDevToList(J_Lowery);
            _devRepo.AddDevToList(L_Jones);
            _devRepo.AddDevToList(M_Rodriguez);
            _devRepo.AddDevToList(W_James);
            _devRepo.AddDevToList(C_Monroe);
            _devRepo.AddDevToList(L_McQueen);
            _devRepo.AddDevToList(W_Pierce);
            _devRepo.AddDevToList(E_Cartman);
            _devRepo.AddDevToList(B_Webber);
        }

        public int IDNumberGenerator()
        {
            Random random = new Random();
            int idNumber = random.Next(1000, 9999);
            return idNumber;
        }
    }
}
