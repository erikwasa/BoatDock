using System;
using System.Linq;

namespace BoatDock
{
    public abstract class Boat
    {
        public int[] PositionIndexes { get; set; }
        public string Type { get; protected set; }
        protected Random rnd = new Random();
        public string ID { get; protected set; }
        public int WeightInKg { get; protected set; }
        public int MaxSpeedInKnots { get; protected set; }
        public int BoatSize { get; protected set; }
        public int DockingDaysLeft { get; protected set; }
        public string ReadablePosition
        {
            get
            {
                var startingPosition = $"{PositionIndexes.First() + 1}";
                return BoatSize > 1 ? $"{startingPosition}-{PositionIndexes.Last() + 1}" : startingPosition;
            }
        }

        public void DayHasPassed()
        {
            DockingDaysLeft--;
        }

        internal void SetDockSpaces(int startingIndex)
        {
            PositionIndexes = Enumerable.Range(startingIndex, BoatSize).ToArray();
        }
    }
}
