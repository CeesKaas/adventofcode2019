using System;

namespace Day13
{
    internal class AutoJoyStick : IInput
    {
        public long ReadInput()
        {
            if (Paddle.Current.X > Ball.Current.X)
                return -1;
            if (Paddle.Current.X < Ball.Current.X)
                return 1;
            return 0;
        }
    }
}