namespace BoatHarbour
{
    internal class Berth
    {
        public Berth()
        {
            Size = 0;
            StartingIndex = 0;
            BoatWillFit = false;
        }

        public int Size { get; set; }
        public int StartingIndex { get; set; }
        public bool BoatWillFit { get; set; }
    }
}
