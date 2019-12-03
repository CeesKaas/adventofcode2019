namespace Day3
{
    public class Vector
    {
        public Vector(Direction direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }

        public Direction Direction { get; set; }
        public int Distance { get; set; }
    }
}