using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Day15
{
    public class RepairRobot
    {
        public RepairRobot(IOutput output, IInput input)
        {
            Output = output;
            Input = input;
        }

        public IOutput Output { get; }
        public IInput Input { get; }

        internal void Start()
        {
            Task.Factory.StartNew(Run);
        }

        private void Out(Direction d)
        {
            Output.WriteOutput((long)d);
        }
        private long In()
        {
            return Input.ReadInput();
        }

        private void Run()
        {
            Out(Direction.North);
            while (true)
            {

            }
        }

        private Direction GetOpposite(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                default: return (Direction)0;
            }
        }
        private Direction TurnRight(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.East;
                case Direction.South:
                    return Direction.West;
                case Direction.West:
                    return Direction.North;
                case Direction.East:
                    return Direction.South;
                default: return (Direction)0;
            }
        }
    }
}
