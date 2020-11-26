namespace BoatHarbour
{
    class CargoShip : Boat
    {
        public int NumContainers { get; set; }
        public CargoShip()
        {
            ID = "L-" + Helpers.GetThreeRandomLetters();
            WeightInKg = rnd.Next(3000, 20_001);
            MaxSpeedInKnots = rnd.Next(0, 21);
            DockingDaysLeft = 6;
            BoatSize = 4;
            NumContainers = rnd.Next(0, 501);
            Type = "Cargo Ship";
        }

        public override int GetUniqueValue()
        {
            return NumContainers;
        }
    }
}
