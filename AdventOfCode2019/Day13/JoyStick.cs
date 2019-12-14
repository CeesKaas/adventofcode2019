using System;

namespace Day13
{
    internal class JoyStick : IInput
    {
        public long ReadInput()
        {
            //return 0;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 27);
            Console.WriteLine("input");
            var key = Console.ReadKey(true);
            Console.ForegroundColor = Console.BackgroundColor;
            Console.SetCursorPosition(0, 27);
            Console.WriteLine("input");
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    return -1;
                case ConsoleKey.RightArrow:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}