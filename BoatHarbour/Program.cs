using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace BoatHarbour
{

    class Program
    {
       private const int harbourPositions = 25;

        static void Main()
        {
            Boat[] harbour = new Boat[harbourPositions];
            List<Boat> refusedBoats = new List<Boat>();

            bool alwaysTrue = true;

            while (alwaysTrue)
            {
                NewDay(harbour);

                var newlyArrivedBoats = RandomBoatsArriving(5);

                for (int i = 0; i < newlyArrivedBoats.Count; i++)
                {
                    var boat = newlyArrivedBoats[i];

                    Berth berth = BerthLocator(harbour, boat.BoatSize);

                    if (berth.BoatWillFit)
                    {
                        for (int k = 0; k < boat.BoatSize; k++)
                        {
                            boat.SetPositionIndexes(berth.StartingIndex);
                            harbour[berth.StartingIndex + k] = boat;
                        }
                    }
                    else
                    {
                        refusedBoats.Add(boat);
                    }
                }
                PrintHarbourStatus(harbour, refusedBoats);
                Console.ReadKey();
            }
        }

        private static void PrintHarbourStatus(Boat[] harbour, List<Boat> refusedBoats)
        {
            Console.WriteLine($"Pos\tType\t\tID\tDays Left");
            for (int i = 0; i < harbour.Length; i++)
            {
                if (harbour[i] == null)
                {
                    Console.WriteLine($"{i + 1}\tEmpty");
                }
                else
                {
                    var boat = harbour[i];
                    Console.WriteLine($"{boat.ReadablePosition}\t{boat.Type}\t{boat.ID}\t{boat.DockingDaysLeft}");

                    int skipRestOfCurrentBoatIndex = boat.BoatSize - 1;
                    i += skipRestOfCurrentBoatIndex;
                }
            }

            var emptySpots = harbour.Where(b => b == null).Count();

            Console.WriteLine($"\nEmpty spots: {emptySpots}");
            Console.WriteLine($"\nNumber of boats refused so far: {refusedBoats.Count}");
        }

        private static Berth BerthLocator(Boat[] harbour, int boatSize)
        {
            Berth berth = new Berth();

            for (int j = 0; j < harbour.Length; j++)
            {
                if (harbour[j] == null)
                {
                    berth.Size++;
                }
                else
                {
                    berth.Size = 0;
                    berth.StartingIndex = j + harbour[j].BoatSize;

                    int skipRestOfCurrentBoatIndex = j + (harbour[j].BoatSize - 1);
                    j += skipRestOfCurrentBoatIndex;
                }

                if (berth.Size == boatSize)
                {
                    berth.BoatWillFit = true;
                    break;
                }
            }
            return berth;
        }

        private static void NewDay(Boat[] harbour)
        {
            Console.Clear();

            List<Boat> boats = UpdateDockingDaysLeft(harbour);
            BoatsLeavingHarbour(harbour, boats);

        }

        private static void BoatsLeavingHarbour(Boat[] harbour, List<Boat> boats)
        {
            var boatsLeaving = boats.Where(b => b.DockingDaysLeft == 0);
            foreach (var boat in boatsLeaving)
            {
                foreach (var index in boat.PositionIndexes)
                {
                    harbour[index] = null;
                }
            }
        }

        private static List<Boat> UpdateDockingDaysLeft(Boat[] harbour)
        {
            var boats = harbour.Where(b => b != null).Distinct().ToList();
            boats.ForEach(b => b.DayHasPassed());
            return boats;
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
