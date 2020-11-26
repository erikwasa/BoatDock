using System;
using System.Linq;

namespace BoatHarbour
{
    public abstract class Boat
    {
        public string ReadablePosition
        {
            get
            {
                var startingPosition = $"{PositionIndexes.First() + 1}";
                return BoatSize > 1 ? $"{startingPosition}-{PositionIndexes.Last() + 1}" : startingPosition;
            }
        }

        public int[] PositionIndexes { get; set; }
        public string Type { get; protected set; }
        protected Random rnd = new Random();
        public string ID { get; protected set; }
        public int WeightInKg { get; protected set; }
        public int MaxSpeedInKnots { get; protected set; }
        public int BoatSize { get; protected set; }
        private int dockingDaysLeft;

        public int DockingDaysLeft
        {
            get { return dockingDaysLeft; }
            protected set
            {
                dockingDaysLeft = value;
            }
        }

        public abstract int GetUniqueValue();

        public void DayHasPassed()
        {
            DockingDaysLeft--;
        }

        internal void SetPositionIndexes(int startingIndex)
        {
            PositionIndexes = Enumerable.Range(startingIndex, BoatSize).ToArray();
        }
    }
}
