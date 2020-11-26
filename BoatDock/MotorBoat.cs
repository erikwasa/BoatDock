namespace BoatDock
{
    class MotorBoat : Boat
    {
        public int HorsePower { get; set; }
        public MotorBoat()
        {
            ID = "M-" + Helpers.GetThreeRandomLetters();
            WeightInKg = rnd.Next(200, 3001);
            MaxSpeedInKnots = rnd.Next(0, 61);
            DockingDaysLeft = 3;
            BoatSize = 1;
            HorsePower = rnd.Next(10, 1001);
            Type = "Motorboat";
        }
    }
}
