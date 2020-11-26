namespace BoatHarbour
{
    class SailBoat : Boat
    {
        public int VesselLengthInFeet { get; set; }
        public SailBoat()
        {
            ID = "S-" + Helpers.GetThreeRandomLetters();
            WeightInKg = rnd.Next(800, 6001);
            MaxSpeedInKnots = rnd.Next(0, 13);
            VesselLengthInFeet = rnd.Next(10, 61);
            DockingDaysLeft = 4;
            BoatSize = 2;
            Type = "Sailboat";
        }

        public override int GetUniqueValue()
        {
            return this.VesselLengthInFeet;
        }
    }
}
