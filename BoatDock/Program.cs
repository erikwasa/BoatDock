using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace BoatDock
{

    class Program
    {
        private const int totalDockingSpaces = 25;

        static void Main()
        {
            Boat[] dock = new Boat[totalDockingSpaces];
            List<Boat> refusedBoats = new List<Boat>();

            bool alwaysTrue = true;

            while (alwaysTrue)
            {
                NewDay(dock);

                var newlyArrivedBoats = RandomBoatsArriving(5);

                for (int i = 0; i < newlyArrivedBoats.Count; i++)
                {
                    var boat = newlyArrivedBoats[i];

                    DockingPlace dockingPlace = DockingPlaceLocator(dock, boat.BoatSize);

                    if (dockingPlace.BoatWillFit)
                    {
                        boat.SetDockSpaces(dockingPlace.StartingIndex);
                        
                        foreach (var dockSpace in boat.PositionIndexes)
                        {
                            dock[dockSpace] = boat;
                        }
                    }
                    else
                    {
                        refusedBoats.Add(boat);
                    }
                }
                PrintDockStatus(dock, refusedBoats);
                Console.ReadKey();
            }
        }

        private static void PrintDockStatus(Boat[] dock, List<Boat> refusedBoats)
        {
            Console.WriteLine($"Pos\tType\t\tID\tDays Left");
            for (int i = 0; i < dock.Length; i++)
            {
                if (dock[i] == null)
                {
                    Console.WriteLine($"{i + 1}\tEmpty");
                }
                else
                {
                    var boat = dock[i];
                    Console.WriteLine($"{boat.ReadablePosition}\t{boat.Type}\t{boat.ID}\t{boat.DockingDaysLeft}");

                    int skipRestOfCurrentBoatIndex = boat.BoatSize - 1;
                    i += skipRestOfCurrentBoatIndex;
                }
            }

            var emptySpaces = dock.Where(b => b == null).Count();

            Console.WriteLine($"\nEmpty docking spaces: {emptySpaces}");
            Console.WriteLine($"\nNumber of boats refused so far: {refusedBoats.Count}");
        }

        private static DockingPlace DockingPlaceLocator(Boat[] dock, int boatSize)
        {
            DockingPlace dockingPlace = new DockingPlace();

            for (int j = 0; j < dock.Length; j++)
            {
                if (dock[j] == null)
                {
                    dockingPlace.Size++;
                }
                else
                {
                    dockingPlace.Size = 0;
                    dockingPlace.StartingIndex = j + dock[j].BoatSize;

                    int skipRestOfCurrentBoatIndex = j + (dock[j].BoatSize - 1);
                    j = skipRestOfCurrentBoatIndex;
                }

                if (dockingPlace.Size == boatSize)
                {
                    dockingPlace.BoatWillFit = true;
                    break;
                }
            }
            return dockingPlace;
        }

        private static void NewDay(Boat[] dock)
        {
            Console.Clear();

            var boats = GetBoatsInDock(dock);
            UpdateDockingDaysLeft(boats);
            BoatsLeavingDock(dock, boats);
        }

        private static void BoatsLeavingDock(Boat[] dock, List<Boat> boats)
        {
            var boatsLeaving = boats.Where(b => b.DockingDaysLeft == 0);
            foreach (var boat in boatsLeaving)
            {
                foreach (var index in boat.PositionIndexes)
                {
                    dock[index] = null;
                }
            }
        }
        private static List<Boat> GetBoatsInDock(Boat[] dock)
        {
            var boats = dock.Where(b => b != null).Distinct().ToList();
            return boats;
        }

        private static void UpdateDockingDaysLeft(List<Boat> boats)
        {
            boats.ForEach(b => b.DayHasPassed());
        }
        private static List<Boat> RandomBoatsArriving(int numOfBoats)
        {
            var random = new Random();
            var boats = new List<Boat>();

            for (int i = 0; i < numOfBoats; i++)
            {
                var boatType = random.Next(1, 4);
                switch (boatType)
                {
                    case 1:
                        {
                            var boat = new MotorBoat();
                            boats.Add(boat);
                            break;
                        }
                    case 2:
                        {
                            var boat = new SailBoat();
                            boats.Add(boat);
                            break;
                        }

                    case 3:
                        {
                            var boat = new CargoShip();
                            boats.Add(boat);
                            break;
                        }

                    default:
                        break;
                }
            }

            return boats;
        }
    }
}
