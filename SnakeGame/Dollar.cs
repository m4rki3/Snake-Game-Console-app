using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.SnakeGame;
public class Dollar
{
    private const int fieldStart = 0;
    private readonly Random random = new();
    public (int, int) Position { get; private set; }
    public void SetNewPosition(Snake snake, int fieldWidth, int fieldHeight)
    {
        int left = 0, top = 0, i;
        bool found = false;
        bool all;
        while (!found)
        {
            left = random.Next(fieldStart, fieldWidth);
            top = random.Next(fieldStart, fieldHeight);
            all = true;
            for (i = 0; i < snake.Length && all; i++)
            {
                if (left == snake.Position[i].Item1
                 && top == snake.Position[i].Item2)
                    all = false;
            }
            found = all;
        }
        Position = (left, top);
    }
    public void Show()
    {
        Console.SetCursorPosition(Position.Item1, Position.Item2);
        Console.Write('$');
    }
}