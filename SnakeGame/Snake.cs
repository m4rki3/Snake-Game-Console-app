using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.SnakeGame;
public class Snake
{
    private const int headIndex = 0;
    #region Properties
    public int DollarsGot { get; internal set; }
    public int Length { get; internal set; }
    public List<(int, int)> Position { get; private set; }
    public (int, int) Head { get; private set; }
    public (int, int) PlaceForTail { get; private set; }
    #endregion
    public Snake()
    {
        DollarsGot = 0;
        Length = 10;
        Head = (9, 0);
        PlaceForTail = (0, 1);
        Position = new()
        {
            Head, (8, 0), (7, 0), (6, 0), (5, 0),
            (4, 0), (3, 0), (2, 0), (1, 0), (0, 0)
        };
    }
    #region Methods
    public void MoveBody()
    {
        PlaceForTail = Position[Length - 1];
        for (int i = Length - 1; i > 0; i--)
        {
            Position[i] = Position[i - 1];
        }
    }
    public void MoveUp()
    {
        MoveBody();
        Head = (Head.Item1, Head.Item2 - 1);
        Position[headIndex] = Head;
    }
    public void MoveDown()
    {
        MoveBody();
        Head = (Head.Item1, Head.Item2 + 1);
        Position[headIndex] = Head;
    }
    public void MoveRight()
    {
        MoveBody();
        Head = (Head.Item1 + 1, Head.Item2);
        Position[headIndex] = Head;
    }
    public void MoveLeft()
    {
        MoveBody();
        Head = (Head.Item1 - 1, Head.Item2);
        Position[headIndex] = Head;
    }
    public Action SwitchDirection() =>
        Console.ReadKey(intercept: true).Key switch
        {
            ConsoleKey.W => MoveUp,
            ConsoleKey.S => MoveDown,
            ConsoleKey.A => MoveLeft,
            ConsoleKey.D => MoveRight,
        };
    public void Show()
    {
        Console.SetCursorPosition(PlaceForTail.Item1, PlaceForTail.Item2);
        Console.Write(' ');
        int i;
        for (i = 0; i < Length; i++)
        {
            Console.SetCursorPosition(Position[i].Item1, Position[i].Item2);
            Console.Write('+');
        }
    }
    public bool OutOfZone(int fieldWidth, int fieldHeight)
    {
        return Head.Item1 < 0 || Head.Item2 < 0
            || Head.Item1 >= fieldWidth
            || Head.Item2 >= fieldHeight;
    }
    public bool AteBody()
    {
        for (int i = 1; i < Length; i++)
        {
            if (Head == Position[i]) return true;
        }
        return false;
    }
    #endregion
}